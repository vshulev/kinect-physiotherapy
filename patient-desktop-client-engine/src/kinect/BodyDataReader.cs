﻿using System.Collections.Generic;
using WindowsPreview.Kinect;

namespace PatientDesktopClientEngine.Kinect
{
    class BodyDataReader
    {
        private bool sensorRunning;
        private KinectSensor sensor;
        private BodyFrameReader reader;
        private IList<Body> bodies;
        private IReadOnlyDictionary<JointType, Joint> joints;
            
        public BodyDataReader()
        {
            sensorRunning = false;
            sensor = KinectSensor.GetDefault();
            reader = sensor.BodyFrameSource.OpenReader();
            reader.FrameArrived += reader_FrameArrived;
        }

        public void StartSensor()
        {
            if (!sensorRunning)
            {
                sensor.Open();
                sensorRunning = true;
            }
        }

        public void StopSensor()
        {
            if (sensorRunning)
            {
                sensor.Close();
                sensorRunning = false;
            }
        }

        public IReadOnlyDictionary<JointType, Joint> GetJoints()
        {
            return joints;
        }

        private void reader_FrameArrived(BodyFrameReader sender, BodyFrameArrivedEventArgs args)
        {
            BodyFrame bodyFrame = args.FrameReference.AcquireFrame();
            if (bodyFrame == null)
            {
                return;
            }
            bodyFrame.GetAndRefreshBodyData(bodies);
            bodyFrame.Dispose();

            for (int i = 0; i < bodies.Count; i++)
            {
                if (bodies[i].IsTracked)
                {
                    joints = bodies[i].Joints;
                }
            }
        }
    }
}