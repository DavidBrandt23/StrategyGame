using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node
{
    public float? MinCostToStart = null;
    public Node NearestToStart;
    public bool Visited;
    public List<Node> Connections = new List<Node>();
    public Vector2Int pos;
}

//public class NodeCompareClass : IComparer<Node>
//{
//    public int Compare(Node x, Node y)
//    {
//        if(x.MinCostToStart)
//    }
//}

public class PathSearch
{
    private Node Start;
    private Node End;

    public PathSearch(bool[,] map, Vector2Int start, Vector2Int end)
    {
        Node[,] nodeMap = new Node[map.GetLength(0), map.GetLength(1)];
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                nodeMap[x, y] = new Node
                {
                    pos = new Vector2Int(x, y)
                };
            }
        }
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                Node node = nodeMap[x, y];
                AddNode(node.Connections, x + 1, y, map, nodeMap);
                AddNode(node.Connections, x - 1, y, map, nodeMap);
                AddNode(node.Connections, x, y + 1, map, nodeMap);
                AddNode(node.Connections, x , y - 1, map, nodeMap);
            }
        }
        Start = nodeMap[start.x,start.y];
        End = nodeMap[end.x, end.y];
    }

    public List<Vector2Int> GetPath()
    {
        List<Vector2Int> pointPath = new List<Vector2Int>();
        List<Node> nodePath = GetShortestPathDijkstra();
        foreach (Node n in nodePath)
        {
            Debug.Log(n.pos.x + "," + n.pos.y);
            pointPath.Add(n.pos);
        }
        pointPath.RemoveAt(0);
        return pointPath;
    }

    private void AddNode(List<Node> con, int x, int y, bool[,] map, Node[,] nodeMap)
    {
        if (validPos(x, y, map))
        {
            con.Add(nodeMap[x, y]);
        }
    }

    private bool validPos(int x, int y, bool[,] map)
    {
        return x >= 0 && y >= 0 && x < map.GetLength(0) && y < map.GetLength(1) && map[x, y];
    }

    private List<Node> GetShortestPathDijkstra()
    {
        DijkstraSearch();
        var shortestPath = new List<Node>();
        shortestPath.Add(End);
        BuildShortestPath(shortestPath, End);
        shortestPath.Reverse();
        return shortestPath;
    }

    private void BuildShortestPath(List<Node> list, Node node)
    {
        if (node.NearestToStart == null)
        {
            return;
        }
        list.Add(node.NearestToStart);
        BuildShortestPath(list, node.NearestToStart);
    }

    private void DijkstraSearch()
    {
        Start.MinCostToStart = 0;
        var prioQueue = new List<Node>();
        prioQueue.Add(Start);
        do
        {
            //NodeCompareClass c = new NodeCompareClass();
            //prioQueue.Sort(c);
            Node node = prioQueue[0];
            prioQueue.Remove(node);
            //foreach (var cnn in node.Connections.OrderBy(x => x.Cost))
            foreach (Node childNode in node.Connections)
            {
              //  var childNode = cnn.ConnectedNode;
                if (childNode.Visited)
                {
                    continue;
                }

                if (childNode.MinCostToStart == null ||
                    node.MinCostToStart + 1 < childNode.MinCostToStart)
                {
                    childNode.MinCostToStart = node.MinCostToStart + 1;
                    childNode.NearestToStart = node;
                    if (!prioQueue.Contains(childNode))
                    {
                        prioQueue.Add(childNode);
                    }
                }
            }
            node.Visited = true;
            if (node == End)
            {
                return;
            }
        } while (prioQueue.Count > 0);
    }
}
