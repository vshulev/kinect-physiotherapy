using System.Collections.Generic;

namespace PatientDesktopClient.Scoring
{
    interface IScorer
    {

        float Score(IReadOnlyList<float> progressValues);

    }
}
