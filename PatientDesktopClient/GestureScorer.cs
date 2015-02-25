using System;
using System.Collections.Generic;

namespace PatientDesktopClient
{

    delegate void GestureScoreUpdatedHandler(object source, GestureScoreUpdatedEventArgs e);

    class GestureScorer
    {
        public static readonly GestureScorer Instance = new GestureScorer();
        public event GestureScoreUpdatedHandler GestureScoreUpdated;
        private List<float> results;

        private GestureScorer()
        {
            GestureReader.Instance.GestureResultChanged += gestureResultChanged;
            results = new List<float>();

            //scorers = new List<IScorer>();
            //scorers.Add(new FluidityScorer());
        }

        private void gestureResultChanged(object source, GestureResultChangedEventArgs e)
        {
            if (GestureScoreUpdated != null)
            {
                GestureScoreUpdated(this, new GestureScoreUpdatedEventArgs(0));
            }
        }
    }

    class GestureScoreUpdatedEventArgs : EventArgs
    {

        public float Score;

        public GestureScoreUpdatedEventArgs(float score)
        {
            Score = score;
        }

    }
}