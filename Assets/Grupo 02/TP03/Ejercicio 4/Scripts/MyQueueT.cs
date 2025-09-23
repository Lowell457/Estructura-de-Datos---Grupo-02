using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

public class MyQueue<T>
{
    private class Node
    {
        public T Data;
        public Node Next;
        public Node(T data) { Data = data; }
    }

    private Node head; // first element (front)
    private Node tail; // last element (rear)
    private int count;

    public int Count => count;

    public void Enqueue(T item)
    {
        Node newNode = new Node(item);
        if (count == 0)
        {
            head = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            tail = newNode;
        }
        count++;
    }

    public T Dequeue()
    {
        if (count == 0)
            throw new InvalidOperationException("Queue is empty.");

        T value = head.Data;
        head = head.Next;
        count--;

        if (count == 0) tail = null;
        return value;
    }

    public T Peek()
    {
        if (count == 0)
            throw new InvalidOperationException("Queue is empty.");
        return head.Data;
    }

    public void Clear()
    {
        head = tail = null;
        count = 0;
    }

    public T[] ToArray()
    {
        T[] array = new T[count];
        Node current = head;
        int i = 0;
        while (current != null)
        {
            array[i++] = current.Data;
            current = current.Next;
        }
        return array;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        Node current = head;
        while (current != null)
        {
            sb.Append(current.Data);
            if (current.Next != null) sb.Append(" -> ");
            current = current.Next;
        }
        return sb.ToString();
    }

    public bool TryDequeue(out T item)
    {
        if (count > 0)
        {
            item = Dequeue();
            return true;
        }
        item = default(T);
        return false;
    }

    public bool TryPeek(out T item)
    {
        if (count > 0)
        {
            item = Peek();
            return true;
        }
        item = default(T);
        return false;
    }
}
