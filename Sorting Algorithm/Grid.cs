using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PathFinding
{
    public class Grid
    {
        public int Width { get; private set; }

        public int Height { get; private set; }

        public Node[,] Nodes { get; private set; }

        public Grid(int x, int y)
        {
            Width = x;
            Height = y;
            Nodes = new Node[x, y];

            SetupGrid(x, y);
        }

        private void SetupGrid(int x, int y)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Nodes[i, j] = new Node(i, j);
                }
            }
        }

        public void SelectNode(int x, int y)
        {
            Nodes[x, y].Colour = Brushes.Black;
        }
    }
}
