
BodyData = function() {

    /* number of bones in body */
    // TODO cannot be referenced from inside BodyData. WHYYYYY?????
    this.BONE_COUNT = 24;

    /* the KinectDataExtractor objects, which provides raw joint data */
    //var dataExtractor = new KinectDataExtractor();
    
    /* array containing bones */
    var bones;

    /* array containing joints */
    //var joints = null;

    init();

    this.getLatestBoneData = function () {
        //joints = dataExtractor.getLatestJointData();
        if (joints == null) {
            return null;
        }

        for (i = 0; i < bones.length; i++) {
            bones[i].recalculateDirectionVector(joints);
        }
        return bones;
    }

    function init() {
        // start the Kinect sensor
        //dataExtractor.startSensor();

        // populate bones array
        bones = new Array();
        // TODO cannot access BONE_COUNT from here, WHYYYY ?????
        for (i = 0; i < 24; i++) {
            bones[i] = new Bone(i);
        }
    }

}