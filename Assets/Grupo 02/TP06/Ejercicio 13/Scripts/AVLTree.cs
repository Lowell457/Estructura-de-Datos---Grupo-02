using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AVLNode
{
    public int score;
    public string playerName;
    public AVLNode left, right;
    public int height;

    public AVLNode(int score, string playerName)
    {
        this.score = score;
        this.playerName = playerName;
        height = 1;
    }
}

public class AVLTree
{
    public AVLNode root;

    private int Height(AVLNode n) => n == null ? 0 : n.height;
    private int GetBalance(AVLNode n) => n == null ? 0 : Height(n.left) - Height(n.right);

    private AVLNode RotateRight(AVLNode y)
    {
        AVLNode x = y.left;
        AVLNode T2 = x.right;
        x.right = y;
        y.left = T2;
        y.height = Mathf.Max(Height(y.left), Height(y.right)) + 1;
        x.height = Mathf.Max(Height(x.left), Height(x.right)) + 1;
        return x;
    }

    private AVLNode RotateLeft(AVLNode x)
    {
        AVLNode y = x.right;
        AVLNode T2 = y.left;
        y.left = x;
        x.right = T2;
        x.height = Mathf.Max(Height(x.left), Height(x.right)) + 1;
        y.height = Mathf.Max(Height(y.left), Height(y.right)) + 1;
        return y;
    }

    public void Insert(int score, string playerName)
    {
        root = Insert(root, score, playerName);
    }

    private AVLNode Insert(AVLNode node, int score, string playerName)
    {
        if (node == null) return new AVLNode(score, playerName);
        if (score < node.score)
            node.left = Insert(node.left, score, playerName);
        else
            node.right = Insert(node.right, score, playerName);

        node.height = Mathf.Max(Height(node.left), Height(node.right)) + 1;

        int balance = GetBalance(node);
        if (balance > 1 && score < node.left.score) return RotateRight(node);
        if (balance < -1 && score > node.right.score) return RotateLeft(node);
        if (balance > 1 && score > node.left.score) { node.left = RotateLeft(node.left); return RotateRight(node); }
        if (balance < -1 && score < node.right.score) { node.right = RotateRight(node.right); return RotateLeft(node); }

        return node;
    }

    // Traversals that return LISTS (for UI)
    public List<(string, int)> InOrder()
    {
        var result = new List<(string, int)>();
        InOrder(root, result);
        return result;
    }

    private void InOrder(AVLNode node, List<(string, int)> list)
    {
        if (node == null) return;
        InOrder(node.right, list); // right first for descending
        list.Add((node.playerName, node.score));
        InOrder(node.left, list);
    }

    public List<(string, int)> PreOrderList()
    {
        var list = new List<(string, int)>();
        PreOrder(root, list);
        return list;
    }

    private void PreOrder(AVLNode node, List<(string, int)> list)
    {
        if (node == null) return;
        list.Add((node.playerName, node.score));
        PreOrder(node.left, list);
        PreOrder(node.right, list);
    }

    public List<(string, int)> PostOrderList()
    {
        var list = new List<(string, int)>();
        PostOrder(root, list);
        return list;
    }

    private void PostOrder(AVLNode node, List<(string, int)> list)
    {
        if (node == null) return;
        PostOrder(node.left, list);
        PostOrder(node.right, list);
        list.Add((node.playerName, node.score));
    }

    public List<(string, int)> LevelOrderList()
    {
        var list = new List<(string, int)>();
        if (root == null) return list;
        Queue<AVLNode> queue = new Queue<AVLNode>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            list.Add((node.playerName, node.score));
            if (node.left != null) queue.Enqueue(node.left);
            if (node.right != null) queue.Enqueue(node.right);
        }
        return list;
    }
}
