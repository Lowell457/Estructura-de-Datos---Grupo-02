using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    public GridManager grid;
    public float moveDelay = 0.3f;

    public void StartPath()
    {
        Tile start = grid.GetStart();
        Tile end = grid.GetEnd();

        if (start == null || end == null)
        {
            Debug.LogWarning("Missing Start or End Point.");
            UIManager.Instance.SetStatus("Place Start and End.");
            return;
        }

        var path = Pathfinder.FindPath(grid, start, end);

        if (path == null || path.Count == 0)
        {
            Debug.Log("No possible path");
            UIManager.Instance.SetStatus("No Solution");
        }
        else
        {
            Debug.Log($"Solution found ({path.Count} steps)");
            UIManager.Instance.SetStatus("Solution found");
            StopAllCoroutines(); // To stop multiple button presses
            StartCoroutine(MoveAlongPath(path));
        }
    }

    IEnumerator MoveAlongPath(List<Tile> path)
    {
        foreach (var tile in path)
        {
            // Move to the tile’s actual world position, not its grid coords
            transform.position = tile.transform.position;
            yield return new WaitForSeconds(moveDelay);
        }
    }
}
