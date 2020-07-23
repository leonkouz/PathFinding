using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace PathFinding
{
    public class UnweightedGraphViewModel
    {
        private Brush defaultNodeColour = Brushes.WhiteSmoke;

        private Grid grid;

        public Node StartNode { get; private set; }

        public Node EndNode { get; private set; }

        public List<Node> Nodes
        {
            get => grid.Nodes.Cast<Node>().ToList();
        }

        public UnweightedGraphViewModel()
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
            StartNode.IsStartNode = true;
            StartNode.Colour = Brushes.Green;
        }

        private void DeselecStartNode()
        {
            if (StartNode != null)
            {
                StartNode.Colour = defaultNodeColour;
                StartNode.IsStartNode = false;
                StartNode = null;
            }
        }

        private void DeselectEndNode()
        {
            if (EndNode != null)
            {
                EndNode.Colour = defaultNodeColour;
                EndNode.IsEndNode = false;
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
            EndNode.IsStartNode = true;
            EndNode.Colour = Brushes.Red;
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
