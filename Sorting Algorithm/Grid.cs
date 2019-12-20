using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
            Nodes.Initialize();

            SetupGrid(x, y);
        }

        public void Clear()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Node[] nodesArray = Nodes.Cast<Node>().ToArray();

                Parallel.ForEach(nodesArray, node =>
                {
                    node.Colour = Brushes.White;

                    if (node.IsWall)
                    {
                        node.RemoveWall();
                    }
                });
            });
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
                    if (i + 1 < x && j - 1 >= 0)
                    {
                        node.SetNorthEastNeighbour(Nodes[i + 1, j - 1]);
                    }
                    if (i - 1 >= 0 && j - 1 >= 0)
                    {
                        node.SetNorthWestNeighbour(Nodes[i - 1, j - 1]);
                    }
                    if (j - 1 >= 0)
                    {
                        node.SetNorthNeighhbour(Nodes[i, j - 1]);
                    }
                    if (i + 1 < x && j + 1 < y)
                    {
                        node.SetSouthEastNeighbour(Nodes[i + 1, j + 1]);
                    }
                    if (i - 1 >= 0 && j + 1 < y)
                    {
                        node.SetSouthWestNeighbour(Nodes[i - 1, j + 1]);
                    }
                    if (j + 1 < y)
                    {
                        node.SetSouthNeighhbour(Nodes[i, j + 1]);
                    }
                }
            }

        }

        public void MakeWall(Node node)
        {
            node.MakeWall();
        }

        public List<Node> DijkstrasAlgorithm(Node start, Node end)
        {
            Node originalStart = start;
            Node originalEnd = end;

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
                    if (neighbour.IsWall == true)
                    {
                        continue;
                    }

                    if (!neighbour.IsSamePositionAs(originalStart) && !neighbour.IsSamePositionAs(originalEnd))
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                             neighbour.Colour = Brushes.SkyBlue;
                        });
                    }

                    // Run after changing colour to green to 
                    // show that node is still processed by the algorithm.
                    if (neighbour.IsSurroundedByDiagonalWall())
                    {
                        continue;
                    }

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

                        if (neighbour == end)
                        {
                            priorityQueue.Clear();
                            break;
                        }
                    }

                    Thread.Sleep(5);

                }
            }

            List<Node> shortestPath = new List<Node>();
            Node curr = end;

            while (curr != start)
            {
                Node neighbour = prevNodes.Where(x => x.Key.X == curr.X && x.Key.Y == curr.Y).First().Value;

                if (!neighbour.IsSamePositionAs(originalStart) && !neighbour.IsSamePositionAs(originalEnd))
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        neighbour.Colour = Brushes.DarkSlateBlue;
                    });
                }

                shortestPath.Add(neighbour);
                curr = neighbour;
            }

            return shortestPath;
        }
    }
}



