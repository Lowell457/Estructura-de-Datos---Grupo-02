using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MyStack<T>
{
    private T[] items;
    private int count;

    public int Count => count;

    public MyStack(int capacity = 4)
    {
        items = new T[capacity];
        count = 0;
    }

    public void Push(T item)
    {
        if (count == items.Length) Array.Resize(ref items, items.Length * 2);
        items[count++] = item;
    }

    public T Pop()
    {
        if (count == 0) throw new InvalidOperationException("Stack is empty");
        T item = items[--count];
        items[count] = default;
        return item;
    }

    public T Peek()
    {
        if (count == 0) throw new InvalidOperationException("Stack is empty");
        return items[count - 1];
    }

    public void Clear()
    {
        for (int i = 0; i < count; i++) items[i] = default;
        count = 0;
    }

    public bool TryPop(out T item)
    {
        if (count > 0)
        {
            item = Pop();
            return true;
        }
        item = default;
        return false;
    }

    public bool TryPeek(out T item)
    {
        if (count > 0)
        {
            item = Peek();
            return true;
        }
        item = default;
        return false;
    }
}
