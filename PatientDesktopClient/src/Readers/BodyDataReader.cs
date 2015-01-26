using System;
using System.Collections.Generic;
using Microsoft.Kinect;

namespace PatientDesktopClient.Readers
{

    public delegate void BodyDataReadHandler(object source, BodyDataReadEventArgs e);

    sealed class BodyDataReader
    {

        public static readonly BodyDataReader Instance = new BodyDataReader();
        public event BodyDataReadHandler BodyDataRead;
        public ulong CurrentTrackingId
        {
            get;
            private set;
        }

        private Body[] bodies;
        private IReadOnlyDictionary<JointType, Joint> joints;
        private IReadOnlyDictionary<JointType, JointOrientation> jointOrientations;

        private BodyDataReader()
        {
            KinectSensor sensor = KinectSensor.GetDefault();
            BodyFrameReader reader = sensor.BodyFrameSource.OpenReader();
            reader.FrameArrived += frameArrived;
            bodies = new Body[sensor.BodyFrameSource.BodyCount];
            sensor.Open();
        }

        private void frameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame!= null)
                {
                    bodyFrame.GetAndRefreshBodyData(bodies);
                    storeJointData();
                    if (BodyDataRead != null)
                        BodyDataRead(this, new BodyDataReadEventArgs(joints, jointOrientations));
                }
            }
        }

        private void storeJointData()
        {
            CurrentTrackingId = 0;
            for (int i = 0; i < bodies.Length; i++)
            {
                if (bodies[i].IsTracked)
                {
                    joints = bodies[i].Joints;
                    jointOrientations = bodies[i].JointOrientations;
                    CurrentTrackingId = bodies[i].TrackingId;
                    break;
                }
            }
        }

    }

    class BodyDataReadEventArgs : EventArgs
    {
        public IReadOnlyDictionary<JointType, Joint> Joints;
        public IReadOnlyDictionary<JointType, JointOrientation> JointOrientations;

        public BodyDataReadEventArgs(IReadOnlyDictionary<JointType, Joint> joints, IReadOnlyDictionary<JointType, JointOrientation> jointOrientations)
        {
            Joints = joints;
            JointOrientations = jointOrientations;
        }
    }
}
