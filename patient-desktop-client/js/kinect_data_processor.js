
function KinectDataProcessor() {

    /* the object which will be providing Kinect joint data */
    var kinectDataExtractor = new KinectDataExtractor();
    kinectDataExtractor.startSensor();

    this.getLatestJointOrientations = function () {
        
    }

}