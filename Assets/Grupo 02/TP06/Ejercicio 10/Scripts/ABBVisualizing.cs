using UnityEngine;
using TMPro;

public class ABBVisualizerController : MonoBehaviour
{
    public TMP_InputField inputField;
    public UnityABBVisualizer visualizer;

    private MyABBTree tree;

    void Start()
    {
        tree = new MyABBTree();

        tree.OnNodeCreated += visualizer.CreateVisual;

        int[] values = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };
        foreach (int v in values) tree.Insert(v);

        visualizer.Redraw(tree.Root);
    }

    public void InsertFromUI()
    {
        if (int.TryParse(inputField.text, out int v))
        {
            tree.Insert(v);
            visualizer.Redraw(tree.Root);
        }
        inputField.text = "";
    }
}
