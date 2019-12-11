using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinding
{
    public class ApplicationViewModel
    {
        private Grid grid;

        public List<Node> Nodes
        {
            get => grid.Nodes.Cast<Node>().ToList();
        }

        public ApplicationViewModel()
        {
            grid = new Grid(100, 100);
        }

        public void SelectNode(Node node)
        {
        }

        public void RunDijkstra()
        {
            grid.DijkstrasAlgorithm(grid.Nodes[51, 51], grid.Nodes[56, 56]);
        }
    }
}
