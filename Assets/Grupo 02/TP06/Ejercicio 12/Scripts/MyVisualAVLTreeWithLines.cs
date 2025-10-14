using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ----------------- Nodo AVL Visual -----------------
public class MyVisualAVLNode : MyABBNode
{
    public int Height;

    public MyVisualAVLNode(int value) : base(value)
    {
        Height = 1;
    }
}

// ----------------- AVL Tree Visual con líneas -----------------
public class MyVisualAVLTreeWithLines : MonoBehaviour
{
    public GameObject nodePrefab;    // Prefab de nodo (círculo + TMP)
    public GameObject linePrefab;    // Prefab con LineRenderer
    public float yOffset = 2.0f;     // Altura entre niveles

    private MyVisualAVLNode root;
    private Dictionary<MyVisualAVLNode, GameObject> nodeVisuals = new Dictionary<MyVisualAVLNode, GameObject>();
    private List<LineRenderer> lines = new List<LineRenderer>();

    // ----------------- Insert -----------------
    public void Insert(int value)
    {
        root = InsertRecursive(root, value);
        UpdateVisualTree();
    }

    private MyVisualAVLNode InsertRecursive(MyVisualAVLNode node, int value)
    {
        if (node == null)
        {
            MyVisualAVLNode newNode = new MyVisualAVLNode(value);
            CreateVisualNode(newNode);
            return newNode;
        }

        if (value < node.Value)
            node.Left = InsertRecursive((MyVisualAVLNode)node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive((MyVisualAVLNode)node.Right, value);
        else
            return node;

        node.Height = 1 + Math.Max(GetHeight((MyVisualAVLNode)node.Left), GetHeight((MyVisualAVLNode)node.Right));
        int balance = GetBalanceFactor(node);

        // Rotaciones AVL
        if (balance > 1 && value < node.Left.Value)
            return RightRotate(node);
        if (balance < -1 && value > node.Right.Value)
            return LeftRotate(node);
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = LeftRotate((MyVisualAVLNode)node.Left);
            return RightRotate(node);
        }
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RightRotate((MyVisualAVLNode)node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    // ----------------- Rotaciones -----------------
    private MyVisualAVLNode RightRotate(MyVisualAVLNode y)
    {
        MyVisualAVLNode x = (MyVisualAVLNode)y.Left;
        MyVisualAVLNode T2 = (MyVisualAVLNode)x.Right;

        x.Right = y;
        y.Left = T2;

        y.Height = 1 + Math.Max(GetHeight((MyVisualAVLNode)y.Left), GetHeight((MyVisualAVLNode)y.Right));
        x.Height = 1 + Math.Max(GetHeight((MyVisualAVLNode)x.Left), GetHeight((MyVisualAVLNode)x.Right));

        return x;
    }

    private MyVisualAVLNode LeftRotate(MyVisualAVLNode x)
    {
        MyVisualAVLNode y = (MyVisualAVLNode)x.Right;
        MyVisualAVLNode T2 = (MyVisualAVLNode)y.Left;

        y.Left = x;
        x.Right = T2;

        x.Height = 1 + Math.Max(GetHeight((MyVisualAVLNode)x.Left), GetHeight((MyVisualAVLNode)x.Right));
        y.Height = 1 + Math.Max(GetHeight((MyVisualAVLNode)y.Left), GetHeight((MyVisualAVLNode)y.Right));

        return y;
    }

    // ----------------- Altura y balance -----------------
    private int GetHeight(MyVisualAVLNode node) => node != null ? node.Height : 0;

    private int GetBalanceFactor(MyVisualAVLNode node)
    {
        if (node == null) return 0;
        return GetHeight((MyVisualAVLNode)node.Left) - GetHeight((MyVisualAVLNode)node.Right);
    }

    // ----------------- Visualización -----------------
    private void CreateVisualNode(MyVisualAVLNode node)
    {
        if (nodePrefab == null) return;

        GameObject obj = Instantiate(nodePrefab);
        obj.name = "Node_" + node.Value;

        TextMeshPro tmp = obj.GetComponentInChildren<TextMeshPro>();
        if (tmp != null) tmp.text = node.Value.ToString();

        nodeVisuals[node] = obj;
    }

    private void UpdateVisualTree()
    {
        // Limpiar líneas anteriores
        foreach (var line in lines)
            Destroy(line.gameObject);
        lines.Clear();

        if (root == null) return;

        // 1. Posicionar nodos
        float treeWidth = SetNodePosition(root, 0, 0, 5.0f, null);

        // 2. Calcular offset para centralizar el árbol
        float offsetX = treeWidth / 2f;

        // 3. Desplazar todos los nodos para centrar
        foreach (var kvp in nodeVisuals)
        {
            Vector3 pos = kvp.Value.transform.position;
            kvp.Value.transform.position = new Vector3(pos.x - offsetX, pos.y, pos.z);
        }

        // 4. Volver a dibujar las líneas
        foreach (var kvp in nodeVisuals)
        {
            MyVisualAVLNode node = kvp.Key;
            if (node.Left != null)
                DrawLine(kvp.Value.transform.position, nodeVisuals[(MyVisualAVLNode)node.Left].transform.position);
            if (node.Right != null)
                DrawLine(kvp.Value.transform.position, nodeVisuals[(MyVisualAVLNode)node.Right].transform.position);
        }
    }

    private float SetNodePosition(MyVisualAVLNode node, int depth, float xPos, float spread, MyVisualAVLNode parent)
    {
        if (node == null) return xPos;

        float leftX = SetNodePosition((MyVisualAVLNode)node.Left, depth + 1, xPos, spread / 2, node);
        float nodeX = leftX;

        if (nodeVisuals.ContainsKey(node))
            nodeVisuals[node].transform.position = new Vector3(nodeX, -depth * yOffset, 0);

        float rightX = SetNodePosition((MyVisualAVLNode)node.Right, depth + 1, nodeX + spread, spread / 2, node);

        // Centrar nodo si tiene ambos hijos
        if (nodeVisuals.ContainsKey(node) && node.Left != null && node.Right != null)
        {
            nodeVisuals[node].transform.position = new Vector3(
                (nodeVisuals[(MyVisualAVLNode)node.Left].transform.position.x +
                 nodeVisuals[(MyVisualAVLNode)node.Right].transform.position.x) / 2,
                -depth * yOffset, 0);
        }

        return nodeVisuals.ContainsKey(node) ? nodeVisuals[node].transform.position.x + spread / 2 : nodeX + spread / 2;
    }

    private void DrawLine(Vector3 start, Vector3 end)
    {
        if (linePrefab == null) return;

        GameObject lineObj = Instantiate(linePrefab);
        LineRenderer lr = lineObj.GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        lines.Add(lr);
    }

    public MyVisualAVLNode GetRoot() => root;
}
