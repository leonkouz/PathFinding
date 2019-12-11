using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathFinding
{
    public class ApplicationViewModel
    {
        private Grid grid;

        public Node StartNode { get; private set; }

        public Node EndNode { get; private set; }

        public List<Node> Nodes
        {
            get => grid.Nodes.Cast<Node>().ToList();
        }

        public ApplicationViewModel()
        {
            grid = new Grid(10, 10);
        }

        public void SelectStartNode(Node node)
        {
            if(StartNode != null)
            {
                StartNode.Colour = Brushes.Khaki;
            }

            StartNode = node;
        }

        public void SelectEndNode(Node node)
        {
            if (EndNode != null)
            {
                EndNode.Colour = Brushes.Khaki;
            }

            EndNode = node;
        }

        public void RunDijkstra()
        {

            if (StartNode != null || EndNode != null)
            {
                Task.Run(() =>
                {
                    grid.DijkstrasAlgorithm(StartNode, EndNode);
                });
            }
        }
    }
}
