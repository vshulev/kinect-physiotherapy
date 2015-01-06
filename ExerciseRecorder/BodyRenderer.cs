using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ExerciseRecorder
{
    class BodyRenderer
    {

        private Canvas canvas;

        public BodyRenderer(double x, double y, double width, double height)
        {
            canvas = new Canvas();

            canvas.Clip = new RectangleGeometry();
            canvas.Clip.Rect = new Rect(x, y, width, height);
        }

    }
}
