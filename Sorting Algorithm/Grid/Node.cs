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
        private Brush colour = Brushes.WhiteSmoke;

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

        private List<Node> NeighbourBackups = new List<Node>();

        public List<Node> Neighbours => GetNeighbours();

        public bool IsWall { get; private set; } = false;

        public int Cost { get; set; }
        
        public double Heuristic { get; set; }

        public Node NorthWestNeighbour { get; private set; }

        public Node NorthNeightbour { get; private set; }

        public Node NorthEastNeighbour { get; private set; }

        public Node EastNeightbour { get; private set; }

        public Node SouthEastNeighbour { get; private set; }

        public Node SouthNeightbour { get; private set; }

        public Node SouthWestNeighbour { get; private set; }

        public Node WestNeightbour { get; private set; }

        public bool IsStartNode { get; set;  }

        public bool IsEndNode { get; set;  }

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Node()
        {

        }

        public bool IsSamePositionAs(Node node)
        {
            if (node.X == this.X && node.Y == this.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
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

            if (SouthWestNeighbour != null && SouthWestNeighbour.IsWall)
            {
                WestNeightbour.SouthEastNeighbour = null;
                SouthNeightbour.NorthWestNeighbour = null;
            }
            if (SouthEastNeighbour != null && SouthEastNeighbour.IsWall)
            {
                EastNeightbour.SouthWestNeighbour = null;
                SouthNeightbour.NorthEastNeighbour = null;
            }
            if (NorthEastNeighbour != null && NorthEastNeighbour.IsWall)
            {
                EastNeightbour.NorthWestNeighbour = null;
                NorthNeightbour.SouthEastNeighbour = null;
            }
            if (NorthWestNeighbour != null && NorthWestNeighbour.IsWall)
            {
                NorthNeightbour.SouthWestNeighbour = null;
                WestNeightbour.NorthEastNeighbour = null;
            }

            NeighbourBackups.AddRange(new List<Node>() { NorthNeightbour, NorthEastNeighbour, NorthWestNeighbour, EastNeightbour,
            WestNeightbour, SouthNeightbour, SouthEastNeighbour,SouthWestNeighbour });

            NorthNeightbour = null;
            NorthEastNeighbour = null;
            NorthWestNeighbour = null;
            EastNeightbour = null;
            WestNeightbour = null;
            SouthNeightbour = null;
            SouthEastNeighbour = null;
            SouthWestNeighbour = null;
        }

        public void RemoveWall()
        {
            Colour = Brushes.WhiteSmoke;
            IsWall = false;

            if (NeighbourBackups.Count > 0)
            {
                NorthNeightbour = NeighbourBackups[0];
                NorthEastNeighbour = NeighbourBackups[1] ?? null;
                NorthWestNeighbour = NeighbourBackups[2] ?? null;
                EastNeightbour = NeighbourBackups[3] ?? null;
                WestNeightbour = NeighbourBackups[4] ?? null;
                SouthNeightbour = NeighbourBackups[5] ?? null;
                SouthEastNeighbour = NeighbourBackups[6] ?? null;
                SouthWestNeighbour = NeighbourBackups[7] ?? null;
            }
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

