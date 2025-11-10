using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public Inventory player1;
    public Inventory player2;
    public TMP_Text resultText;

    private List<Item> allItems;

    void Start()
    {
        // Genereate 40 unique items
        allItems = new List<Item>();
        for (int i = 0; i < 40; i++)
            allItems.Add(new Item($"Item {i+1}", Random.Range(10f, 200f)));

        // Initialize both inventories
        player1.InitializeInventory(allItems);
        player2.InitializeInventory(allItems);
    }

    public void ShowCommonItems()
    {
        var common = player1.items.Intersect(player2.items).ToList();
        ShowResult("Ítems en común:", common);
    }

    public void ShowUniqueItems()
    {
        // Items only Player 1 has
        var uniqueToP1 = player1.items.Except(player2.items).ToList();

        // Items only Player 2 has
        var uniqueToP2 = player2.items.Except(player1.items).ToList();

        // Build text output
        string result = "Ítems únicos de Jugador 1:\n";
        if (uniqueToP1.Count > 0)
            result += string.Join("\n", uniqueToP1.Select(i => i.ToString()));
        else
            result += "(Ninguno)\n";

        result += "\n\nÍtems únicos de Jugador 2:\n";
        if (uniqueToP2.Count > 0)
            result += string.Join("\n", uniqueToP2.Select(i => i.ToString()));
        else
            result += "(Ninguno)";

        resultText.text = result;
    }


    public void ShowMissingItems()
    {
        var allNames = new HashSet<Item>(allItems);
        var owned = player1.items.Union(player2.items);
        var missing = allNames.Except(owned).ToList();
        ShowResult("Ítems que ninguno tiene:", missing);
    }

    public void ShowCounts()
    {
        resultText.text = $"Jugador 1: {player1.items.Count} ítems\n" +
                          $"Jugador 2: {player2.items.Count} ítems";
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
