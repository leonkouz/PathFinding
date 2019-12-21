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

        private List<Control> interactiveControls = new List<Control>();

        public MainWindow()
        {
            this.DataContext = viewModel;

            InitializeComponent();

            interactiveControls.AddRange(new List<Control>() { selectEndNodeButton, selectStartNodeButton, runDijkstraButton, clearButton });
        }

        private void DisableInteractiveButtons()
        {
            foreach(Control control in interactiveControls)
            {
                control.IsEnabled = false;
            }
        }

        private void EnableInteractiveButtons()
        {
            foreach (Control control in interactiveControls)
            {
                control.IsEnabled = true;
            }
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
                DisableInteractiveButtons();
            }
        }

        private void SelectEndNodeButton_Click(object sender, RoutedEventArgs e)
        {
            if(isStartNodeSelecting == false)
            {
                isEndNodeSelecting = true;
                DisableInteractiveButtons();
            }
        }

        private async void RunDijkstraButton_Click(object sender, RoutedEventArgs e)
        {
            DisableInteractiveButtons();

            await viewModel.RunDijkstra();

            EnableInteractiveButtons();
        }

        private void Node_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Node node = (sender as NodeControl).Node;

            if (isEndNodeSelecting == true)
            {
                isEndNodeSelecting = false;
                EnableInteractiveButtons();
                viewModel.SelectEndNode(node);
                node.Colour = Brushes.Red;
            }
            else if(isStartNodeSelecting == true)
            {
                isStartNodeSelecting = false;
                EnableInteractiveButtons();
                viewModel.SelectStartNode(node);
                node.Colour = Brushes.Green;
            }
            else
            {
                if (node.IsWall)
                {
                    node.RemoveWall();
                }
                else
                {
                    node.MakeWall();
                }
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearGrid();
        }
    }
}
