using System;
using System.Linq;
using System.Collections.Generic;

namespace PatientDesktopClient.Scoring
{
    class FluidityScorer : IScorer
    {

        public float Score(IReadOnlyList<float> progressValues)
        {
            if (progressValues.Count < 2)
                return 0;

            //// create a list of speeds
            //List<float> speeds = new List<float>();
            //for (int i = 0; i < progressValues.Count - 1; i++)
            //    speeds.Add(Math.Abs(progressValues[i] - progressValues[i + 1]));

            //// compute standard deviation
            //float avg = speeds.Average();
            //float variance = speeds.Sum(s => (float) Math.Pow(s - avg, 2)) / speeds.Count;
            //return (float) Math.Sqrt(variance);

            float avg = progressValues.Average();
            float variance = progressValues.Sum(s => (float)Math.Pow(s - avg, 2)) / progressValues.Count;
            return (float)Math.Sqrt(variance);
        
        }

    }
}
