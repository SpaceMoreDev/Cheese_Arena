using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ICoords {
    public float GetDistance(ICoords other);
    public Vector2 Pos { get; set; }
}
public class NodePathfinding
{
    public NodePathfinding Connection {get; private set;}
    public List<NodePathfinding> Neighbors {get; private set;}
    public ICoords Coords;
    public float GetDistance(NodePathfinding other) => Coords.GetDistance(other.Coords); // Helper to reduce noise in pathfinding
    public float G {get; private set;}
    public float H {get; private set;}
    public float F => G+H;
    public bool Walkable { get; private set; }

    public void MakeConnection(NodePathfinding node)=>Connection = node;
    public void SetG(float g)=>G=g;
    public void SetH(float h)=>H=h;


    static List<NodePathfinding> FindPath(NodePathfinding startNode, NodePathfinding targetNode)
    {
        var toSearch = new List<NodePathfinding>(){startNode};
        var processed = new List<NodePathfinding>();

        while(toSearch.Any()){
            var current = toSearch[0];
            foreach(var i in toSearch){
                if(i.F<current.F || i.F == current.F && i.H < current.H){
                    current = i;
                }
            }

            processed.Add(current);
            toSearch.Remove(current);

            if (current == targetNode) {
                var currentPathTile = targetNode;
                var path = new List<NodePathfinding>();
                var count = 100;
                while (currentPathTile != startNode) {
                    path.Add(currentPathTile);
                    currentPathTile = currentPathTile.Connection;
                    count--;
                    if (count < 0) throw new Exception();
                }
                
                return path;
            }

            foreach (var neighbor in current.Neighbors.Where(t => t.Walkable && !processed.Contains(t))) {
                var inSearch = toSearch.Contains(neighbor);

                var costToNeighbor = current.G + current.GetDistance(neighbor);

                if (!inSearch || costToNeighbor < neighbor.G) {
                    neighbor.SetG(costToNeighbor);
                    neighbor.MakeConnection(current);

                    if (!inSearch) {
                        neighbor.SetH(neighbor.GetDistance(targetNode));
                        toSearch.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

}
