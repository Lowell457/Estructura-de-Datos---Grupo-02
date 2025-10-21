using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public Transform slotContainer;   // Asigná aquí el GameObject "ItemContainer"
    private TextMeshProUGUI[] slotTexts;

    public HashSet<Item> items = new HashSet<Item>();

    private void Awake()
    {
        // Busca automáticamente todos los TMP_Text dentro del contenedor
        slotTexts = slotContainer.GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void InitializeInventory(List<Item> allItems)
    {
        items.Clear();

        // Limpia el texto de cada slot
        foreach (var text in slotTexts)
            text.text = "";

        // 70% de chance de llenar un slot
        for (int i = 0; i < slotTexts.Length; i++)
        {
            if (Random.value <= 0.7f)
            {
                Item randomItem = allItems[Random.Range(0, allItems.Count)];
                items.Add(randomItem);
                slotTexts[i].text = randomItem.name;
            }
        }
    }
}
