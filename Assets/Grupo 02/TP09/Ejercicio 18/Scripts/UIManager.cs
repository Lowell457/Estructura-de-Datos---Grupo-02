using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GridManager grid;
    public PlayerMover player;
    public TextMeshProUGUI statusText;

    void Awake() => Instance = this;

    public void OnSetTileType(int type)
    {
        grid.SetCurrentPaint(type);
    }

    public void OnCheckPath()
    {
        player.StartPath();
    }

    public void SetStatus(string text)
    {
        statusText.text = text;
    }
}
