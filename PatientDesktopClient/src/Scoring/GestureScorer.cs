using System;
using System.Collections.Generic;
using PatientDesktopClient.Readers;

namespace PatientDesktopClient.Scoring
{

    delegate void GestureScoreUpdatedHandler(object source, GestureScoreUpdatedEventArgs e);

    class GestureScorer
    {
        private static readonly float ERROR_MARGIN = 0.1f;

        public static readonly GestureScorer Instance = new GestureScorer();
        
        public event GestureScoreUpdatedHandler GestureScoreUpdated;

        private List<IScorer> scorers;
        private List<float> results;

        // TODO TEMP
        private float lastScore = 0;
        private float totalScore = 0;
        private int numScores = 0;

        private GestureScorer()
        {
            GestureReader.Instance.GestureResultChanged += gestureResultChanged;
            results = new List<float>();

            scorers = new List<IScorer>();
            scorers.Add(new FluidityScorer());
        }

        private void gestureResultChanged(object source, GestureResultChangedEventArgs e)
        {
            if (e.Result <= ERROR_MARGIN)
            {
                if (results.Count > 1)
                {
                    Console.WriteLine(lastScore);

                    //for (float i = 0; i < 1.1f && i < lastScore; i += 0.1f)
                    //{
                    //    Console.Write("=");
                    //}
                    //Console.WriteLine("");
                    lastScore = 0;
                    totalScore = 0;
                    numScores = 0;
                }
                results = new List<float>();
            }
            results.Add(e.Result);

            if (GestureScoreUpdated != null)
            {
                float score = scorers[0].Score(results);

                //for (float i = 0; i < 1.1f && i < score; i += 0.1f)
                //{
                //    Console.Write("=");
                //}
                //Console.WriteLine("");
                
                if (!float.IsNaN(score))
                {
                    lastScore = score;
                    totalScore += score;
                    numScores++;
                }
                GestureScoreUpdated(this, new GestureScoreUpdatedEventArgs(score));
            }
        }
    }

    class GestureScoreUpdatedEventArgs : EventArgs {

        public float Score;

        public GestureScoreUpdatedEventArgs(float score)
        {
            Score = score;
        }

    }
}
