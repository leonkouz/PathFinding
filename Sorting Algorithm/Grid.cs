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
            Parallel.For(0, y, i =>
            {
                for (int j = 0; j < x; j++)
                {
                    Node node = new Node(i, j);

                    Nodes[j, i] = node;
                }
            });

            Parallel.For(0, y, i =>
            {
                for (int j = 0; j < x; j++)
                {
                    Node node = Nodes[j, i];

                    if (j > 0)
                    {
                        node.SetWestNeighhbour(Nodes[j - 1, i]);
                    }
                    if (j + 1 < x)
                    {
                        node.SetEastNeighhbour(Nodes[j + 1, i]);
                    }
                    if (i > 0)
                    {
                        node.SetNorthNeighhbour(Nodes[j, i - 1]);
                    }
                    if (i + 1 < y)
                    {
                        node.SetSouthNeighhbour(Nodes[j, i + 1]);
                    }
                    if (j > 0 && i > 0)
                    {
                        node.SetNorthWestNeighbour(Nodes[j - 1, i - 1]);
                    }
                    if (j + 1 < x && i > 0)
                    {
                        node.SetNorthEastNeighbour(Nodes[j + 1, i - 1]);
                    }
                    if (j + 1 < x && i + 1 < y)
                    {
                        node.SetSouthEastNeighbour(Nodes[j + 1, i + 1]);
                    }
                    if(j > 0 && i + 1 < y)
                    {
                        node.SetSouthWestNeighbour(Nodes[j - 1, i + 1]);
                    }
                }
            });
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



