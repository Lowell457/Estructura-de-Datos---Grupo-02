using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyLinkedList;

public class TestMyList : MonoBehaviour
{
    void Start()
    {
        MyList<int> numeros = new MyList<int>();

        numeros.Add(10);
        numeros.Add(20);
        numeros.Add(30);
        Debug.Log("Lista después de Add: " + numeros);

        numeros.Insert(1, 15);
        Debug.Log("Lista después de Insert(1,15): " + numeros);

        numeros.Remove(20);
        Debug.Log("Lista después de Remove(20): " + numeros);

        numeros.RemoveAt(0);
        Debug.Log("Lista después de RemoveAt(0): " + numeros);

        int[] arrayValores = { 40, 50, 60 };
        numeros.AddRange(arrayValores);
        Debug.Log("Lista después de AddRange(array): " + numeros);

        MyList<int> otros = new MyList<int>();
        otros.Add(70);
        otros.Add(80);
        numeros.AddRange(otros);
        Debug.Log("Lista después de AddRange(MyList): " + numeros);

        Debug.Log("Count: " + numeros.Count);

        numeros.Clear();
        Debug.Log("Lista después de Clear: " + numeros);
    }
}
