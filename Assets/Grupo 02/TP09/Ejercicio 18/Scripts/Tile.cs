using UnityEngine;

public class Tile : MonoBehaviour
{
    public int x, y;
    public TileType type = TileType.Floor;

    private SpriteRenderer sr;
    private GridManager grid;

    public void Init(int x, int y, GridManager grid)
    {
        this.x = x;
        this.y = y;
        this.grid = grid;
        sr = GetComponent<SpriteRenderer>();
        UpdateColor();
    }

    void OnMouseDown()
    {
        SetType(grid.currentPaint);
    }

    public void SetType(TileType newType)
    {
        type = newType;
        if (type == TileType.Start)
            grid.RegisterStart(this);
        else if (type == TileType.End)
            grid.RegisterEnd(this);

        UpdateColor();
    }

    void UpdateColor()
    {
        Color c = Color.white;
        switch (type)
        {
            case TileType.Floor: c = Color.white; break;
            case TileType.Wall: c = Color.black; break;
            case TileType.Start: c = Color.blue; break;
            case TileType.End: c = Color.red; break;
        }
        sr.color = c;
    }

    public bool IsWalkable() => type != TileType.Wall;
}
