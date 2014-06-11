using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GestureTouch;

namespace MenuInteractionExample
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var wrap = new GestureTouchPipeline(Button);
            wrap.GestureTouchMove += wrap_GestureTouchMove;
            wrap.GestureTouchUp += wrap_GestureTouchUp;
        }

        void wrap_GestureTouchMove(object sender, GestureTouchEventArgs e)
        {
            if ((e.TouchPoint.Size.Width <= 6))
            {
                Box.Text = PasswordBox.Password;
                Box.Visibility = Visibility.Visible;
                PasswordBox.Visibility = Visibility.Hidden;
                Button.Background = Output.Foreground = Brushes.Orange;
                Output.Content = "Password Revealed!";
            }
            else if (e.TouchPoint.Size.Width > 6 && e.TouchPoint.Size.Width <= 12)
            {
                if (PasswordBox.Password != "qwertyu")
                {
                    Button.Background = Output.Foreground = Brushes.DarkRed;
                    Output.Content = "Incorrect Password";
                }
                else
                {
                    Button.Background = Output.Foreground = Brushes.DarkOliveGreen;
                    Output.Content = "Password Accepted";
                }
            }
            else
            {
                PasswordBox.Password = "";
                Button.Background = Output.Foreground = Brushes.Gray;
                Output.Content = "Password Reset";
            }
        }

        void wrap_GestureTouchUp(object sender, GestureTouchEventArgs e)
        {
            PasswordBox.PasswordChar = '*';
            Box.Text = "";
            Box.Visibility = Visibility.Hidden;
            PasswordBox.Visibility = Visibility.Visible;
            Button.Background = Brushes.Gray;
            Output.Content = "";
        }

        private void PasswordBox_OnPreviewTouchDown(object sender, TouchEventArgs e)
        {
            Process.Start(@"C:\Program Files\Common Files\Microsoft Shared\ink\TabTip.exe");
        }
    }
}
