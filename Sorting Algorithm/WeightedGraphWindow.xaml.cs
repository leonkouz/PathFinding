using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PathFinding
{
    /// <summary>
    /// Interaction logic for WeightedGraphWindow.xaml
    /// </summary>
    public partial class WeightedGraphWindow : Window
    {
        private bool isNewNode = false;

        public static Canvas MainCanvas; 

        public WeightedGraphWindow()
        {
            InitializeComponent();

            MainCanvas = mainCanvas;
        }

        private void Window_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        } 

        private void newNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if(WeightedNodeControl.IsFollowingMouse == false)
            {
                WeightedNodeControl weightedNodeControl = new WeightedNodeControl();
                weightedNodeControl.PreviewMouseLeftButtonDown += WeightedNodeControl_PreviewMouseLeftButtonDown;
                mainCanvas.Children.Add(weightedNodeControl);
                isNewNode = true;
            }
        }

        private void WeightedNodeControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(isNewNode == true)
            {
                WeightedNodeControl nodeControl = sender as WeightedNodeControl;
                WeightedNodeControl newNode = new WeightedNodeControl();

                isNewNode = false;

                LinkingLine line = new LinkingLine(nodeControl, newNode);
                line.X1 = Canvas.GetLeft(nodeControl) + (nodeControl.Width / 2);
                line.Y1 = Canvas.GetTop(nodeControl) + (nodeControl.Height / 2);

                mainCanvas.Children.Add(line);
                mainCanvas.Children.Add(newNode);

                Canvas.SetZIndex(line, 0);
                Canvas.SetZIndex(nodeControl, 1);
                Canvas.SetZIndex(newNode, 1);
            }
        }
    }
}
