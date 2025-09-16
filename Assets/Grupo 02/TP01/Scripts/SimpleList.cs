using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Generic simple list that manages an internal array and doubles capacity when full.
/// </summary>
public class SimpleList<T> : ISimpleList<T>
{
    private T[] items;
    private int count;

    public SimpleList(int capacity = 1)
    {
        if (capacity < 1) capacity = 1;
        items = new T[capacity];
        count = 0;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count) throw new IndexOutOfRangeException();
            items[index] = value;
        }
    }

    public int Count => count;

    public void Add(T item)
    {
        EnsureCapacity();
        items[count++] = item;
    }

    public void AddRange(T[] collection)
    {
        if (collection == null) return;
        foreach (var item in collection) Add(item);
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index == -1) return false;

        // Shift left
        for (int i = index; i < count - 1; i++)
            items[i] = items[i + 1];

        count--;
        items[count] = default(T); // clear last slot
        return true;
    }

    public void Clear()
    {
        for (int i = 0; i < count; i++)
            items[i] = default(T);
        count = 0;
    }

    private void EnsureCapacity()
    {
        if (items == null || items.Length == 0)
        {
            items = new T[1];
            return;
        }

        if (count >= items.Length)
        {
            int newSize = items.Length * 2;
            if (newSize == 0) newSize = 1;
            T[] newArray = new T[newSize];
            Array.Copy(items, 0, newArray, 0, count);
            items = newArray;
        }
    }

    private int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < count; i++)
            if (comparer.Equals(items[i], item)) return i;
        return -1;
    }

    public override string ToString()
    {
        if (count == 0) return "(empty)";

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < count; i++)
        {
            if (i > 0) sb.Append(", ");
            sb.Append(items[i] != null ? items[i].ToString() : "null");
        }
        return sb.ToString();
    }
}
