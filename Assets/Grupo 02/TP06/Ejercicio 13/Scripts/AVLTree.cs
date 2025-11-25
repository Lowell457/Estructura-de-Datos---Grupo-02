using System;
using System.Collections.Generic;
using UnityEngine;

public class AVLTree
{
    public NodeTp7 root;

    public event Action<NodeTp7> OnNodeCreated;

    public void Insert(int score, string playerName)
    {
        root = InsertRecursive(root, score, playerName);
    }

    private NodeTp7 InsertRecursive(NodeTp7 node, int score, string playerName)
    {
        if (node == null)
        {
            var newNode = new NodeTp7(score, playerName);
            OnNodeCreated?.Invoke(newNode);
            return newNode;
        }

        // Block duplicate names
        if (node.PlayerName == playerName)
        {
            Debug.LogWarning($"Player '{playerName}' already exists. Not inserting.");
            return node;
        }

        // Allow duplicate scores
        if (score > node.Score)                    
            node.Left = InsertRecursive(node.Left, score, playerName);
        else                                        
            node.Right = InsertRecursive(node.Right, score, playerName);

        UpdateHeight(node);

        return Balance(node);
    }


    // AVL helpers 

    private int Height(NodeTp7 n) => n?.Height ?? 0;

    private void UpdateHeight(NodeTp7 n) => n.Height = Mathf.Max(Height(n.Left), Height(n.Right)) + 1;

    private int BalanceFactor(NodeTp7 n) => Height(n.Left) - Height(n.Right);

    private NodeTp7 Balance(NodeTp7 node)
    {
        int bf = BalanceFactor(node);

        if (bf > 1)
        {
            if (BalanceFactor(node.Left) < 0)
                node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        if (bf < -1)
        {
            if (BalanceFactor(node.Right) > 0)
                node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private NodeTp7 RotateRight(NodeTp7 y)
    {
        NodeTp7 x = y.Left;
        NodeTp7 T2 = x.Right;

        x.Right = y;
        y.Left = T2;

        UpdateHeight(y);
        UpdateHeight(x);
        return x;
    }

    private NodeTp7 RotateLeft(NodeTp7 x)
    {
        NodeTp7 y = x.Right;
        NodeTp7 T2 = y.Left;

        y.Left = x;
        x.Right = T2;

        UpdateHeight(x);
        UpdateHeight(y);
        return y;
    }

    // List traversals (For UI)

    public List<NodeTp7> InOrder()
    {
        List<NodeTp7> list = new();
        InOrder(root, list);
        return list;
    }
    private void InOrder(NodeTp7 n, List<NodeTp7> list)
    {
        if (n == null) return;
        InOrder(n.Left, list);
        list.Add(n);
        InOrder(n.Right, list);
    }

    public List<NodeTp7> PreOrder()
    {
        List<NodeTp7> list = new();
        PreOrder(root, list);
        return list;
    }
    private void PreOrder(NodeTp7 n, List<NodeTp7> list)
    {
        if (n == null) return;
        list.Add(n);
        PreOrder(n.Left, list);
        PreOrder(n.Right, list);
    }

    public List<NodeTp7> PostOrder()
    {
        List<NodeTp7> list = new();
        PostOrder(root, list);
        return list;
    }
    private void PostOrder(NodeTp7 n, List<NodeTp7> list)
    {
        if (n == null) return;
        PostOrder(n.Left, list);
        PostOrder(n.Right, list);
        list.Add(n);
    }

    public List<NodeTp7> LevelOrder()
    {
        List<NodeTp7> result = new();
        if (root == null) return result;

        Queue<NodeTp7> q = new();
        q.Enqueue(root);

        while (q.Count > 0)
        {
            var n = q.Dequeue();
            result.Add(n);

            if (n.Left != null) q.Enqueue(n.Left);
            if (n.Right != null) q.Enqueue(n.Right);
        }

        return result;
    }
    
    // Checking for duplicate names

    public bool ContainsName(string name)
    {
        return ContainsNameRecursive(root, name);
    }

    private bool ContainsNameRecursive(NodeTp7 node, string name)
    {
        if (node == null) return false;

        if (node.PlayerName.Equals(name, StringComparison.OrdinalIgnoreCase))
            return true;

        return ContainsNameRecursive(node.Left, name) ||
               ContainsNameRecursive(node.Right, name);
    }

}
