using System.Collections.Generic;
using Microsoft.Kinect.VisualGestureBuilder;
using WindowsPreview.Kinect;

namespace PatientDesktopClientEngine
{

    // TODO change access modifiers
    class GestureDetector : IBodyDataListener
    {
        // TODO move to some config file...
        private readonly string gestureDatabase = @"C:\Users\Velizar\Desktop\DiscreteGestureBasics-WPF\Database\arm_raise.gbd";
        private readonly string gestureName = "arm_raiseProgress_Right";

        private VisualGestureBuilderFrameSource vgbFrameSource;
        private VisualGestureBuilderFrameReader vgbFrameReader;

        // TODO not considering case when vgbFrameReader may end up being null
        public GestureDetector()
        {
            KinectSensor sensor = KinectSensor.GetDefault();

            if (sensor != null)
            {
                vgbFrameSource = new VisualGestureBuilderFrameSource(sensor, 0);
                vgbFrameReader = vgbFrameSource.OpenReader();
                if (vgbFrameReader != null)
                {
                    vgbFrameReader.IsPaused = true;
                    vgbFrameReader.FrameArrived += reader_GestureFrameArrived;
                }

                //new VisualGestureBuilderDatabase(gestureDatabase);

                //using (VisualGestureBuilderDatabase db = new VisualGestureBuilderDatabase(gestureDatabase))
                //{
                //    foreach (Gesture g in db.AvailableGestures)
                //    {
                //        if (g.Name.Equals(gestureName))
                //        {
                //            vgbFrameSource.AddGesture(g);
                //        }
                //    }
                //}
            }
        }

        public float GestureProgress
        {
            get;
            private set;
        }

        public void BodyDataArrived(System.Collections.Generic.IReadOnlyDictionary<JointType, Joint> joints, ulong bodyId)
        {
            vgbFrameSource.TrackingId = bodyId;
            vgbFrameReader.IsPaused = false;
        }

        public void NoBodyTracked()
        {
            vgbFrameSource.TrackingId = 0;
            vgbFrameReader.IsPaused = true;
        }

        private void reader_GestureFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
        {
            VisualGestureBuilderFrameReference frameReference = e.FrameReference;
            using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame())
            {
                if (frame == null)
                {
                    return;
                }

                IReadOnlyDictionary<Gesture, ContinuousGestureResult> gestureResults = frame.ContinuousGestureResults;
                if (gestureResults != null)
                {
                    foreach (Gesture g in vgbFrameSource.Gestures)
                    {
                        if (g.Name.Equals(gestureName))
                        {
                            ContinuousGestureResult result = null;
                            gestureResults.TryGetValue(g, out result);
                            if (result != null)
                            {
                                GestureProgress = result.Progress;
                            }
                        }
                    }
                }
            }
        }

    }
}
