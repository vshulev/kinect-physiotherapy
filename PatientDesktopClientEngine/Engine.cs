using System.Collections.Generic;
using WindowsPreview.Kinect;

namespace PatientDesktopClientEngine
{
    public sealed class Engine
    {

        private BodyDataReader bodyDataReader;
        private GestureDetector gestureDetector;

        public Engine()
        {
            bodyDataReader = new BodyDataReader();
            bodyDataReader.StartSensor();
            gestureDetector = new GestureDetector();
            bodyDataReader.Subscribe(gestureDetector);
        }

        public IReadOnlyDictionary<JointType, Joint> Joints
        {
            get
            {
                return bodyDataReader.Joints;
            }
        }

        public float GestureProgress
        {
            get
            {
                return gestureDetector.GestureProgress;
            }
        }

    }
}
