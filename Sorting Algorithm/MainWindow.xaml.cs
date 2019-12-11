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
using System.Windows.Navigation;
using System.Windows.Shapes;


/// <summary>
/// TODO:
/// 
/// Dijkstra's using min-priority queue
/// Dijkstra's using Fibonacci heap to optimise 
/// 
/// </summary>


namespace PathFinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationViewModel viewModel = new ApplicationViewModel();

        private bool isStartNodeSelecting = false;
        private bool isEndNodeSelecting = false;

        public MainWindow()
        {
            this.DataContext = viewModel;

            InitializeComponent();
        }

        private void Node_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Task.Run(() =>
            {
                viewModel.RunDijkstra();
            });
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Grid.Items.Refresh();
        }

        private void SelectStartNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if(isEndNodeSelecting == false)
            {
                isStartNodeSelecting = true;
                selectStartNodeButton.IsEnabled = false;
            }
        }

        private void SelectEndNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if(isStartNodeSelecting == false)
            {
                isEndNodeSelecting = true;
                selectEndNodeButton.IsEnabled = false;
            }
        }

        private void RunDijkstraButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RunDijkstra();
        }

        private void Node_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(isEndNodeSelecting == true)
            {
                Node node = (sender as NodeControl).Node;

                isEndNodeSelecting = false;
                selectEndNodeButton.IsEnabled = true;
                viewModel.SelectEndNode(node);
                node.Colour = Brushes.Yellow;
            }

            if(isStartNodeSelecting == true)
            {
                Node node = (sender as NodeControl).Node;

                isStartNodeSelecting = false;
                selectStartNodeButton.IsEnabled = true;
                viewModel.SelectStartNode(node);
                node.Colour = Brushes.Blue;
            }
        }
    }
}
