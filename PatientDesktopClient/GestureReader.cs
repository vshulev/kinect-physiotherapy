using Microsoft.Kinect;
using Microsoft.Kinect.VisualGestureBuilder;
using System;
using System.Collections.Generic;

namespace PatientDesktopClient
{
    delegate void GestureResultChangedHandler(object source, GestureResultChangedEventArgs e);

    sealed class GestureReader
    {

        private static readonly string GESTURE_DB = @"exercises\arm_abduction\arm_abduction.gbd";
        private static readonly string GESTURE_NAME = "arm_raiseProgress_Right";

        public static readonly GestureReader Instance = new GestureReader();
        public event GestureResultChangedHandler GestureResultChanged;

        private VisualGestureBuilderFrameSource vgbFrameSource;
        private VisualGestureBuilderFrameReader vgbFrameReader;

        private GestureReader()
        {
            setupFrameSource();
            readDb();
        }

        public void Track(ulong id)
        {
            vgbFrameSource.TrackingId = id;
            vgbFrameReader.IsPaused = (id == 0);
        }

        private void setupFrameSource()
        {
            vgbFrameSource = new VisualGestureBuilderFrameSource(KinectSensor.GetDefault(), 0);
            vgbFrameReader = vgbFrameSource.OpenReader();
            if (vgbFrameReader != null)
            {
                vgbFrameReader.IsPaused = true;
                vgbFrameReader.FrameArrived += gestureFrameArrived;
            }
        }

        private void readDb()
        {
            using (VisualGestureBuilderDatabase db = new VisualGestureBuilderDatabase(GESTURE_DB))
            {
                foreach (Gesture g in db.AvailableGestures)
                {
                    //if (g.Name.Equals(continuousGestureName))
                    //{
                    vgbFrameSource.AddGesture(g);
                    //}
                }
            }
        }

        private void gestureFrameArrived(object sender, VisualGestureBuilderFrameArrivedEventArgs e)
        {
            if (GestureResultChanged == null)
                return;

            VisualGestureBuilderFrameReference frameReference = e.FrameReference;
            using (VisualGestureBuilderFrame frame = frameReference.AcquireFrame())
            {
                if (frame == null)
                    return;

                IReadOnlyDictionary<Gesture, ContinuousGestureResult> continuousGestureResults = frame.ContinuousGestureResults;
                if (continuousGestureResults != null)
                {
                    foreach (Gesture g in vgbFrameSource.Gestures)
                    {
                        if (g.Name.Equals(GESTURE_NAME))
                        {
                            ContinuousGestureResult result = null;
                            continuousGestureResults.TryGetValue(g, out result);
                            GestureResultChanged(this, new GestureResultChangedEventArgs(result.Progress));
                        }
                    }
                }
            }
        }
    }

    class GestureResultChangedEventArgs : EventArgs
    {

        public float Result;

        public GestureResultChangedEventArgs(float result)
        {
            Result = result;
        }

    }

}