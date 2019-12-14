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
        private Brush colour = Brushes.Khaki;

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

        public bool IsWall { get; private set; } = false;

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

        /// <summary>
        /// Indicates if the node has two walls that are
        /// adjacent to itself. This is used to determine
        /// if the node can be walked on when a diagonal wall
        /// is placed next to it.
        /// </summary>
        /// <returns></returns>
        public bool IsSurroundedByDiagonalWall()
        {
            if(NorthNeightbour != null && NorthNeightbour.IsWall && 
                WestNeightbour!= null && WestNeightbour.IsWall)
            {
                return true;
            }
            if (NorthNeightbour != null && NorthNeightbour.IsWall &&
                EastNeightbour != null && EastNeightbour.IsWall)
            {
                return true;
            }
            if(SouthNeightbour != null && SouthNeightbour.IsWall && 
                EastNeightbour != null &&  EastNeightbour.IsWall)
            {
                return true;
            }
            if (SouthNeightbour != null && SouthNeightbour.IsWall &&
                WestNeightbour != null && WestNeightbour.IsWall)
            {
                return true;
            }

            return false;
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

        public void MakeWall()
        {
            Colour = Brushes.Black;
            IsWall = true;

            NorthNeightbour = null;
            NorthEastNeighbour = null;
            NorthWestNeighbour = null;
            EastNeightbour = null;
            WestNeightbour = null;
            SouthNeightbour = null;
            SouthEastNeighbour = null;
            SouthWestNeighbour = null;
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

