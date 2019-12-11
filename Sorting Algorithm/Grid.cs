using System;
using System.Collections.Generic;
using System.IO;
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
        public object FIle { get; private set; }

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
                    Node node = new Node(i, j);

                    Nodes[i, j] = node;
                }
            }

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Node node = Nodes[i, j];

                    if (i - 1 >= 0)
                    {
                        node.SetWestNeighhbour(Nodes[i - 1, j]);
                    }
                    if (i + 1 < x)
                    {
                        node.SetEastNeighhbour(Nodes[i + 1, j]);
                    }
                    if (j - 1 >= 0)
                    {
                        node.SetNorthNeighhbour(Nodes[i, j - 1]);
                    }
                    if (j + 1 < y)
                    {
                        node.SetSouthNeighhbour(Nodes[i, j + 1]);
                    }
                }
            }

            List<Node> shortestPath = DijkstrasAlgorithm(Nodes[87, 1], Nodes[1, 92]);

            foreach(Node node in shortestPath)
            {
                node.Colour = Brushes.Black;
            }
        }

        public void SelectNode(int x, int y)
        {
            Nodes[x, y].Colour = Brushes.Black;
        }

        public List<Node> DijkstrasAlgorithm(Node start, Node end)
        {
            Dictionary<Node, int> totalCosts = new Dictionary<Node, int>();
            Dictionary<Node, Node> prevNodes = new Dictionary<Node, Node>();
            SimplePriorityQueue<Node> priorityQueue = new SimplePriorityQueue<Node>();
            List<Node> visited = new List<Node>();

            totalCosts.Add(start, 0);
            priorityQueue.Enqueue(start, 0);

            foreach (Node node in Nodes)
            {
                if (node != start)
                {
                    totalCosts.Add(node, int.MaxValue);
                }
            }

            while (priorityQueue.Count != 0)
            {
                Node newSmallest = priorityQueue.Dequeue();

                foreach (Node neighbour in newSmallest.Neighbours)
                {
                    if (!visited.Contains(neighbour))
                    {
                        int altPath = totalCosts[newSmallest] + 1; // Cost is always 1.

                        if (altPath < totalCosts[neighbour])
                        {
                            totalCosts[neighbour] = altPath;
                            prevNodes.Add(neighbour, newSmallest);

                            if (priorityQueue.Contains(neighbour))
                            {
                                priorityQueue.UpdatePriority(neighbour, altPath);
                            }
                            else
                            {
                                priorityQueue.Enqueue(neighbour, altPath);
                            }
                        }

                        visited.Add(neighbour);
                    }
                }
            }

            List<Node> shortestPath = new List<Node>();
            Node curr = end;

            while (curr != start)
            {
                List<KeyValuePair<Node, Node>> neighbours = prevNodes.Where(x => x.Key.X == curr.X && x.Key.Y == curr.Y).ToList();

                int lowestCost = int.MaxValue;
                Node lowestCostNode = null;

                foreach(var pair in neighbours)
                {
                    Node newNode = pair.Value;

                    if(totalCosts[newNode] < lowestCost)
                    {
                        lowestCost = totalCosts[newNode];
                        lowestCostNode = newNode;
                    }
                }

                shortestPath.Add(lowestCostNode);
                curr = lowestCostNode;
            }

            return shortestPath;
        }
    }
}



