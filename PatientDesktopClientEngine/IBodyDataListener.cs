using System.Collections.Generic;
using WindowsPreview.Kinect;

namespace PatientDesktopClientEngine
{
    // TODO change access modifier
    public interface IBodyDataListener
    {
        void BodyDataArrived(IReadOnlyDictionary<JointType, Joint> joints, ulong bodyId);

        void NoBodyTracked();

    }
}
