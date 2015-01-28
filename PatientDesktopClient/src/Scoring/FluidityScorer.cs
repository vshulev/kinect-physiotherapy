using System;
using System.Collections.Generic;

namespace PatientDesktopClient.Scoring
{
    class FluidityScorer : IScorer
    {

        public float Score(IReadOnlyList<float> progressValues)
        {
            // create a list of speeds
            List<float> speeds = new List<float>();
            for (int i = 0; i < progressValues.Count - 1; i++)
                speeds.Add(Math.Abs(progressValues[i] - progressValues[i + 1]));

            // compute standard deviation
            float mean = Mean(speeds);
            float sum = 0;
            foreach (float speed in speeds)
                sum += (float)Math.Pow(speed - mean, 2);

            return sum / speeds.Count;
            //float standardDeviation = (float)Math.Sqrt(sum / speeds.Count);

            //// compute worst possible standard deviation
            //float a = Math.Min(Math.Abs(0 - mean), Math.Abs(1 - mean));
            //sum = (float)(2 * (Math.Pow(a, 2)));
            //float worstStandardDeviation = (float)Math.Sqrt(sum / 2);

            //return 1 - standardDeviation / worstStandardDeviation;
        }

        private float Mean(List<float> values)
        {
            float total = 0;
            foreach (float val in values)
                total += val;
            return total / values.Count;
        }
    }
}
