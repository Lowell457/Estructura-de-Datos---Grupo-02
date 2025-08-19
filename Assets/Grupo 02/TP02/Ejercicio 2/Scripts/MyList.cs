using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace MyLinkedList
{
    public class MyList<T>
    {
        private MyNode<T> root;
        private MyNode<T> tail;

        public int Count { get; private set; }

        public MyList()
        {
            root = null;
            tail = null;
            Count = 0;
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                MyNode<T> current = root;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                return current.Data;
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                MyNode<T> current = root;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
                current.Data = value;
            }
        }

        public void Add(T value)
        {
            MyNode<T> newNode = new MyNode<T>(value);
            if (IsEmpty())
            {
                root = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }
            Count++;
        }

        public void AddRange(MyList<T> values)
        {
            for (int i = 0; i < values.Count; i++)
            {
                Add(values[i]);
            }
        }

        public void AddRange(T[] values)
        {
            foreach (var v in values)
            {
                Add(v);
            }
        }

        public bool Remove(T value)
        {
            MyNode<T> current = root;
            while (current != null)
            {
                if (current.IsEquals(value))
                {
                    if (current == root)
                    {
                        root = root.Next;
                        if (root != null) root.Prev = null;
                    }
                    else if (current == tail)
                    {
                        tail = tail.Prev;
                        if (tail != null) tail.Next = null;
                    }
                    else
                    {
                        current.Prev.Next = current.Next;
                        current.Next.Prev = current.Prev;
                    }

                    Count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();

            MyNode<T> current = root;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }

            if (current == root)
            {
                root = root.Next;
                if (root != null) root.Prev = null;
            }
            else if (current == tail)
            {
                tail = tail.Prev;
                if (tail != null) tail.Next = null;
            }
            else
            {
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
            }

            Count--;
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();

            MyNode<T> newNode = new MyNode<T>(value);

            if (index == 0)
            {
                if (IsEmpty())
                {
                    root = newNode;
                    tail = newNode;
                }
                else
                {
                    newNode.Next = root;
                    root.Prev = newNode;
                    root = newNode;
                }
            }
            else if (index == Count)
            {
                Add(value);
                return;
            }
            else
            {
                MyNode<T> current = root;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                newNode.Prev = current.Prev;
                newNode.Next = current;
                current.Prev.Next = newNode;
                current.Prev = newNode;
            }

            Count++;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public void Clear()
        {
            root = null;
            tail = null;
            Count = 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            MyNode<T> current = root;

            sb.Append("[");
            while (current != null)
            {
                sb.Append(current.ToString());
                if (current.Next != null) sb.Append(", ");
                current = current.Next;
            }
            sb.Append("]");

            return sb.ToString();
        }
    }
}