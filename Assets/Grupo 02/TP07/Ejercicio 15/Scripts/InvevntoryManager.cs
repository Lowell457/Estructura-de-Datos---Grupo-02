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
        // Create 40 items in a linked list
        allItems = new LinkedList<Item>();
        for (int i = 0; i < 40; i++)
            allItems.AddLast(new Item($"Item {i + 1}", Random.Range(10f, 200f)));

        player1.InitializeInventory(allItems);
        player2.InitializeInventory(allItems);
    }

    // SHOW COMMON ITEMS (unique items only)
    public void ShowCommonItems()
    {
        var common = player1.uniqueItems
            .Intersect(player2.uniqueItems)
            .ToList();

        ShowResult("Ítems en común:", common);
    }

    // SHOW UNIQUE ITEMS PER PLAYER
    public void ShowUniqueItems()
    {
        var uniqueToP1 = player1.uniqueItems.Except(player2.uniqueItems).ToList();
        var uniqueToP2 = player2.uniqueItems.Except(player1.uniqueItems).ToList();

        string result = "Ítems únicos de Jugador 1:\n";
        result += uniqueToP1.Count > 0
            ? string.Join("\n", uniqueToP1.Select(i => i.ToString()))
            : "(Ninguno)\n";

        result += "\n\nÍtems únicos de Jugador 2:\n";
        result += uniqueToP2.Count > 0
            ? string.Join("\n", uniqueToP2.Select(i => i.ToString()))
            : "(Ninguno)";

        resultText.text = result;
    }

    // SHOW ITEMS THAT NEITHER PLAYER HAS
    public void ShowMissingItems()
    {
        // unique set of all items
        var allSet = new HashSet<Item>(allItems);

        // union of both inventories' unique sets
        var owned = new HashSet<Item>(player1.uniqueItems);
        owned.UnionWith(player2.uniqueItems);

        var missing = allSet.Except(owned).ToList();
        ShowResult("Ítems que ninguno tiene:", missing);
    }

    // SHOW COUNT
    public void ShowCounts()
    {
        resultText.text =
            $"Jugador 1: {player1.slotItems.Count} ítems\n" +
            $"Jugador 2: {player2.slotItems.Count} ítems";
    }

    // PRIVATE HELPER FOR DISPLAY
    private void ShowResult(string title, List<Item> items)
    {
        if (items.Count == 0)
        {
            resultText.text = $"{title}\n(Ninguno)";
            return;
        }

        resultText.text = title + "\n" + string.Join("\n", items.Select(i => i.ToString()));
    }

    // REGENERATE BOTH INVENTORIES
    public void Regenerate()
    {
        player1.InitializeInventory(allItems);
        player2.InitializeInventory(allItems);
    }
}
