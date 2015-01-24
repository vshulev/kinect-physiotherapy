using System.Collections.Generic;
using Microsoft.Kinect;

namespace PatientDesktopClientEngine
{
    // TODO change access modifier
    public interface IBodyDataListener
    {
        void BodyDataArrived(IReadOnlyDictionary<JointType, Joint> joints, ulong bodyId);

        void NoBodyTracked();

    }
}
