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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ApplicationViewModel viewModel = new ApplicationViewModel();

        public MainWindow()
        {
            this.DataContext = viewModel;

            InitializeComponent();

        }

        private void node_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(sender != null)
            {
                Node node = (sender as NodeControl).Node;
                node.Colour = Brushes.White;

                Grid.Items.Refresh();
            }
        }
    }
}
