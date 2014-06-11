GestureTouchToolkit
===================
The GestureTouchToolkit is a (work in progress) toolkit for WPF that detects the size of
the touch input on a standard capacitive touchscreen by exploiting either the Stylus
API (GestureTouch.WPF) or by hooking into the native events send by the touchscreen
(GestureTouch.Native).

Because the window handle can only be attached to one touch handler (limitation of
Windows), you can only use the integrated WPF detector or the Native detector. Note
that the WPF integration is slower than the Native, but using the Native disables
all build in touch support for WPF, meaning you have to do your own UIElement
touch detections etc. I would only recommend using the Native toolkit when drawing
your own User Interface.


GestureTouch.WPF
===================
The WPF toolkit supports i) full window support by deriving your WPF window from TouchWindow or ii) per-UI element support by wrapping any UI element in the GestureTouchPipeline, which gives the user access to the gesture touch events for that UI element.

## TouchWindow
```
public class TestWindow : TouchWindow
    {
        public TestWindow()
        {
            GestureTouchDown += TestWindow_GestureTouchDown;
            GestureTouchMove += TestWindow_GestureTouchMove;
            GestureTouchUp += TestWindow_GestureTouchUp;
        }

        void TestWindow_GestureTouchUp(object sender, GestureTouchEventArgs e)
        {
            var touchPoint = e.TouchPoint;
            Console.WriteLine(
                "Width:{0} Height:{1} X:{2} Y:{3}", 
                touchPoint.Size.Width, 
                touchPoint.Size.Height,
                touchPoint.Position.X,
                touchPoint.Position.Y);
        }

        void TestWindow_GestureTouchMove(object sender, GestureTouchEventArgs e)
        {
            var touchPoint = e.TouchPoint;
            Console.WriteLine(
                "Width:{0} Height:{1} X:{2} Y:{3}",
                touchPoint.Size.Width,
                touchPoint.Size.Height,
                touchPoint.Position.X,
                touchPoint.Position.Y);
        }

        void TestWindow_GestureTouchDown(object sender, GestureTouchEventArgs e)
        {
            var touchPoint = e.TouchPoint;
            Console.WriteLine(
                "Width:{0} Height:{1} X:{2} Y:{3}",
                touchPoint.Size.Width,
                touchPoint.Size.Height,
                touchPoint.Position.X,
                touchPoint.Position.Y);
        }
    }
```
## GestureTouchPipeline
```
 public class TestWindow : Window
    {
        readonly Button _button= new Button();
        public TestWindow()
        {

            var pipeline = new GestureTouchPipeline(_button);

            pipeline.GestureTouchDown += button_GestureTouchDown;
            pipeline.GestureTouchMove += button_GestureTouchMove;
            pipeline.GestureTouchUp += button_GestureTouchUp;
        }

        void button_GestureTouchUp(object sender, GestureTouchEventArgs e)
        {
            var touchPoint = e.TouchPoint;
            Console.WriteLine(
                "Width:{0} Height:{1} X:{2} Y:{3}",
                touchPoint.Size.Width,
                touchPoint.Size.Height,
                touchPoint.Position.X,
                touchPoint.Position.Y);
        }

        void button_GestureTouchMove(object sender, GestureTouchEventArgs e)
        {
            var touchPoint = e.TouchPoint;
            Console.WriteLine(
                "Width:{0} Height:{1} X:{2} Y:{3}",
                touchPoint.Size.Width,
                touchPoint.Size.Height,
                touchPoint.Position.X,
                touchPoint.Position.Y);
        }

        void button_GestureTouchDown(object sender, GestureTouchEventArgs e)
        {
            var touchPoint = e.TouchPoint;
            Console.WriteLine(
                "Width:{0} Height:{1} X:{2} Y:{3}",
                touchPoint.Size.Width,
                touchPoint.Size.Height,
                touchPoint.Position.X,
                touchPoint.Position.Y);
        }
    }

```


GestureTouch.Native
===================
The native toolkit only gives access to the window touches. Since in this approach, all default WPF touch events are disabled, the developer has to manage UI interaction themselves.

```
public class TestWindow : NativeTouchWindow
{
	public TestWindow()
	{
		NativeTouchDown += TestWindow_NativeTouchDown;
		NativeTouchUp += TestWindow_NativeTouchUp;
		NativeTouchMove += TestWindow_NativeTouchMove;
	}

	void TestWindow_NativeTouchMove(object sender, NativeTouchEventArgs e)
	{
		Console.WriteLine("Width:{0} Height:{1}",e.Width,e.Height);
	}

	void TestWindow_NativeTouchUp(object sender, NativeTouchEventArgs e)
	{
		Console.WriteLine("Width:{0} Height:{1}", e.Width, e.Height);
	}

	void TestWindow_NativeTouchDown(object sender, NativeTouchEventArgs e)
	{
		Console.WriteLine("Width:{0} Height:{1}", e.Width, e.Height);
	}
}

```
 
