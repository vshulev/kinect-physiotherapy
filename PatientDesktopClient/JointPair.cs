using Microsoft.Kinect;

namespace PatientDesktopClient
{
    public class JointPair
    {
        public JointPair(JointType start, JointType end)
        {
            this.Start = start;
            this.End = end;
        }

        public JointType Start
        {
            get;
            private set;
        }

        public JointType End
        {
            get;
            private set;
        }
    }
}