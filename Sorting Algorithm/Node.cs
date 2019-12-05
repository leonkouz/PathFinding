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

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
