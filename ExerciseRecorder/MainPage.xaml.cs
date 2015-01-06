using System.ComponentModel;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
//using Microsoft.Speech.Recognition;
//using Microsoft.Speech.AudioFormat;
using System.Collections.Generic;
using System;

using PatientDesktopClientEngine;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
namespace ExerciseRecorder
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Canvas canvas;
        //private SpeechRecognitionEngine speechEngine;

        public MainPage()
        {
            this.InitializeComponent();

            //// create a canvas and add an ellipse to it
            //canvas = new Canvas();
            //canvas.Clip = new RectangleGeometry();
            //canvas.Clip.Rect = new Rect(0.0, 0.0, this.DisplayGrid.Width, DisplayGrid.Height);

            //this.DisplayGrid.Children.Add(canvas);

            //this.canvas.Children.Add(new Ellipse()
            //{
            //    Visibility = Windows.UI.Xaml.Visibility.Visible,
            //    Fill = new SolidColorBrush(Colors.Red),
            //    Width = 8.0,
            //    Height = 8.0,
            //});

            //// do some speech recognition stuff!
            //IEnumerable<RecognizerInfo> recognizers = SpeechRecognitionEngine.InstalledRecognizers();
            //RecognizerInfo ri = null;

            //foreach (RecognizerInfo recognizer in recognizers)
            //{
            //    string value;
            //    recognizer.AdditionalInfo.TryGetValue("Kinect", out value);
            //    if ("True".Equals(value, StringComparison.OrdinalIgnoreCase) && "en-US".Equals(recognizer.Culture.Name, StringComparison.OrdinalIgnoreCase))
            //    {
            //        ri = recognizer;
            //    }
            //}

            //if (ri == null)
            //{
            //    return;
            //}

            //speechEngine = new SpeechRecognitionEngine(ri.Id);

            //Choices words = new Choices();
            //words.Add(new SemanticResultValue("start", "START"));
            //words.Add(new SemanticResultValue("stop", "STOP"));
            //GrammarBuilder gb = new GrammarBuilder { Culture = ri.Culture };
            //gb.Append(words);

            //Grammar g = new Grammar(gb);

            //this.speechEngine.SpeechRecognized += this.SpeechRecognized;
            //this.speechEngine.SpeechRecognitionRejected += this.SpeechRejected;
            //this.speechEngine.SetInputToAudioStream(
            //        new AudioStream(), new SpeechAudioFormatInfo(EncodingFormat.Pcm, 16000, 16, 1, 32000, 2, null));
            //this.speechEngine.RecognizeAsync(RecognizeMode.Multiple);

        }

        //private void SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        //{
        //    // TODO stub
        //}

        //private void SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        //{
        //    // TODO stub
        //}

    }
}
