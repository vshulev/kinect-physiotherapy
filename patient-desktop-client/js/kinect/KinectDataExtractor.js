
function KinectDataExtractor() {


    /* the SDK Kinect object */
    var kinect = WindowsPreview.Kinect;

    /* the Kinect sensor */
    var sensor = kinect.KinectSensor.getDefault();

    /* A reader object which reads body frames from the sensor */
    var bodyFrameReader = sensor.bodyFrameSource.openReader();
    bodyFrameReader.addEventListener("framearrived", readerBodyFrameArrived);

    /* an empty array that will store body objects read from the sensor */
    var bodies = new Array(sensor.bodyFrameSource.bodyCount);

    /* indicates wether the Kinect sensor is running */
    var running = false;

    /* an array for storing joints data
     * each joint is of type WindowsPreview.Kinect
     */
    var joints = null;

    /* starts the Kinect sensor */
    this.startSensor = function () {
        if (!running) {
            sensor.open();
            running = true;
        }
    }

    /* stops the Kinect sensor */
    this.stopSensor = function () {
        if (running) {
            snesor.close();
            running = false;
        }
    }

    /* returns an array of Joint objects. Each Joint object contains the latest data about joint. May return null. */
    this.getLatestJointData = function () {
        return joints;
    }

    /* called every time a new body frame is read from the sensor */
    function readerBodyFrameArrived(args) {
        // get bodies data from frame
        var bodyFrame = args.frameReference.acquireFrame();
        if (bodyFrame == null) {
            return;
        }
        bodyFrame.getAndRefreshBodyData(bodies);
        bodyFrame.close();

        // find a tracked body and store its joint positions
        for (i = 0; i < bodies.length; i++) {
            // find a tracked body and exit loop as soon as one is found
            if (bodies[i].isTracked) {
                var kinectJoints = bodies[i].joints;
                joints = new Array();
                for (i = 0; i < kinectJoints.size; i++) {
                    joints.push(kinectJoints.lookup(i));
                }
                break;
            }
        }
    }

}


