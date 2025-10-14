using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// ----------------- Nodo AVL -----------------
public class MyAVLNode : MyABBNode
{
    public int Height;

    public MyAVLNode(int value) : base(value)
    {
        Height = 1; // Un nodo nuevo tiene altura 1
    }
}

// ----------------- AVL Tree -----------------
public class MyAVLTree : MyABBTree
{
    public new MyAVLNode Root { get; private set; }

    public MyAVLTree(GameObject prefab) : base(prefab) { }

    // ----------------- Insert con balance -----------------
    public void InsertAVL(int value)
    {
        Root = InsertRecursiveAVL(Root, value);
    }

    private MyAVLNode InsertRecursiveAVL(MyAVLNode node, int value)
    {
        if (node == null)
        {
            MyAVLNode newNode = new MyAVLNode(value);
            CreateVisualNode(newNode);
            return newNode;
        }

        if (value < node.Value)
            node.Left = InsertRecursiveAVL((MyAVLNode)node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursiveAVL((MyAVLNode)node.Right, value);
        else
            return node; // No se permiten duplicados

        // Actualizar altura
        node.Height = 1 + Math.Max(GetHeight((MyAVLNode)node.Left), GetHeight((MyAVLNode)node.Right));

        // Obtener factor de balance
        int balance = GetBalanceFactor(node);

        // ----------------- Rotaciones -----------------
        // Rotación derecha
        if (balance > 1 && value < node.Left.Value)
            return RightRotate(node);

        // Rotación izquierda
        if (balance < -1 && value > node.Right.Value)
            return LeftRotate(node);

        // Rotación izquierda-derecha
        if (balance > 1 && value > node.Left.Value)
        {
            node.Left = LeftRotate((MyAVLNode)node.Left);
            return RightRotate(node);
        }

        // Rotación derecha-izquierda
        if (balance < -1 && value < node.Right.Value)
        {
            node.Right = RightRotate((MyAVLNode)node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    // ----------------- Rotaciones -----------------
    private MyAVLNode RightRotate(MyAVLNode y)
    {
        MyAVLNode x = (MyAVLNode)y.Left;
        MyAVLNode T2 = (MyAVLNode)x.Right;

        // Rotación
        x.Right = y;
        y.Left = T2;

        // Actualizar alturas
        y.Height = 1 + Math.Max(GetHeight((MyAVLNode)y.Left), GetHeight((MyAVLNode)y.Right));
        x.Height = 1 + Math.Max(GetHeight((MyAVLNode)x.Left), GetHeight((MyAVLNode)x.Right));

        return x;
    }

    private MyAVLNode LeftRotate(MyAVLNode x)
    {
        MyAVLNode y = (MyAVLNode)x.Right;
        MyAVLNode T2 = (MyAVLNode)y.Left;

        // Rotación
        y.Left = x;
        x.Right = T2;

        // Actualizar alturas
        x.Height = 1 + Math.Max(GetHeight((MyAVLNode)x.Left), GetHeight((MyAVLNode)x.Right));
        y.Height = 1 + Math.Max(GetHeight((MyAVLNode)y.Left), GetHeight((MyAVLNode)y.Right));

        return y;
    }

    // ----------------- Funciones de altura y balance -----------------
    private int GetHeight(MyAVLNode node)
    {
        return node != null ? node.Height : 0;
    }

    private int GetBalanceFactor(MyAVLNode node)
    {
        if (node == null) return 0;
        return GetHeight((MyAVLNode)node.Left) - GetHeight((MyAVLNode)node.Right);
    }

    // ----------------- Sobreescribir InOrder para AVL -----------------
    public void InOrderAVL(MyAVLNode node)
    {
        if (node == null) return;
        InOrderAVL((MyAVLNode)node.Left);
        Debug.Log("InOrderAVL: " + node.Value);
        InOrderAVL((MyAVLNode)node.Right);
    }
}
