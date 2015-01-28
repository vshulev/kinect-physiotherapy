using System;
using PatientDesktopClient.UI.Scene;
using PatientDesktopClient.Readers;
using PatientDesktopClient.Scoring;

namespace PatientDesktopClient
{
    class App
    {

        public static void Main(string[] args)
        {
            new ExerciseScene().Start();
        }

    }
}
