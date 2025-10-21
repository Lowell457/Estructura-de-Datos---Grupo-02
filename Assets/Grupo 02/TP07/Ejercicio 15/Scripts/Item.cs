using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public float price;

    public Item(string name, float price)
    {
        this.name = name;
        this.price = price;
    }

    public override bool Equals(object obj)
    {
        if (obj is Item other)
            return name == other.name;
        return false;
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }

    public override string ToString()
    {
        return $"{name} (${price})";
    }
}
