using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public interface ISimpleList<T>
    {
        //Must give access to element at the given "index" of the list 
        public T this[int index] { get; set; }

        //Must indicate the amount of elements saved in the list
        public int Count { get; }

        //Must add "item" at the end of the list
        public void Add(T item);

        //Must add all elemens of the "collection" to the end of the list
        public void AddRange(T[] collection);

        //Must remove the first element that's the same as "item"
        //Also must return true if an element could be removed, false if not
        public bool Remove(T item);

        //Must wipe the entire list
        public void Clear();
    }
