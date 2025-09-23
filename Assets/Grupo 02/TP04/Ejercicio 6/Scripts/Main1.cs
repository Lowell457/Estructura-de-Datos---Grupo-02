using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class Main1 : MonoBehaviour
{
    public Transform storePanel1;        //Panel to hold buttons
    public GameObject itemButtonPrefab1; //Prefab with Button, Name, Price and Image
    public TMP_Dropdown sortDropdown;
    public TextMeshProUGUI playerMoneyText1;

    public Transform inventoryPanel1;
    public GameObject inventoryItemPrefab1;

    public int playerMoney1 = 100;

    public Sprite swordSprite1;
    public Sprite potionSprite1;
    public Sprite bowSprite1;
    public Sprite shieldSprite1;
    public Sprite arrowSprite1;
    public Sprite gunSprite1;

    // STORE & INVENTORY DATA
    private readonly Dictionary<int, Item1> storeItems1 = new Dictionary<int, Item1>();
    // IMPORTANT: named tuple both in declaration and instantiation to keep .item and .qty available
    private readonly Dictionary<int, (Item1 item, int qty)> playerInventory1 = new Dictionary<int, (Item1 item, int qty)>();
    public enum ItemSort { ID1, Name1, Price1, Rarity1, Type1 }
    private ItemSort currentSort = ItemSort.ID1;

    private void Start()
    {
        // Sample items
        var sword = new Item1 { id1 = 0, itemName1 = "Sword", price1 = 10, rarity1 = "Common", type1 = "Weapon", icon1 = swordSprite1 };
        var healthPotion = new Item1 {id1 = 1, itemName1 = "Health Potion", price1 = 5, rarity1 = "Common", type1 = "Consumable", icon1 = potionSprite1 };
        var bow = new Item1 { id1 = 2, itemName1 = "Bow", price1 = 15, rarity1 = "Rare", type1 = "Weapon", icon1 = bowSprite1 };
        var shield = new Item1 {id1 = 3, itemName1 = "Shield", price1 = 20, rarity1 = "Rare", type1 = "Weapon", icon1 = shieldSprite1 };
        var arrow = new Item1 {id1 = 4, itemName1 = "Arrow", price1 = 5, rarity1 = "Common", type1 = "Consumable", icon1 = arrowSprite1 };
        var gun = new Item1 {id1 = 5, itemName1 = "Missile", price1 = 100, rarity1 = "Epic", type1 = "Weapon", icon1 = gunSprite1 };

        storeItems1.Add(sword.id1, sword);
        storeItems1.Add(healthPotion.id1, healthPotion);
        storeItems1.Add(bow.id1, bow);
        storeItems1.Add(shield.id1, shield);
        storeItems1.Add(arrow.id1, arrow);
        storeItems1.Add(gun.id1, gun);

        LoadItemPrices();          // restore saved prices
        BuildSortDropdown();       // setup dropdown UI
        RefreshStoreUI();          // draw store with current sort
        UpdatePlayerMoneyUI();
    }

    // =======================
    // Dropdown (sorting)
    // =======================
    private void BuildSortDropdown()
    {
        if (sortDropdown == null) return;

        sortDropdown.ClearOptions();
        var options = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("ID"),
            new TMP_Dropdown.OptionData("Name"),
            new TMP_Dropdown.OptionData("Price"),
            new TMP_Dropdown.OptionData("Rarity"),
            new TMP_Dropdown.OptionData("Type")
        };
        sortDropdown.AddOptions(options);

        // Sync UI with current sort
        sortDropdown.onValueChanged.RemoveAllListeners();
        sortDropdown.onValueChanged.AddListener(OnSortDropdownChanged);
        sortDropdown.value = (int)currentSort;
        sortDropdown.RefreshShownValue();
    }

    private void OnSortDropdownChanged(int index)
    {
        currentSort = (ItemSort)index;
        RefreshStoreUI();
    }

    // =======================
    // Store UI
    // =======================
    private void RefreshStoreUI()
    {
        // Clear panel
        foreach (Transform child in storePanel1)
            Destroy(child.gameObject);

        // Sort items
        var list = new List<Item1>(storeItems1.Values);
        list.Sort(CompareItems);

        // Rebuild buttons
        foreach (var item in list)
            CreateStoreButton(item);
    }

    private int CompareItems(Item1 a, Item1 b)
    {
        switch (currentSort)
        {
            case ItemSort.ID1: return a.id1.CompareTo(b.id1);
            case ItemSort.Name1: return string.Compare(a.itemName1, b.itemName1, System.StringComparison.Ordinal);
            case ItemSort.Price1: return a.price1.CompareTo(b.price1);
            case ItemSort.Rarity1: return GetRarityRank(a.rarity1).CompareTo(GetRarityRank(b.rarity1));
            case ItemSort.Type1: return string.Compare(a.type1, b.type1, System.StringComparison.Ordinal);
            default: return 0;
        }
    }

    private int GetRarityRank(string rarity)
    {
        switch (rarity)
        {
            case "Common": 
                return 0;
            case "Rare": 
                return 1;
            case "Epic": 
                return 2;
            default: 
                return 4;
        }
    }

    private void CreateStoreButton(Item1 item)
    {
        var buttonObj = Instantiate(itemButtonPrefab1, storePanel1);

        buttonObj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.itemName1;
        buttonObj.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = "$" + item.price1;
        buttonObj.transform.Find("Icon").GetComponent<Image>().sprite = item.icon1;

        int itemId = item.id1;

        // Pointer click (LMB buy / RMB sell)
        var trigger = buttonObj.GetComponent<EventTrigger>() ?? buttonObj.gameObject.AddComponent<EventTrigger>();
        var clickEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerClick };
        clickEntry.callback.AddListener((data) =>
        {
            var ped = (PointerEventData)data;
            if (ped.button == PointerEventData.InputButton.Left)
                BuyItem(itemId);
            else if (ped.button == PointerEventData.InputButton.Right)
                SellItem(itemId);
        });
        trigger.triggers.Add(clickEntry);
    }

    // =======================
    // Inventory UI
    // =======================
    private void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryPanel1)
            Destroy(child.gameObject);

        foreach (var kv in playerInventory1)
        {
            var item = kv.Value.item;
            var qty = kv.Value.qty;

            var row = Instantiate(inventoryItemPrefab1, inventoryPanel1);
            row.GetComponent<TextMeshProUGUI>().text = $"{item.itemName1} x{qty}";
        }
    }

    // =======================
    // Buy / Sell
    // =======================
    private void BuyItem(int id)
    {
        if (!storeItems1.TryGetValue(id, out var item)) return;

        if (playerMoney1 < item.price1)
        {
            Debug.Log("Not enough money!");
            return;
        }

        playerMoney1 -= item.price1;

        if (playerInventory1.ContainsKey(id))
            playerInventory1[id] = (item, playerInventory1[id].qty + 1);
        else
            playerInventory1.Add(id, (item, 1));

        Debug.Log("Bought: " + item.itemName1);

        // Dynamic pricing: +10% on buy
        item.price1 = Mathf.CeilToInt(item.price1 * 1.10f);
        SaveItemPrice(item);

        UpdatePlayerMoneyUI();
        UpdateInventoryUI();
        RefreshStoreUI(); // keeps current sort
    }

    private void SellItem(int id)
    {
        if (!playerInventory1.ContainsKey(id)) return;

        var (item, qty) = playerInventory1[id];

        playerMoney1 += item.price1 / 2; // sell value = half current price
        qty--;
        if (qty > 0) playerInventory1[id] = (item, qty);
        else playerInventory1.Remove(id);

        Debug.Log("Sold: " + item.itemName1);

        // Dynamic pricing: -5% on sell (min 1)
        item.price1 = Mathf.Max(1, Mathf.FloorToInt(item.price1 * 0.95f));
        SaveItemPrice(item);

        UpdatePlayerMoneyUI();
        UpdateInventoryUI();
        RefreshStoreUI(); // keeps current sort
    }

    // =======================
    // Money UI
    // =======================
    private void UpdatePlayerMoneyUI()
    {
        playerMoneyText1.text = "Money: $" + playerMoney1;
    }

    // =======================
    // Persist prices
    // =======================
    private void SaveItemPrice(Item1 item)
    {
        PlayerPrefs.SetInt($"ItemPrice_{item.id1}", item.price1);
        PlayerPrefs.Save();
    }

    private void LoadItemPrices()
    {
        foreach (var kv in storeItems1)
        {
            int id = kv.Key;
            if (PlayerPrefs.HasKey($"ItemPrice_{id}"))
                kv.Value.price1 = PlayerPrefs.GetInt($"ItemPrice_{id}");
        }
    }
}