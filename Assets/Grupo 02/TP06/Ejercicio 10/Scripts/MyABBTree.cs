using System;

public class MyABBTree
{
    public MyABBNode Root { get; private set; }

    public event Action<MyABBNode> OnNodeCreated;

    public void Insert(int value)
    {
        Root = InsertRecursive(Root, value);
    }

    private MyABBNode InsertRecursive(MyABBNode node, int value)
    {
        if (node == null)
        {
            var newNode = new MyABBNode(value);
            OnNodeCreated?.Invoke(newNode); // notifies visualizer
            return newNode;
        }

        if (value < node.Value)
            node.Left = InsertRecursive(node.Left, value);
        else if (value > node.Value)
            node.Right = InsertRecursive(node.Right, value);

        return node;
    }

    public int GetHeight(MyABBNode node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    public int GetBalanceFactor(MyABBNode node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }
}
