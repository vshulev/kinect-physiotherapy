
/* Extracts raw Kinect data */
function KinectDataExtractor() {

    /* the SDK Kinect object */
    var kinect = WindowsPreview.Kinect;

    /* the Kinect sensor */
    var sensor = kinect.KinectSensor.getDefault();

    /* A reader objects that reads body frames from the sensor */
    var bodyFrameReader = sensor.bodyFrameSource.openReader();
    bodyFrameReader.addEventListener("framearrived", readerBodyFrameArrived);
  
    /* an empty array that will store body objects read from the sensor */
    var bodies = new Array(sensor.bodyFrameSource.bodyCount);

    /* indicates wether the Kinect sensor is running */
    var running = false;

    /* an array for storing joints data */
    var joints = null;

    /* starts the Kinect sensor and starts gathering body data */
    this.startSensor = function () {
        if (!running) {
            sensor.open();
            running = true;
        }
    }

    /* returns an array of Joint objects. Each Joint object contains the latest data about joint. May return null. */
    this.getLatestJointData = function () {
        return joints;
    }

    /* called every time a new body frame is read from the sensor */
    function readerBodyFrameArrived(args) {
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
                    var joint = kinectJoints.lookup(i);
                    joints.push({
                        x: joint.position.x,
                        y: joint.position.y,
                        z: joint.position.z
                    });
                }
                break;
            }
        }
    }

}
