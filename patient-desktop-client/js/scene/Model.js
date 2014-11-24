
// TODO make model inherit from SkinnedMesh
// TODO remove magic values
function Model(theScene, theCamera) {
    
    /* the number of bones this mesh has */
    var meshBones;

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

    /* load the character model */
    new THREE.JSONLoader().load("models/stan-lee/stan_lee.js", setUp);

    this.update = function () {
        var kinectBones = bodyData.getLatestBoneData();

        if (mesh == null || kinectBones == null) {
            return;
        }
        applyKinectBonesToMesh(kinectBones);
    }

    function setUp(geometry, materials) {
        for (i = 0; i < materials.length; i++) {
            materials[i].skinning = true;
        }

        mesh = new THREE.SkinnedMesh(geometry, new THREE.MeshFaceMaterial(materials));
        mesh.scale.set(1, 1, 1);

        scene.add(mesh);

        mesh_bones = mesh.skeleton.bones.length;

        new THREE.JSONLoader().load("models/stan-lee/stan_lee_right_arm.js", setUpArm);
    }

    /* TODO temporary function */
    function setUpArm(geometry, materials) {
        var glowMaterial = new THREE.ShaderMaterial({
            uniforms: {
                "c": { type: "f", value: 1.5 }, // opacity?
                "p": { type: "f", value: 1.4 },
                glowColor: { type: "c", value: new THREE.Color(0xff0000) },
                viewVector: { type: "v3", value: camera.position }
            },
            vertexShader: document.getElementById("vertexShader").textContent,
            fragmentShader: document.getElementById("fragmentShader").textContent,
            side: THREE.FrontSide,
            blending: THREE.AdditiveBlending,
            transparent: true,
            skinning: true
        });
        glowMaterial.skinning = true;
        armMesh = new THREE.SkinnedMesh(
            geometry,
            new THREE.MeshBasicMaterial({ color: 0xff0000, wireframe: true, transparent: true, skinning: true})
            );
        // link arm skeleton to model skeleton!
        armMesh.skeleton = mesh.skeleton;

        scene.add(armMesh);
    }

    function applyKinectBonesToMesh(kinectBones) {
        for (i = 0; i < 7/*mapping.length*/; i++) {
            var boneType = mapping[i];
           
            var dv = new THREE.Vector3(0, 0, 1);

            var tv = kinectBones[boneType].getDirectionVector();
            tv = translateToLocalOrigin(i, tv);
            tv = convertToLocalSpace(i, tv);

            var axis = new THREE.Vector3().crossVectors(dv, tv).normalize();
            var angle = dv.angleTo(tv);

            mesh.skeleton.bones[i].rotateOnAxis(axis, angle);

            //if (i == 0) {
            //    mesh.rotation.y = mesh.skeleton.bones[0].rotation.z;
            //}

            //mesh.skeleton.bones[i].rotation.z = 0;
        }
    }

    function translateToLocalOrigin(boneIndex, vector) {
        var translationVector = getGlobalBonePosition(mesh.skeleton.bones[boneIndex]);
        return new THREE.Vector3().addVectors(vector, translationVector);
    }

    function convertToLocalSpace(boneIndex, vector) {
        return mesh.skeleton.bones[boneIndex].worldToLocal(vector).normalize();
    }

    function getGlobalBonePosition(bone) {
        return new THREE.Vector3().setFromMatrixPosition(bone.matrixWorld);
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