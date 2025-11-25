using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Inventory player1;
    public Inventory player2;
    public TMP_Text resultText;

    private LinkedList<Item> allItems;

    void Start()
    {
        // Create 40 items in the linked list
        allItems = new LinkedList<Item>();
        for (int i = 0; i < 40; i++)
            allItems.AddLast(new Item($"Item {i + 1}", Random.Range(10f, 200f)));

        player1.InitializeInventory(allItems);
        player2.InitializeInventory(allItems);
    }

    // Show items in common (unique items only)
    public void ShowCommonItems()
    {
        var common = player1.uniqueItems.Intersect(player2.uniqueItems).ToList();
        ShowResult("Ítems en común:", common);
    }

    // Show unique items per player 
    public void ShowUniqueItems()
    {
        var uniqueToP1 = player1.uniqueItems.Except(player2.uniqueItems).ToList();
        var uniqueToP2 = player2.uniqueItems.Except(player1.uniqueItems).ToList();

        string result = "Ítems únicos de Jugador 1:\n";
        result += uniqueToP1.Count > 0 ? string.Join("\n", uniqueToP1.Select(i => i.ToString())) : "(Ninguno)\n";
        result += "\n\nÍtems únicos de Jugador 2:\n";
        result += uniqueToP2.Count > 0 ? string.Join("\n", uniqueToP2.Select(i => i.ToString())  : "(Ninguno)";

        resultText.text = result;
    }

    // Show items that are not in either inventory
    public void ShowMissingItems()
    {
        // unique set of all items
        var allSet = new Set<Item>(allItems);

        // union of both inventories' unique sets
        var owned = new Set<Item>(player1.uniqueItems);
        owned.UnionWith(player2.uniqueItems);

        var missing = allSet.Except(owned).ToList();
        ShowResult("Ítems que ninguno tiene:", missing);
    }

    // Show count of items
    public void ShowCounts()
    {
        resultText.text = $"Jugador 1: {player1.slotItems.Count} ítems\n" + $"Jugador 2: {player2.slotItems.Count} ítems";
    }

    private void ShowResult(string title, List<Item> items)
    {
        if (items.Count == 0)
        {
            resultText.text = $"{title}\n(Ninguno)";
            return;
        }

        resultText.text = title + "\n" + string.Join("\n", items.Select(i => i.ToString()));
    }

    public void Regenerate()
    {
        player1.InitializeInventory(allItems);
        player2.InitializeInventory(allItems);
    }
}
