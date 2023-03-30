using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets instance;

    [SerializeField] ItemsManager[] itemsAvailable;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public ItemsManager GetItemAsset(string itemToGetName)
    {
        foreach(ItemsManager item in itemsAvailable)
        {
            if(item.itemName == itemToGetName)
            {
                return item;
            }
        }
        return null;
    }
}
