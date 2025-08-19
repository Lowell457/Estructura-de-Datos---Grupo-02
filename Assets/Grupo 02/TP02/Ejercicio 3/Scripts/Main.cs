using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static UnityEditor.Progress;

public class Main : MonoBehaviour
{
    public Transform storePanel;        //Panel to hold buttons
    public GameObject itemButtonPrefab; //Prefab with Button, Name, Price and Image
    public TextMeshProUGUI playerMoneyText;

    public Transform inventoryPanel;
    public GameObject inventoryItemPrefab;

    public int playerMoney = 100;

    public Sprite swordSprite;
    public Sprite potionSprite;

    //Store dictionary: key = itemID, Value = Item
    public Dictionary<int, Item> storeItems = new Dictionary<int, Item>();

    //Player inventory: Key = itemID, Value = (Item, Quantity)
    public Dictionary<int, (Item, int)> playerInventory = new Dictionary<int, (Item, int)>();

    private void Start()
    {
        Item sword = new Item { id = 0, itemName = "Sword", price = 10, rarity = "Common", type = "Weapon", icon = swordSprite };
        Item health_potion = new Item { id = 1, itemName = "Health Potion", price = 5, rarity = "Common", type = "Consumable", icon = potionSprite };


        storeItems.Add(sword.id, sword);
        storeItems.Add(health_potion.id, health_potion);

        PopulateStoreUI();
        UpdatePlayerMoneyUI();
    }


    void PopulateStoreUI()
    {
        foreach (var kvp in storeItems)
        {
            Item item = kvp.Value;
            GameObject buttonObj = Instantiate(itemButtonPrefab, storePanel);

            //Setup UI
            buttonObj.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.itemName;
            buttonObj.transform.Find("Price").GetComponent<TextMeshProUGUI>().text = "$" + item.price;
            buttonObj.transform.Find("Icon").GetComponent<Image>().sprite = item.icon;

            int itemId = item.id; //store the ID for the lambda

            // Add EventTrigger for left/right click
            EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
            if (trigger == null) trigger = buttonObj.AddComponent<EventTrigger>();

            EventTrigger.Entry clickEntry = new EventTrigger.Entry();
            clickEntry.eventID = EventTriggerType.PointerClick;

            // Call BuyItem on left click, SellItem on right click
            clickEntry.callback.AddListener((data) =>
            {
                PointerEventData ped = (PointerEventData)data;

                if (ped.button == PointerEventData.InputButton.Left)
                    BuyItem(itemId);
                else if (ped.button == PointerEventData.InputButton.Right)
                    SellItem(itemId);
            });

            trigger.triggers.Add(clickEntry);
        }
    }

    void UpdateInventoryUI()
    {
        //Clear existing items
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        //Add each item in playerInventory
        foreach (var kvp in playerInventory)
        {
            Item item = kvp.Value.Item1;
            int quantity = kvp.Value.Item2;

            GameObject entry = Instantiate(inventoryItemPrefab, inventoryPanel);
            entry.GetComponent<TextMeshProUGUI>().text = item.itemName + " x" + quantity;
            ;
        }
    }


    void BuyItem (int id)
    {
        if (storeItems.ContainsKey(id))
        {
            Item item = storeItems[id];
            if (playerMoney >= item.price)
            {
                playerMoney -= item.price;

                if (playerInventory.ContainsKey(id))
                {
                    playerInventory[id] = (item, playerInventory[id].Item2 + 1);
                }
                else
                {
                    playerInventory.Add(id, (item, 1));
                }
                Debug.Log("Bought: " + item.itemName);
                UpdatePlayerMoneyUI();
                UpdateInventoryUI();
            }
            else
            {
                Debug.Log("Not enough money!");
            }
        }
    }

    void SellItem(int id)
    {
        if (storeItems.ContainsKey(id))
        {
            (Item item, int quantity) = playerInventory[id];
            playerMoney += item.price / 2; //Sells for half price
            quantity--;

            if (quantity > 0)
            {
                playerInventory[id] = (item, quantity);
            }
            else
            {
                playerInventory.Remove(id);
            }
            Debug.Log("Sold : " + item.itemName);
            UpdatePlayerMoneyUI();
            UpdateInventoryUI();
        }
    }

    void UpdatePlayerMoneyUI()
    {
        playerMoneyText.text = "Money: $" + playerMoney;
    }
}
