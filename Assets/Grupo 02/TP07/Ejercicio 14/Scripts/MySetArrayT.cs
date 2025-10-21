using System;
using System.Collections.Generic;
using UnityEngine;

public class MySetArray<T> : MySet<T>
{
    private T[] elements;
    private int count;

    public MySetArray()
    {
        elements = new T[10];
        count = 0;
    }

    public override void Add(T item)
    {
        if (Contains(item)) return;

        if (count >= elements.Length)
        {
            Array.Resize(ref elements, elements.Length * 2);
        }
        elements[count++] = item;
    }

    public override void Remove(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i], item))
            {
                elements[i] = elements[count - 1]; // reemplaza por el último
                count--;
                return;
            }
        }
    }

    public override void Clear()
    {
        count = 0;
        elements = new T[10];
    }

    public override bool Contains(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(elements[i], item))
                return true;
        }
        return false;
    }

    public override void Show()
    {
        Debug.Log(ToString());
    }

    public override string ToString()
    {
        string result = "{ ";
        for (int i = 0; i < count; i++)
        {
            result += elements[i];
            if (i < count - 1) result += ", ";
        }
        result += " }";
        return result;
    }

    public override int Cardinality()
    {
        return count;
    }

    public override bool IsEmpty()
    {
        return count == 0;
    }

    public override MySet<T> Union(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();

        for (int i = 0; i < count; i++)
            result.Add(elements[i]);

        string[] parts = other.ToString().Trim('{', '}', ' ').Split(',');
        foreach (var p in parts)
        {
            if (string.IsNullOrWhiteSpace(p)) continue;
            T val = (T)Convert.ChangeType(p.Trim(), typeof(T));
            result.Add(val);
        }

        return result;
    }

    public override MySet<T> Intersect(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
        {
            if (other.Contains(elements[i]))
                result.Add(elements[i]);
        }
        return result;
    }

    public override MySet<T> Difference(MySet<T> other)
    {
        MySetArray<T> result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
        {
            if (!other.Contains(elements[i]))
                result.Add(elements[i]);
        }
        return result;
    }
}
