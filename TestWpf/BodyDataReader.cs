using System.Collections.Generic;
using Microsoft.Kinect;

// TODO change namespace - add organization name & project name!
namespace PatientDesktopClientEngine
{
    // TODO change access modifiers
    public sealed class BodyDataReader
    {

        // the Kinect sensor
        private KinectSensor sensor;

        // reads frames containing body data from Kinect
        private BodyFrameReader reader;

        // an array of body data
        private Body[] bodies;

        // a list of objects that need to be notified when a body frame arrives
        private List<IBodyDataListener> subscribers;

        public BodyDataReader()
        {
            sensor = KinectSensor.GetDefault();
            reader = sensor.BodyFrameSource.OpenReader();
            reader.FrameArrived += reader_FrameArrived;

            bodies = new Body[sensor.BodyFrameSource.BodyCount];
            subscribers = new List<IBodyDataListener>();
        }

        // start reading data from the sensor
        // TODO either rename or delete
        public void StartSensor()
        {
            if (!sensor.IsOpen)
            {
                sensor.Open();
            }
        }

        // stop reading data from the sensor and stop the sensor
        // TODO either rename or remove this?
        public void StopSensor()
        {
            if (sensor.IsOpen)
            {
                sensor.Close();
            }
        }

        // adds a method to be called when a body frame arrives
        // TODO think of a better explanation
        public void Subscribe(IBodyDataListener listener)
        {
            subscribers.Add(listener);
        }

        // a dictionary containing up to date joints data
        // is null if no data has been made available yet
        public IReadOnlyDictionary<JointType, Joint> Joints
        {
            get;
            private set;
        }

        // the event which gets fired when a body frame arrives
        private void reader_FrameArrived(object sender, BodyFrameArrivedEventArgs args)
        {

            // acqure a fresh body frame if one is available
            using (BodyFrame bodyFrame = args.FrameReference.AcquireFrame())
            {
                if (bodyFrame == null)
                {
                    return;
                }

                // update all bodies and dispose of the body frame
                bodyFrame.GetAndRefreshBodyData(bodies);
            }

            // find the body with the smallest ID and update the joints
            // TODO validate that there is only a single user in front of the Kinect!
            // TODO should store the tracked body ID so that if a second person accidentally jumps into frame the
            //      exercising person is not screwed up
            ulong trackingId = 0;
            for (int i = 0; i < bodies.Length; i++)
            {
                if (bodies[i].IsTracked)
                {
                    Joints = bodies[i].Joints;
                    trackingId = bodies[i].TrackingId;
                    break; // TODO bad code, refactor
                }
            }

            if (trackingId != 0)
            {
                foreach (IBodyDataListener listener in subscribers)
                {
                    listener.BodyDataArrived(Joints, trackingId);
                }
            }
            else
            {
                foreach (IBodyDataListener listener in subscribers)
                {
                    listener.NoBodyTracked();
                }
            }
        }
    }
}