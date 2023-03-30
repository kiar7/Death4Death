using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemsManager> itemsList;

    public static Inventory inventoryInstance;

    // Start is called before the first frame update
    void Start()
    {
        inventoryInstance = this;

        itemsList = new List<ItemsManager>();//costruttore della lista, la sto instanziando
        Debug.Log("new inventory has been created");
    }

    public void AddItems(ItemsManager item)
    {
        if(item.isStackable)
        {
            bool itemAlreadyInInventory = false;//di default sto raccogliendo un item che non ho nell'inventario

            //questo for controlla se già possiedo l'item che ho raccolto
            foreach(ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == item.itemName)
                {
                    itemInInventory.amount += item.amount;
                    itemAlreadyInInventory = true;//se il for trova un item che sto cercando di inserire nell'inventario marca vero il fatto che ora già possiedo quell'item
                    Debug.Log(itemAlreadyInInventory);
                }
            }

            //se il for non trova nessun item uguale a quello raccolto lo aggiungo alla lista
            if(!itemAlreadyInInventory)
            {
                itemsList.Add(item);
                Debug.Log(itemAlreadyInInventory);
            }
        }
        else
        {
            itemsList.Add(item);
        }
    }

    public void RemoveItemFromList(ItemsManager itemToRemove)
    {
        if(itemToRemove.isStackable)
        {
            ItemsManager inventoryItem = null;

            foreach(ItemsManager itemInInventory in itemsList)
            {
                if(itemInInventory.itemName == itemToRemove.itemName)
                {
                    itemInInventory.amount--;
                    inventoryItem = itemInInventory;
                }
            }

            if(inventoryItem != null && inventoryItem.amount <= 0)
            {
                itemsList.Remove(inventoryItem);
            }
        }
        else
        {
            itemsList.Remove(itemToRemove);
        }
    }

    public List<ItemsManager> GetItemsList()
    {
        return itemsList;
    }
}
