using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public ItemsManager itemOnButton;

    public void Press()
    {
        
        if(MenuManager.menuInstance.menu.activeInHierarchy)
        {
            MenuManager.menuInstance.itemName.text = itemOnButton.itemName;
            MenuManager.menuInstance.itemDescription.text = itemOnButton.itemDescription;
            MenuManager.menuInstance.activeItem = itemOnButton;
        }

        if(ShopManager.instance.shopMenu.activeInHierarchy)
        {
            if(ShopManager.instance.buyPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedBuyItem(itemOnButton);
            }
            else if(ShopManager.instance.sellPanel.activeInHierarchy)
            {
                ShopManager.instance.SelectedSellItem(itemOnButton);
            }
        }
    }
}

