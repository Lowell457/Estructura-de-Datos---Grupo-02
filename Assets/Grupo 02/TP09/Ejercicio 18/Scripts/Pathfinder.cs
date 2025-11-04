using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    public static List<Tile> FindPath(GridManager grid, Tile start, Tile end)
    {
        if (start == null || end == null)
            return null;

        var openSet = new List<Tile> { start };
        var cameFrom = new Dictionary<Tile, Tile>();

        var gScore = new Dictionary<Tile, float>();
        var fScore = new Dictionary<Tile, float>();

        foreach (var tile in grid.grid)
        {
            gScore[tile] = Mathf.Infinity;
            fScore[tile] = Mathf.Infinity;
        }

        gScore[start] = 0;
        fScore[start] = Heuristic(start, end);

        while (openSet.Count > 0)
        {
            openSet.Sort((a, b) => fScore[a].CompareTo(fScore[b]));
            Tile current = openSet[0];
            openSet.RemoveAt(0);

            if (current == end)
                return ReconstructPath(cameFrom, current);

            foreach (var neighbor in GetNeighbors(grid, current))
            {
                float tentative = gScore[current] + 1;

                if (tentative < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative;
                    fScore[neighbor] = tentative + Heuristic(neighbor, end);

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null; // sin solución
    }

    static float Heuristic(Tile a, Tile b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    static List<Tile> ReconstructPath(Dictionary<Tile, Tile> cameFrom, Tile current)
    {
        var total = new List<Tile> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            total.Insert(0, current);
        }
        return total;
    }

    static List<Tile> GetNeighbors(GridManager grid, Tile tile)
    {
        var dirs = new Vector2Int[] {
            new Vector2Int(1,0), new Vector2Int(-1,0),
            new Vector2Int(0,1), new Vector2Int(0,-1)
        };
        List<Tile> neighbors = new List<Tile>();

        foreach (var d in dirs)
        {
            int nx = tile.x + d.x;
            int ny = tile.y + d.y;
            if (nx >= 0 && nx < grid.width && ny >= 0 && ny < grid.height)
            {
                Tile t = grid.grid[nx, ny];
                if (t.IsWalkable())
                    neighbors.Add(t);
            }
        }

        return neighbors;
    }
}
