using System.Collections.Generic;
using UnityEngine;

public enum TileType { Floor, Wall, Start, End }

public class GridManager : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject tilePrefab;
    public float tileSize = 1f;

    public Tile[,] grid;
    public TileType currentPaint = TileType.Floor;

    private Tile startTile;
    private Tile endTile;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        // Wipes old tiles
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        grid = new Tile[width, height];

        float xOffset = (width - 1) * tileSize / 2f;
        float yOffset = (height - 1) * tileSize / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // local position related to GridManager
                float spacing = 0.1f;
                Vector3 localPos = new Vector3(x * (tileSize + spacing) - xOffset, y * (tileSize + spacing) - yOffset, 0f);

                // Instantiate as child without trying to set world position
                GameObject obj = Instantiate(tilePrefab, transform);

                // Makes sure the instantiated  object has Transform empty 
                obj.transform.localPosition = localPos;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one; // avoids unwanted inherited escalations

                obj.name = $"Tile_{x}_{y}";

                Tile tile = obj.GetComponent<Tile>();
                if (tile == null)
                {
                    Debug.LogError("Tile prefab no tiene componente Tile.");
                    continue;
                }

                tile.Init(x, y, this);
                grid[x, y] = tile;
            }
        }
    }

    public void SetCurrentPaint(int type)
    {
        currentPaint = (TileType)type;
    }

    public void RegisterStart(Tile tile)
    {
        if (startTile != null)
            startTile.SetType(TileType.Floor);

        startTile = tile;

        // Move player to exact world position of the tile
        var player = FindObjectOfType<PlayerMover>();
        if (player != null)
        {
            player.transform.position = tile.transform.position;
        }
    }



    public void RegisterEnd(Tile tile)
    {
        if (endTile != null)
            endTile.SetType(TileType.Floor);

        endTile = tile;
    }

    public Tile GetStart() => startTile;
    public Tile GetEnd() => endTile;
}
