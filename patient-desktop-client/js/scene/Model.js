
// TODO make model inherit from SkinnedMesh
// TODO remove magic values
Model = function(theScene, theCamera) {

    /* the mesh for this model */
    var mesh = null;

    /* the arm mesh */
    var armMesh = null;

    /* the THREE.Scene this model belongs to */
    var scene = theScene;

    /* the camera */
    var camera = theCamera;

    /* object which provides bone data */
    var bodyData = new BodyData();

    /* array of objects of type Bone */
    var bones = null;

    var rightThumbDir = null;
    var leftThumbDir = null;
    var rightHipDir = null;

    var exercise = null;

    /* load the character model */
    new THREE.JSONLoader().load("models/stan-lee/stan_lee.js", setUp);

    this.update = function () {
        var kinectBones = bodyData.getLatestBoneData();

        if (mesh == null || kinectBones == null) {
            return;
        }

        if (exercise == null) {
            exercise = new Exercise(5, 5, BoneType.shoulderRightElbowRight, BoneType.elbowRightWristRight, Math.PI, Math.PI / 6);
            exercise.onStart(exerciseStart);
            exercise.onSetDone(exerciseSetDone);
            exercise.onRepDone(exerciseRepDone);
            exercise.onComplete(exerciseComplete);
        }

        exercise.update(kinectBones);
        applyKinectBonesToMesh(kinectBones);
    }

    function exerciseStart() {
        setExerciseOk();
        setExerciseText("Repetitions: 0 <br /> Sets: 0");
        playExerciseStart();
    }

    function exerciseRepDone() {
        setExerciseText("Repetitions: " + exercise.getRepCount() + "<br /> Sets: " + exercise.getSetCount());
        playRepDone();
    }

    function exerciseSetDone() {
        setExerciseText("Repetitions: 0 <br /> Sets: " + exercise.getSetCount());
    }

    function exerciseComplete() {
        setExerciseText("Exercise is now complete.");
        playExerciseDone();
    }

    function setExerciseText(text) {
        document.getElementById("msg").innerHTML = text;
    }

    function playExerciseStart() {
        document.getElementById("exercise_start").play();
    }

    function playExerciseDone() {
        document.getElementById("exercise_done").play();
    }

    function playRepDone() {
        document.getElementById("rep_done").play();
    }

    function setUp(geometry, materials) {
        for (i = 0; i < materials.length; i++) {
            materials[i].skinning = true;
        }

        mesh = new THREE.SkinnedMesh(geometry, new THREE.MeshFaceMaterial(materials));
        mesh.scale.set(1, 1, 1);
        mesh.castShadow = true;

        scene.add(mesh);

        new THREE.JSONLoader().load("models/stan-lee/stan_lee_right_arm.js", setUpArm);
    }

    /* TODO temporary function */
    function setUpArm(geometry, materials) {
        //var glowMaterial = new THREE.ShaderMaterial({
        //    uniforms: {
        //        "c": { type: "f", value: 1.5 }, // opacity?
        //        "p": { type: "f", value: 1.4 },
        //        glowColor: { type: "c", value: new THREE.Color(0xff0000) },
        //        viewVector: { type: "v3", value: camera.position }
        //    },
        //    vertexShader: document.getElementById("vertexShader").textContent,
        //    fragmentShader: document.getElementById("fragmentShader").textContent,
        //    side: THREE.FrontSide,
        //    blending: THREE.AdditiveBlending,
        //    transparent: true,
        //    skinning: true
        //});
        //glowMaterial.skinning = true;
        armMesh = new THREE.SkinnedMesh(
            geometry,
            new THREE.MeshBasicMaterial({ color: 0xff0000, wireframe: true, transparent: true, skinning: true})
            );
        // link arm skeleton to model skeleton!
        armMesh.skeleton = mesh.skeleton;
        scene.add(armMesh);
    }

    function applyKinectBonesToMesh(kinectBones) {
        
        for (i = 0; i < mapping.length; i++) {
            // determine which bones to animate
            if(i == 0 || i == 2 || i == 3 || i == 5 || i == 6 || i == 9 || i == 10) {

                var boneType = mapping[i];

                var dv = new THREE.Vector3(0, 0, 1); // the current direction vector for the bone is always (0, 0, 1)
                var tv = kinectBones[boneType].getDirectionVector();
                // no data about bone - do not rotate
                if (tv == null) {
                    continue;
                }

                // get the local version of tv
                tv = translateToLocal(i, tv);

                // determine the axis and angle of rotation
                var axis = new THREE.Vector3().crossVectors(dv, tv).normalize();
                var angle = dv.angleTo(tv);

                // apply rotation
                mesh.skeleton.bones[i].quaternion.multiply(new THREE.Quaternion().setFromAxisAngle(axis, angle)).normalize();

                // bone specific rotations
                // TODO not working currently!!!
                if (i == 5 || i == 6) {
                    mesh.skeleton.bones[i].rotation.z = 0;

                    //if (rightThumbDir != null) {
                    //    var zNormal = new THREE.Vector3(0, 0, 1);
                    //    var proj;

                    //    var dir1 = translateToLocal(i, rightThumbDir);
                    //    proj = dir1.clone().projectOnVector(zNormal);
                    //    dir1.sub(proj).normalize();

                    //    var dir2 = kinectBones[BoneType.wristRightThumbRight].getDirectionVector();
                    //    proj = dir2.clone().projectOnVector(zNormal);
                    //    dir2.sub(proj).normalize();


                    //    var angle = dir1.angleTo(dir2);
                    //    //mesh.skeleton.bones[i].quaternion.multiply(new THREE.Quaternion().setFromAxisAngle(zNormal, angle)).normalize();
                    //}
                    //rightThumbDir = kinectBones[BoneType.wristRightThumbRight].getDirectionVector();
                } else if (i == 9 || i == 10) {
                    mesh.skeleton.bones[i].rotation.z = Math.PI;
                }
            }
        }
    }

    function translateToLocal(boneType, vector) {
        return convertToLocalSpace( boneType, translateToLocalOrigin(boneType, vector) );
    }

    // TODO change function & arg names?
    function translateToLocalOrigin(boneIndex, vector) {
        var translationVector = getGlobalBonePosition(mesh.skeleton.bones[boneIndex]);
        return new THREE.Vector3().addVectors(vector, translationVector);
    }

    // TODO change fucntion & arg names?
    function convertToLocalSpace(boneIndex, vector) {
        return mesh.skeleton.bones[boneIndex].worldToLocal(vector).normalize();
    }

    function getGlobalBonePosition(bone) {
        return new THREE.Vector3().setFromMatrixPosition(bone.matrixWorld);
    }

    function setExerciseOk() {
        armMesh.material = new THREE.MeshBasicMaterial({ color: 0x00ff00, wireframe: true, transparent: true, skinning: true });
    }

    function setExerciseWrong() {
        armMesh.material = new THREE.MeshBasicMaterial({ color: 0xff0000, wireframe: true, transparent: true, skinning: true });
    }

    /*
     * A mapping between the mesh bones and the Kinect bones.
     * TODO this should be a separate file?
     */
    var mapping = [
        BoneType.spineBaseSpineMid,           // 0
        BoneType.spineMidSpineShoulder,       // 1
        BoneType.spineShoulderNeck,           // 2
        BoneType.neckHead,                    // 3
        BoneType.spineShoulderShoulderRight,  // 4
        BoneType.shoulderRightElbowRight,     // 5
        BoneType.elbowRightWristRight,        // 6
        BoneType.wristRightHandRight,         // 7
        BoneType.spineShoulderShoulderLeft,   // 8
        BoneType.shoulderLeftElbowLeft,       // 9
        BoneType.elbowLeftWristLeft,          // 10
        BoneType.wristLeftHandLeft,           // 11
        BoneType.spineBaseHipRight,           // 12
        BoneType.hipRightKneeRight,           // 13
        BoneType.kneeRightAnkleRight,         // 14
        BoneType.ankleRightFootRight,         // 15
        BoneType.spineBaseHipLeft,            // 16
        BoneType.hipLeftKneeLeft,             // 17
        BoneType.kneeLeftAnkleLeft,           // 18
        BoneType.ankleLeftFootLeft            // 19
    ];

}