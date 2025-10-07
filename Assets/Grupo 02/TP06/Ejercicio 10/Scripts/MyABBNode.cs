using System;
using UnityEngine;

// Representa un nodo del árbol binario de búsqueda (ABB)
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
