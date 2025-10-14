using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ----------------- Nodo ABB -----------------
public class MyABBNode
{
    public int Value;
    public MyABBNode Left;
    public MyABBNode Right;

    public MyABBNode(int value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}

// ----------------- ABB Tree -----------------
public class MyABBTree
{
    public MyABBNode Root { get; private set; }

    private GameObject nodePrefab;
    public Dictionary<MyABBNode, GameObject> nodeVisuals = new Dictionary<MyABBNode, GameObject>();

    public MyABBTree(GameObject prefab)
    {
        nodePrefab = prefab;
    }

    // ----------------- Insert -----------------
    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }

    private MyABBNode InsertRecursive(MyABBNode node, int value)
    {
        if (node == null)
        {
            MyABBNode newNode = new MyABBNode(value);
            CreateVisualNode(newNode);
            return newNode;
        }

        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive(node.Right, value);

        return node;
    }

    // ----------------- Visualización de nodo -----------------
    protected void CreateVisualNode(MyABBNode node)
    {
        if (nodePrefab == null) return;

        GameObject obj = GameObject.Instantiate(nodePrefab);
        obj.name = "Node_" + node.Value;

        TextMeshPro tmp = obj.GetComponentInChildren<TextMeshPro>();
        if (tmp != null) tmp.text = node.Value.ToString();

        nodeVisuals[node] = obj;
    }

    // ----------------- Altura -----------------
    public int GetHeight(MyABBNode node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    // ----------------- Factor de balance -----------------
    public int GetBalanceFactor(MyABBNode node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    // ----------------- Recorridos -----------------
    public void InOrder(MyABBNode node)
    {
        if (node == null) return;
        InOrder(node.Left);
        Debug.Log("InOrder: " + node.Value);
        InOrder(node.Right);
    }

    public void PreOrder(MyABBNode node)
    {
        if (node == null) return;
        Debug.Log("PreOrder: " + node.Value);
        PreOrder(node.Left);
        PreOrder(node.Right);
    }

    public void PostOrder(MyABBNode node)
    {
        if (node == null) return;
        PostOrder(node.Left);
        PostOrder(node.Right);
        Debug.Log("PostOrder: " + node.Value);
    }

    public void LevelOrder(MyABBNode root)
    {
        if (root == null) return;

        Queue<MyABBNode> queue = new Queue<MyABBNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            MyABBNode current = queue.Dequeue();
            Debug.Log("LevelOrder: " + current.Value);

            if (current.Left != null) queue.Enqueue(current.Left);
            if (current.Right != null) queue.Enqueue(current.Right);
        }
    }
}
