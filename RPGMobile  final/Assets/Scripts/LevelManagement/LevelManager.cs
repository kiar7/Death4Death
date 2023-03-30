using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class LevelManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] ItemsManager[] itemsInScene;

    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManagerInstance.onLoading = false;
        Player.playerInstance.CheckIfLoading();

        bottomLeftEdge = tilemap.localBounds.min + new Vector3(0.5f, 1f, 0f);
        topRightEdge = tilemap.localBounds.max + new Vector3(-0.5f, -1f, 0f);

        Player.playerInstance.SetLimit(bottomLeftEdge, topRightEdge);

        foreach(ItemsManager itemInScene in itemsInScene)
        {
            foreach(ItemsManager itemInInventory in Inventory.inventoryInstance.GetItemsList())
            {
                if(itemsInScene.Equals(itemInInventory))
                {
                    itemInScene.gameObject.SetActive(false);
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
