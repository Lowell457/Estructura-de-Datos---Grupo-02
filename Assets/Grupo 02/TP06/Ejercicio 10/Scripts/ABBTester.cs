using UnityEngine;

public class ABBTester : MonoBehaviour
{
    // Prefab visual del nodo (asignalo en el Inspector)
    public GameObject nodePrefab;

    void Start()
    {
        // Creamos el árbol pasando el prefab
        MyABBTree tree = new MyABBTree(nodePrefab);

        int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };

        foreach (int v in myArray)
            tree.Insert(v);

        Debug.Log("Altura del árbol: " + tree.GetHeight(tree.Root));
        Debug.Log("Factor de balance (raíz): " + tree.GetBalanceFactor(tree.Root));

        Debug.Log("----- InOrder -----");
        tree.InOrder(tree.Root);

        Debug.Log("----- PreOrder -----");
        tree.PreOrder(tree.Root);

        Debug.Log("----- PostOrder -----");
        tree.PostOrder(tree.Root);

        Debug.Log("----- LevelOrder -----");
        tree.LevelOrder(tree.Root);
    }
}
