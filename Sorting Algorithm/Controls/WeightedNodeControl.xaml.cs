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
    /// Interaction logic for WeightedNodeControl.xaml
    /// </summary>
    public partial class WeightedNodeControl : UserControl
    {
        private Timer timer = new Timer();

        private bool isFirstLoad = true;

        private int number;

        private bool wasContextMenuOptionSelected = false;

        public static bool IsFollowingMouse { get; private set; } = false;

        public static List<WeightedNodeControl> ActiveNodes = new List<WeightedNodeControl>();

        public static int NumberOfNodes = 0;


        public WeightedNodeControl()
        {
            InitializeComponent();

            this.numberTextBlock.Text = NumberOfNodes.ToString();
            this.number = NumberOfNodes;
            NumberOfNodes++;

            ActiveNodes.Add(this);

            timer.Interval = 5;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(isFirstLoad == true)
            {
                isFirstLoad = false;
                FollowMouse();
            }
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

                    Canvas.SetLeft(this, mousePosition.X - (this.Width / 2));
                    Canvas.SetTop(this, mousePosition.Y - (this.Height / 2));
                });
            }
        }

        private void UserControl_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsFollowingMouse == true)
            {
                StopFollowingMouse();
            }
            else
            {
                FollowMouse();
            }
        }

        private void contextMenuDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFollowingMouse)
            {
                StopFollowingMouse();
            }

            ((Panel)this.Parent).Children.Remove(this);
            wasContextMenuOptionSelected = true;
        }

        private void UserControl_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            ActiveNodes.Remove(this);

            StopFollowingMouse();
            contextMenu.PlacementTarget = this;
            contextMenu.IsOpen = true;
            wasContextMenuOptionSelected = false;
        }

        private void contextMenu_Closed(object sender, RoutedEventArgs e)
        {
            if(wasContextMenuOptionSelected == false)
            {
                FollowMouse();
            }
        }

        private void contextMenuNewPathButton_Click(object sender, RoutedEventArgs e)
        {
            WeightedNodeControl newNode = new WeightedNodeControl();
            Canvas mainCanvas = (Canvas)this.Parent;

            LinkingLine line = new LinkingLine(this, newNode);
            line.X1 = Canvas.GetLeft(this) + (this.Width / 2);
            line.Y1 = Canvas.GetTop(this) + (this.Height / 2);

            mainCanvas.Children.Add(line);
            mainCanvas.Children.Add(newNode);

            Canvas.SetZIndex(line, 0);
            Canvas.SetZIndex(newNode, 1);

            wasContextMenuOptionSelected = true;
        }
    }
}
