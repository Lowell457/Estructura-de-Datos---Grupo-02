using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UnityABBVisualizer : MonoBehaviour, IABBVisualizer
{
    public GameObject nodePrefab;

    private Dictionary<MyABBNode, GameObject> map = new Dictionary<MyABBNode, GameObject>();
    private List<GameObject> lines = new List<GameObject>();

    public void CreateVisual(MyABBNode node)
    {
        var obj = Instantiate(nodePrefab);
        obj.name = $"Node_{node.Value}";

        var tmp = obj.GetComponentInChildren<TextMeshPro>();
        if (tmp != null) tmp.text = node.Value.ToString();

        map[node] = obj;
    }

    public void Redraw(MyABBNode root)
    {
        ClearLines();

        float xMin = -10, xMax = 10, yStart = 4f;
        PositionRecursive(root, xMin, xMax, yStart);
        DrawLines(root);
    }

    private void ClearLines()
    {
        foreach (var l in lines) Destroy(l);
        lines.Clear();
    }

    private void PositionRecursive(MyABBNode node, float xMin, float xMax, float y)
    {
        if (node == null) return;

        float x = (xMin + xMax) / 2f;
        map[node].transform.position = new Vector2(x, y);

        float yOffset = -1.5f;

        PositionRecursive(node.Left, xMin, x, y + yOffset);
        PositionRecursive(node.Right, x, xMax, y + yOffset);
    }

    private void DrawLines(MyABBNode node)
    {
        if (node == null) return;

        if (node.Left != null) CreateLine(node, node.Left);
        if (node.Right != null) CreateLine(node, node.Right);

        DrawLines(node.Left);
        DrawLines(node.Right);
    }

    private void CreateLine(MyABBNode parent, MyABBNode child)
    {
        var obj = new GameObject($"Line_{parent.Value}_{child.Value}");
        var lr = obj.AddComponent<LineRenderer>();

        lr.positionCount = 2;
        lr.SetPosition(0, map[parent].transform.position);
        lr.SetPosition(1, map[child].transform.position);
        lr.startWidth = lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = lr.endColor = Color.white;

        lines.Add(obj);
    }
}
