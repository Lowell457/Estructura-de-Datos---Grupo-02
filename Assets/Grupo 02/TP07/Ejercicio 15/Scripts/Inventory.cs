using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Transform slotContainer;
    private TextMeshProUGUI[] slotTexts;

    // Unique items
    public HashSet<Item> uniqueItems = new HashSet<Item>();

    // Actual slot contents 
    public LinkedList<Item> slotItems = new LinkedList<Item>();

    private void Awake()
    {
        slotTexts = slotContainer.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void InitializeInventory(LinkedList<Item> allItems)
    {
        uniqueItems.Clear();
        slotItems.Clear();

        foreach (var text in slotTexts)
            text.text = "";

        Item[] arr = new Item[allItems.Count];
        allItems.CopyTo(arr, 0);

        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (Random.value <= 0.7f)
            {
                Item randomItem = arr[Random.Range(0, arr.Length)];
                slotItems.AddLast(randomItem);         // track actual slot
                uniqueItems.Add(randomItem);           // track uniqueness
                slotTexts[i].text = randomItem.name;
            }
        }
    }
}
