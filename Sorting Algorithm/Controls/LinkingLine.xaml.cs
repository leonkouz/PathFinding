using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PathFinding
{
    /// <summary>
    /// Interaction logic for LinkingLine.xaml
    /// </summary>
    public partial class LinkingLine : UserControl
    {
        private Timer timer = new Timer();

        private WeightedNodeControl source;

        private WeightedNodeControl destination;

        public bool IsFollowingMouse { get; private set; } = false;

        public double X1
        {
            get { return (double)GetValue(X1Property); }
            set
            {
                SetValue(X1Property, value);
            }
        }

        public double X2
        {
            get { return (double)GetValue(X2Property); }
            set
            {
                SetValue(X2Property, value);
            }
        }

        public double Y1
        {
            get { return (double)GetValue(Y1Property); }
            set
            {
                SetValue(Y1Property, value);
            }
        }

        public double Y2
        {
            get { return (double)GetValue(Y2Property); }
            set
            {
                SetValue(Y2Property, value);
            }
        }

        #region Dependency Properties

        public static readonly DependencyProperty Y2Property =
            DependencyProperty.Register("Y2", typeof(double),
       typeof(LinkingLine), new PropertyMetadata(null));

        public static readonly DependencyProperty X1Property =
           DependencyProperty.Register("X1", typeof(double),
             typeof(LinkingLine), new PropertyMetadata(null));


        public static readonly DependencyProperty X2Property =
            DependencyProperty.Register("X2", typeof(double),
              typeof(LinkingLine), new PropertyMetadata(null));


        public static readonly DependencyProperty Y1Property =
            DependencyProperty.Register("Y1", typeof(double),
              typeof(LinkingLine), new PropertyMetadata(null));


        #endregion

        public LinkingLine(WeightedNodeControl source, WeightedNodeControl destination)
        {
            InitializeComponent();

            this.source = source;
            this.destination = destination;

            destination.PreviewMouseLeftButtonDown += Destination_PreviewMouseLeftButtonDown;

            timer.Interval = 5;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void Destination_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StopFollowingMouse();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            FollowMouse();
        }

        private void FollowMouse()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            IsFollowingMouse = true;
        }

        private void StopFollowingMouse()
        {
            timer.Elapsed -= Timer_Elapsed;
            timer.Stop();
            IsFollowingMouse = false;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Point mousePosition = Mouse.GetPosition(WeightedGraphWindow.MainCanvas);

                    X2 = mousePosition.X; 
                    Y2 = mousePosition.Y;
                });
            }
        }
    }
}
