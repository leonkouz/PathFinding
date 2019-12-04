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

namespace PathFinding
{
    /// <summary>
    /// Interaction logic for GridNode.xaml
    /// </summary>
    public partial class GridNode : UserControl
    {
        private UIElement nodeContent;

        public List<GridNode> nodes = new List<GridNode>();

        private bool isClearing = false;

        public UIElement NodeContent
        {
            get => nodeContent;
            set
            {
                nodeContent = value;
                UpdateNodeContent();
            }
        }

        public GridNode()
        {
            InitializeComponent();
        }

        private void UpdateNodeContent()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                content.Content = nodeContent;
            });
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (StartNode.IsDragging)
            {
                content.Content = new StartNode();
                nodes.Add((GridNode)(sender as Border).Parent);
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (StartNode.IsDragging)
            {
                content.Content = null;
            }
        }

        private async void Border_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (StartNode.IsDragging)
            {
                content.Content = new StartNode();
            }

            if (isClearing == false && StartNode.IsDragging)
            {
                isClearing = true;

                await Task.Run(() =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        Parallel.ForEach(nodes, node =>
                        {
                            if (node != sender && node != null)
                            {
                                node.NodeContent = null;
                            }
                        });

                        isClearing = false;
                    });
                });
            }

        }
    }
}
