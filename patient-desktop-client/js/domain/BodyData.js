
function BodyData() {

    /* number of bones in body */
    this.BONE_COUNT = 14;

    /* the KinectDataExtractor objects, which provides raw joint data */
    var dataExtractor = new KinectDataExctractor();
    
    /* array containing bones */
    var bones;

    /* array containing joints */
    var joints;

    init();

    this.getLatestBoneData = function () {
        // call getLatestJointData

        // calculate direction vectors - could delegate this to bone objects??
    }

    function init() {
        dataExtractor.startSensor();

        bones = new Array(this.BONE_COUNT);
        // populate bones array with bone objects...
    }

}