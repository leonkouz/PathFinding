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
    /// Interaction logic for GraphSelection.xaml
    /// </summary>
    public partial class GraphSelectionWindow : Window
    {
        public GraphSelectionWindow()
        {
            InitializeComponent();
        }

        private void weightedGraphButton_Click(object sender, RoutedEventArgs e)
        {
            WeightedGraphWindow window = new WeightedGraphWindow();
            window.Show();

            this.Close();
        }

        private void unwieghtedGraphButton_Click(object sender, RoutedEventArgs e)
        {
            UnweightedGraphWindow window = new UnweightedGraphWindow();
            window.Show();

            this.Close();
        }
    }
}
