using Microsoft.Kinect;

namespace PatientDesktopClient.UI.Scene
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
