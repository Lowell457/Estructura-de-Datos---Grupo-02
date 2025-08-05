using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleList<T> : ISimpleList<T>
{
    T[] array;
    public T this[int index] { get { return array[index]; } set { array[index] = value; } }

    public int Count => throw new System.NotImplementedException();

    public void Add(T item)
    {
        throw new System.NotImplementedException();
    }

    public void AddRange(T[] collection)
    {
        throw new System.NotImplementedException();
    }

    public void Clear()
    {
        throw new System.NotImplementedException();
    }

    public bool Remove(T item)
    {
        throw new System.NotImplementedException();
    }

    /*
     * Borren esto antes de entregar
    public T Get(int index)
    {
        return array[index];
    }

    public void Set(int index, T value)
    {
        array[index] = value;
    }
    */
}
