using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathFinding
{
    public class Node
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public Brush Colour { get; set; } = Brushes.Blue;

        public List<Node> Neighbours => GetNeighbours();

        public Node NorthNeightbour { get; private set; }

        public Node EastNeightbour { get; private set; }

        public Node WestNeightbour { get; private set; }

        public Node SouthNeightbour { get; private set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetNorthNeighhbour(Node node)
        {
            NorthNeightbour = node;
        }

        public void SetEastNeighhbour(Node node)
        {
            EastNeightbour = node;
        }

        public void SetWestNeighhbour(Node node)
        {
            WestNeightbour = node;
        }

        public void SetSouthNeighhbour(Node node)
        {
            SouthNeightbour = node;
        }

        private List<Node> GetNeighbours()
        {
            List<Node> connectedNodes = new List<Node>();

            if(NorthNeightbour != null)
            {
                connectedNodes.Add(NorthNeightbour);
            }
            if(EastNeightbour != null)
            {
                connectedNodes.Add(EastNeightbour);
            }
            if(WestNeightbour != null)
            {
                connectedNodes.Add(WestNeightbour);
            }
            if(SouthNeightbour != null)
            {
                connectedNodes.Add(SouthNeightbour);
            }

            return connectedNodes;
        }
    }
}

