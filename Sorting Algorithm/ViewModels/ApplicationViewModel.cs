using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PathFinding
{
    public class ApplicationViewModel
    {
        private Brush defaultNodeColour = Brushes.WhiteSmoke;

        private Grid grid;

        public Node StartNode { get; private set; }

        public Node EndNode { get; private set; }

        public List<Node> Nodes
        {
            get => grid.Nodes.Cast<Node>().ToList();
        }

        public ApplicationViewModel()
        {
            grid = new Grid(25, 25);
        }

        public void SelectStartNode(Node node)
        {
            if (StartNode != null)
            {
                StartNode.Colour = defaultNodeColour;
            }

            StartNode = node;
        }

        private void DeselecStartNode()
        {
            if (StartNode != null)
            {
                StartNode.Colour = defaultNodeColour;
                StartNode = null;
            }
        }

        private void DeselectEndNode()
        {
            if (EndNode != null)
            {
                EndNode.Colour = defaultNodeColour;
                EndNode = null;
            }
        }

        public void SelectEndNode(Node node)
        {
            if (EndNode != null)
            {
                EndNode.Colour = defaultNodeColour;
            }

            EndNode = node;
        }

        public void ClearGrid()
        {
            grid.Clear();
            DeselecStartNode();
            DeselectEndNode();
        }

        private async Task RunAlgorithm(Action function)
        {
            if (StartNode != null && EndNode != null)
            {
                await Task.Run(() =>
                {
                    function.Invoke();
                });
            }
            else
            {
                MessageBox.Show("Both a start node and an end node must be selected.");
                return;
            }
        }

        public async Task RunDijkstra()
        {
            await Task.Run(async () =>
            {
                await RunAlgorithm(() => grid.DijkstrasAlgorithm(StartNode, EndNode));
            });
        }

        public async Task RunAStar()
        {
            await Task.Run(async () =>
            {
                await RunAlgorithm(() => grid.AStarAlgorithm(StartNode, EndNode));
            });
        }
    }
}
