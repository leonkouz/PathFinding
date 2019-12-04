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
    /// Interaction logic for StartNode.xaml
    /// </summary>
    public partial class StartNode : UserControl
    {
        public static bool IsDragging = false;

        public StartNode()
        {
            InitializeComponent();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            IsDragging = true;
        }
    }
}

