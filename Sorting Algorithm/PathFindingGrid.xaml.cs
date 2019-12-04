using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for SortingGrid.xaml
    /// </summary>
    public partial class PathFindingGrid : UserControl
    {
        public PathFindingGrid()
        {
            InitializeComponent();
        }

        GridNode[,] grid;

        private void StackPanel_Loaded(object sender, RoutedEventArgs e)
        {
            int numberOfRows = (int)(mainStackPanel.Height / 50);
            int numberOfColumns = (int)(mainStackPanel.Width / 50);

            grid = new GridNode[numberOfRows, numberOfColumns];

            for (int i = 0; i < numberOfRows; i++)
            {
                StackPanel panel = new StackPanel();
                panel.Orientation = Orientation.Horizontal;
                panel.HorizontalAlignment = HorizontalAlignment.Left;

                for (int j = 0; j < numberOfColumns; j++)
                {
                    GridNode node = new GridNode();
                    panel.Children.Add(node);
                    grid[i, j] = node;
                }

                mainStackPanel.Children.Add(panel);
            }

            CreateStartEndNodes();
        }

        private void CreateStartEndNodes()
        {
            grid[3, 3].NodeContent = new StartNode();
            grid[6, 6].NodeContent = new EndNode();
        }
    }
}
