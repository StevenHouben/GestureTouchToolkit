using System.Windows.Media;
using GestureTouch;

namespace PaintExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var drawingAttributes = new System.Windows.Ink.DrawingAttributes
            {
                Color = Colors.Black,
                Width = 10,
                Height = 10
            };

            Canvas.DefaultDrawingAttributes = drawingAttributes;
            Canvas.UsesTouchShape = false;
            var wrap = new GestureTouchPipeline(Canvas);

            wrap.GestureTouchMove += wrap_GestureTouchMove;
            wrap.GestureTouchDown += wrap_GestureTouchDown;
        }

        void wrap_GestureTouchDown(object sender, GestureTouchEventArgs e)
        {
            
            Title = e.TouchPoint.Size.Width.ToString();
            var drawingAttributes = new System.Windows.Ink.DrawingAttributes();
            if (e.TouchPoint.Size.Width < 6)
            {
                drawingAttributes.Width = drawingAttributes.Height = 5;
                drawingAttributes.Color = Colors.DarkOliveGreen;
            }
            else if (e.TouchPoint.Size.Width > 6 && e.TouchPoint.Size.Width < 14)
            {
                drawingAttributes.Width = drawingAttributes.Height = 15;
                drawingAttributes.Color = Colors.Orange;
            }
            else
            {
                drawingAttributes.Width = drawingAttributes.Height = 30;
                drawingAttributes.Color = Colors.Red;
            }
            Canvas.DefaultDrawingAttributes = drawingAttributes;
            Canvas.ReleaseStylusCapture();
        }

        void wrap_GestureTouchMove(object sender, GestureTouchEventArgs e)
        {
            Title = e.TouchPoint.Size.Width.ToString();
            var drawingAttributes = new System.Windows.Ink.DrawingAttributes();
            if (e.TouchPoint.Size.Width < 6)
            {
                drawingAttributes.Width = drawingAttributes.Height = 5;
                drawingAttributes.Color = Colors.DarkOliveGreen;
            }
            else if (e.TouchPoint.Size.Width > 6 && e.TouchPoint.Size.Width < 14)
            {
                drawingAttributes.Width = drawingAttributes.Height = 15;
                drawingAttributes.Color = Colors.Orange;
            }
            else
            {
                drawingAttributes.Width = drawingAttributes.Height = 30;
                drawingAttributes.Color = Colors.Red;
            }
            Canvas.DefaultDrawingAttributes = drawingAttributes;
            Canvas.ReleaseStylusCapture();
        }
    }
}
