using UnityEngine;

public class AVLTester : MonoBehaviour
{
    private MyVisualAVLTreeWithLines tree;

    void Start()
    {
        // Encuentra el GameObject que tiene MyVisualAVLTreeWithLines
        tree = FindObjectOfType<MyVisualAVLTreeWithLines>();

        // Insertar nodos
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(5);
        tree.Insert(15);
        tree.Insert(25);
    }
}
