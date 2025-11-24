public class MyABBNode
{
    public int Value { get; }
    public MyABBNode Left { get; set; }
    public MyABBNode Right { get; set; }

    public MyABBNode(int value) => Value = value;
}
