using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathFinding
{
    public class Node : OnPropertyChangedBehaviour
    {
        private Brush colour = Brushes.Blue;

        public int X { get; private set; }

        public int Y { get; private set; }

        public Brush Colour
        {
            get => colour;
            set
            {
                colour = value;
                OnPropertyChanged(nameof(Colour));
            }
        }

        public List<Node> Neighbours => GetNeighbours();

        public Node NorthWestNeighbour { get; private set; }

        public Node NorthNeightbour { get; private set; }

        public Node NorthEastNeighbour { get; private set; }

        public Node EastNeightbour { get; private set; }

        public Node SouthEastNeighbour { get; private set; }

        public Node SouthNeightbour { get; private set; }

        public Node SouthWestNeighbour { get; private set; }

        public Node WestNeightbour { get; private set; }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void SetNorthWestNeighbour(Node node)
        {
            NorthWestNeighbour = node;
        }

        public void SetNorthNeighhbour(Node node)
        {
            NorthNeightbour = node;
        }

        public void SetNorthEastNeighbour(Node node)
        {
            NorthEastNeighbour = node;
        }

        public void SetEastNeighhbour(Node node)
        {
            EastNeightbour = node;
        }

        public void SetSouthEastNeighbour(Node node)
        {
            SouthEastNeighbour = node;
        }

        public void SetSouthWestNeighbour(Node node)
        {
            SouthWestNeighbour = node;
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

            if (NorthNeightbour != null)
            {
                connectedNodes.Add(NorthNeightbour);
            }
            if (NorthEastNeighbour != null)
            {
                connectedNodes.Add(NorthEastNeighbour);
            }
            if (NorthWestNeighbour != null)
            {
                connectedNodes.Add(NorthWestNeighbour);
            }
            if (EastNeightbour != null)
            {
                connectedNodes.Add(EastNeightbour);
            }
            if (WestNeightbour != null)
            {
                connectedNodes.Add(WestNeightbour);
            }
            if (SouthNeightbour != null)
            {
                connectedNodes.Add(SouthNeightbour);
            }
            if (SouthWestNeighbour != null)
            {
                connectedNodes.Add(SouthWestNeighbour);
            }
            if (SouthEastNeighbour != null)
            {
                connectedNodes.Add(SouthEastNeighbour);
            }

            return connectedNodes;
        }
    }
}

