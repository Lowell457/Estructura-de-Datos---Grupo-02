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
        // Limpia hijos previos (si re-generate)
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);

        grid = new Tile[width, height];

        float xOffset = (width - 1) * tileSize / 2f;
        float yOffset = (height - 1) * tileSize / 2f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // posición local relativa al GridManager
                Vector3 localPos = new Vector3(x * tileSize - xOffset, y * tileSize - yOffset, 0f);

                // Instancia como hijo sin intentar fijar world position
                GameObject obj = Instantiate(tilePrefab, transform);

                // Asegura que el objeto instanciado tenga el transform limpio
                obj.transform.localPosition = localPos;
                obj.transform.localRotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one; // evita escalados indeseados heredados

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
