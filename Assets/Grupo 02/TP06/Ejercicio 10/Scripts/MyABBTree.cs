using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyABBTree
{
    public MyABBNode Root { get; private set; }

    private GameObject nodePrefab;
    private Dictionary<MyABBNode, GameObject> nodeVisuals = new Dictionary<MyABBNode, GameObject>();

    public MyABBTree(GameObject prefab)
    {
        nodePrefab = prefab;
    }

    // -------------------------------
    // Insertar nodo
    // -------------------------------
    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value, Vector2.zero, 0);
    }

    private MyABBNode InsertRecursive(MyABBNode node, int value, Vector2 position, int depth)
    {
        if (node == null)
        {
            MyABBNode newNode = new MyABBNode(value);
            CreateVisualNode(newNode, position);
            return newNode;
        }

        float horizontalOffset = 2.0f / (depth + 1); // controla la separación horizontal

        if (value < node.Value)
        {
            Vector2 leftPos = new Vector2(position.x - horizontalOffset, position.y - 1.5f);
            node.Left = InsertRecursive(node.Left, value, leftPos, depth + 1);
            DrawLine(node, node.Left);
        }
        else if (value > node.Value)
        {
            Vector2 rightPos = new Vector2(position.x + horizontalOffset, position.y - 1.5f);
            node.Right = InsertRecursive(node.Right, value, rightPos, depth + 1);
            DrawLine(node, node.Right);
        }

        return node;
    }

    // -------------------------------
    // Obtener altura del árbol o subárbol
    // -------------------------------
    public int GetHeight(MyABBNode node)
    {
        if (node == null)
            return 0;

        int leftHeight = GetHeight(node.Left);
        int rightHeight = GetHeight(node.Right);
        return 1 + Mathf.Max(leftHeight, rightHeight);
    }

    // -------------------------------
    // Obtener factor de balance de un nodo
    // -------------------------------
    public int GetBalanceFactor(MyABBNode node)
    {
        if (node == null)
            return 0;

        return GetHeight(node.Left) - GetHeight(node.Right);
    }
    // -------------------------------
    // Recorridos del árbol
    // -------------------------------
    public void InOrder(MyABBNode node)
    {
        if (node == null) return;

        InOrder(node.Left);
        Debug.Log("InOrder visit: " + node.Value);
        InOrder(node.Right);
    }

    public void PreOrder(MyABBNode node)
    {
        if (node == null) return;

        Debug.Log("PreOrder visit: " + node.Value);
        PreOrder(node.Left);
        PreOrder(node.Right);
    }

    public void PostOrder(MyABBNode node)
    {
        if (node == null) return;

        PostOrder(node.Left);
        PostOrder(node.Right);
        Debug.Log("PostOrder visit: " + node.Value);
    }

    public void LevelOrder(MyABBNode root)
    {
        if (root == null) return;

        Queue<MyABBNode> queue = new Queue<MyABBNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            MyABBNode current = queue.Dequeue();
            Debug.Log("LevelOrder visit: " + current.Value);

            if (current.Left != null) queue.Enqueue(current.Left);
            if (current.Right != null) queue.Enqueue(current.Right);
        }
    }

    // -------------------------------
    // Crear nodo visual
    // -------------------------------
    private void CreateVisualNode(MyABBNode node, Vector2 position)
    {
        GameObject obj = GameObject.Instantiate(nodePrefab);
        obj.transform.position = position;
        obj.GetComponentInChildren<TextMeshPro>().text = node.Value.ToString();
        nodeVisuals[node] = obj;
    }

    // -------------------------------
    // Dibujar línea entre dos nodos
    // -------------------------------
    private void DrawLine(MyABBNode parent, MyABBNode child)
    {
        if (parent == null || child == null) return;

        GameObject parentObj = nodeVisuals[parent];
        GameObject childObj = nodeVisuals[child];

        GameObject lineObj = new GameObject("Line_" + parent.Value + "_" + child.Value);
        LineRenderer line = lineObj.AddComponent<LineRenderer>();
        line.positionCount = 2;
        line.SetPosition(0, parentObj.transform.position);
        line.SetPosition(1, childObj.transform.position);
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.white;
        line.endColor = Color.white;
    }
}
