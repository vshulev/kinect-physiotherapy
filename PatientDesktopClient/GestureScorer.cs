using System;
using System.Linq;
using System.Collections.Generic;

namespace PatientDesktopClient
{

    delegate void GestureScoreUpdatedHandler(object source, GestureScoreUpdatedEventArgs e);

    class GestureScorer
    {
        private static readonly float ERROR_MARGIN = 0.2f;

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

        // fired after a repetition is completed.
        private void gestureResultChanged(object source, GestureResultChangedEventArgs e)
        {
            results.Add(e.Result);

            if (e.Result <= ERROR_MARGIN && results.Count != 1 && results[results.Count - 2] > ERROR_MARGIN)
            {
                // compute progress (i.e. find max value in list)

                // compute fluidity (i.e. find std. dev of values ERROR_MARGIN apart)

                // compute speed (i.e. change between values ERROR_MARGIN(?) apart)

                // TODO compute posture - need to know discrete gesture confidence

                if (GestureScoreUpdated != null)
                {
                    GestureScoreUpdated(this, new GestureScoreUpdatedEventArgs() {
                        isProgressGood = computeProgress(),
                        isFluidityGood = computeFluidity(),
                        isSpeedGood = computeSpeed(),
                        fluidity = fl
                    });
                }

                results = new List<float>();
            }

        }

        private bool computeProgress()
        {
            float max = 0;
            foreach (float val in results)
                if (val > max)
                    max = val;

            return max >= 1 - ERROR_MARGIN;
        }

        private float fl;

        private bool computeFluidity()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Velizar\Desktop\kinect_outp.txt", true))
            {
                foreach (float res in results)
                        file.WriteLine(res);
                file.WriteLine();
                file.WriteLine();
            }

            List<float> speeds = new List<float>();
            for (int i = 0; i < results.Count - 1; i++) {
                var v = results[i] - results[i + 1];
                v *= 100;
                v = (float)Math.Pow((double)v, 2);
                speeds.Add(v);
            }

            float avg = speeds.Average();
            float variance = speeds.Sum(s => (float)Math.Pow(s - avg, 2)) / speeds.Count;
            float stdDev = (float)Math.Sqrt(variance);
            fl = stdDev;
            return stdDev >= 0.25f; // TODO value is pretty arbitrary... need to know more...
        }

        private bool computeSpeed()
        {
            return true;
        }
            
    }

    class GestureScoreUpdatedEventArgs : EventArgs
    {

        public bool isProgressGood;
        public bool isFluidityGood;
        public bool isSpeedGood;
        public float fluidity;

        public GestureScoreUpdatedEventArgs() { }

    }
}