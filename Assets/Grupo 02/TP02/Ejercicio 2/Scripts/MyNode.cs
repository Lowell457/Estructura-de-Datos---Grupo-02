using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyLinkedList
{
    public class MyNode<T>
    {
        public T Data { get; set; }
        public MyNode<T> Next { get; set; }
        public MyNode<T> Prev { get; set; }

        public MyNode(T data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }

        public override string ToString()
        {
            return Data?.ToString() ?? "null";
        }

        public bool IsEquals(T value)
        {
            if (Data == null && value == null) return true;
            return Data?.Equals(value) ?? false;
        }
    }
}