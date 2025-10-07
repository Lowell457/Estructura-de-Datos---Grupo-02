using UnityEngine;

public class ABBVisualizer : MonoBehaviour
{
    [Header("Prefab del nodo (con TextMeshPro)")]
    public GameObject nodePrefab;

    void Start()
    {
        MyABBTree tree = new MyABBTree(nodePrefab);

        int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };

        foreach (int value in myArray)
            tree.Insert(value);

        Debug.Log("Árbol visual generado.");
    }
}
