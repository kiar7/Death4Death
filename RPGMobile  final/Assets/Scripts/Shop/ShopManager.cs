using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    public GameObject shopMenu, buyPanel, sellPanel;

    public List<ItemsManager> itemsForSale;

    [SerializeField] TextMeshProUGUI currentCoinsText;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotBuyContainerParent;
    [SerializeField] Transform itemSlotSellContainerParent;

    [SerializeField] ItemsManager selectedItem;
    [SerializeField] TextMeshProUGUI buyItemName, buyItemDescription, buyItemValue;
    [SerializeField] TextMeshProUGUI SellItemName, SellItemDescription, SellItemValue;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenShopMenu()
    {
        currentCoinsText.text = "Coins: " +  GameManager.gameManagerInstance.currentCoins;
        MenuManager.menuInstance.HandleInputVisibility(false);
        shopMenu.SetActive(true);
        GameManager.gameManagerInstance.shopOpened = true;
    }

    public void CloseShopMenu()
    {
        shopMenu.SetActive(false);
        MenuManager.menuInstance.HandleInputVisibility(true);
        GameManager.gameManagerInstance.shopOpened = false;
    }

    public void OpenBuyPanel()
    {
        buyPanel.SetActive(true);
        sellPanel.SetActive(false);

        UpdateItemsInShop(itemSlotBuyContainerParent, itemsForSale);
    }
    
    public void OpenSellPanel()
    {
        buyPanel.SetActive(false);
        sellPanel.SetActive(true);

        UpdateItemsInShop(itemSlotSellContainerParent, Inventory.inventoryInstance.GetItemsList());
    }

    private void UpdateItemsInShop(Transform itemsSlotContainerParent, List<ItemsManager> itemsToLookThrough)
    {
        foreach (Transform itemSlot in itemsSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }
        foreach (ItemsManager item in itemsToLookThrough)
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemsSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("ItemImage").GetComponent<Image>();
            itemImage.sprite = item.itemSprite;

            TextMeshProUGUI itemAmountText = itemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();

            if (item.amount > 1)
            {
                itemAmountText.text = "";
            }
            else
            {
                itemAmountText.text = "";
            }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void SelectedBuyItem(ItemsManager itemToBuy)
    {
        selectedItem = itemToBuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.itemDescription;
        buyItemValue.text = "Value: " + selectedItem.valueInCoins;
    }

    public void SelectedSellItem(ItemsManager itemToSell)
    {
        selectedItem = itemToSell;
        SellItemName.text = selectedItem.itemName;
        SellItemDescription.text = selectedItem.itemDescription;
        SellItemValue.text = "Value: " + (int)(selectedItem.valueInCoins * 0.75f);
    }

    public void BuyItem()
    {
        if(GameManager.gameManagerInstance.currentCoins >= selectedItem.valueInCoins)
        {
            GameManager.gameManagerInstance.currentCoins -= selectedItem.valueInCoins;
            Inventory.inventoryInstance.AddItems(selectedItem);

            currentCoinsText.text = "Coins: " + GameManager.gameManagerInstance.currentCoins;
        }
    }

    public void SellItem()
    {
        if(selectedItem != null)
        {
            GameManager.gameManagerInstance.currentCoins += (int)(selectedItem.valueInCoins * 0.75f);
            Inventory.inventoryInstance.RemoveItemFromList(selectedItem);

            currentCoinsText.text = "Coins: " + GameManager.gameManagerInstance.currentCoins;
            selectedItem = null;
            OpenSellPanel();
        }
    }
}
