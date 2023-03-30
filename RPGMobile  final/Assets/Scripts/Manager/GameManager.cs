using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public bool gameMenuOpened, dialogBoxOpened, shopOpened, onLoading;
    public int currentCoins;

    [SerializeField] PlayerStats[] playerStats; 

    // Start is called before the first frame update
    void Start()
    {
        if (gameManagerInstance != null && gameManagerInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameManagerInstance = this;
        }
        DontDestroyOnLoad(gameObject);

        playerStats = FindObjectsOfType<PlayerStats>();

        if(SceneManager.GetActiveScene().name == "LoadingScene")
        {
            onLoading = true;
        }
        else
        {
            onLoading = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("saved");
            SaveDate();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("loaded");
            LoadData();
        }

        if (gameMenuOpened || dialogBoxOpened || shopOpened)
        {
            Player.playerInstance.deactivateMovement = true;
        }
        else
        {
            Player.playerInstance.deactivateMovement = false;
        }
    }

    public PlayerStats[] GetPlayerStats()
    {
        return playerStats;
    }

    public void SaveDate()
    {
        SavingPlayerPosition();
        SavingPlayerStats();

        PlayerPrefs.SetString("Current_Scene", SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name);

        PlayerPrefs.SetInt("Number_Of_Item", Inventory.inventoryInstance.GetItemsList().Count);

        for(int i = 0; i < Inventory.inventoryInstance.GetItemsList().Count; i++)
        {
            ItemsManager itemInInventory = Inventory.inventoryInstance.GetItemsList()[i];
            PlayerPrefs.SetString("Items_" + i + "_Name", itemInInventory.itemName);

            if (itemInInventory.isStackable)
            {
                PlayerPrefs.SetInt("Item_" + i + "_Amount", itemInInventory.amount);
            }
        }
    }

    private static void SavingPlayerPosition()
    {
        PlayerPrefs.SetFloat("Player_Pos_X", Player.playerInstance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Pos_Y", Player.playerInstance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Pos_Z", Player.playerInstance.transform.position.z);
    }

    private void SavingPlayerStats()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].gameObject.activeInHierarchy)
            {
                PlayerPrefs.SetInt("Player_ " + playerStats[i].playerName + "_Active", 1);
            }
            else
            {
                PlayerPrefs.SetInt("Player_ " + playerStats[i].playerName + "_Active", 0);
            }

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_Level", playerStats[i].playerLevel);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_CurrXP", playerStats[i].currentXP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_MaxHP", playerStats[i].maxHP);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_CurrHP", playerStats[i].currentHP);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_MaxMana", playerStats[i].maxMana);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_CurrMana", playerStats[i].currentMana);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_Dexterity", playerStats[i].dexterity);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_Defence", playerStats[i].defence);

            PlayerPrefs.SetString("Player_" + playerStats[i].playerName + "_EquippedWeapon", playerStats[i].equippedWeaponName);
            PlayerPrefs.SetString("Player_" + playerStats[i].playerName + "_EquippedArmor", playerStats[i].equippedArmorName);

            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_WeaponPower", playerStats[i].weaponPower);
            PlayerPrefs.SetInt("Player_" + playerStats[i].playerName + "_ArmorDefence", playerStats[i].armorDef);
        }
    }

    public void LoadData()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));
        LoadingPlayerPosition();
        LoadingPlayerStats();

        for(int i = 0; i < PlayerPrefs.GetInt("Number_Of_Item"); i++)
        {
            string itemName = PlayerPrefs.GetString("Items_" + i + "_Name");
            Debug.Log(itemName);

            ItemsManager itemToAdd = ItemAssets.instance.GetItemAsset(itemName);
            Debug.Log(itemToAdd);

            int itemAmount = 0;
            if (PlayerPrefs.HasKey("Item_ + i + _Amount"))
            {
                itemAmount = PlayerPrefs.GetInt("Item_" + i + "_Amount");
            }

            Inventory.inventoryInstance.AddItems(itemToAdd);
            if (itemToAdd.isStackable && itemAmount > 1)
            {
                itemToAdd.amount = itemAmount;
            }
        }
    }

    private void LoadingPlayerStats()
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            if (PlayerPrefs.GetInt("Player_ " + playerStats[i].playerName + "_Active") == 0)
            {
                playerStats[i].gameObject.SetActive(false);
            }
            else
            {
                playerStats[i].gameObject.SetActive(true);

            }

            playerStats[i].playerLevel = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_Level");
            playerStats[i].currentXP = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_CurrXP");

            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_MaxHP");
            playerStats[i].currentHP = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_CurrHP");

            playerStats[i].maxMana = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_MaxMana");
            playerStats[i].currentMana = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_CurrMana");

            playerStats[i].dexterity = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_Dexterity");
            playerStats[i].maxHP = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_Defence");

            playerStats[i].equippedWeaponName = PlayerPrefs.GetString("Player_" + playerStats[i].playerName + "_EquippedWeapon");
            playerStats[i].equippedArmorName = PlayerPrefs.GetString("Player_" + playerStats[i].playerName + "_EquippedArmor");

            playerStats[i].weaponPower = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_WeaponPower");
            playerStats[i].armorDef = PlayerPrefs.GetInt("Player_" + playerStats[i].playerName + "_ArmorDefence");
        }
    }

    private static void LoadingPlayerPosition()
    {
        Player.playerInstance.transform.position = new Vector3(
            PlayerPrefs.GetFloat("Player_Pos_X"),
            PlayerPrefs.GetFloat("Player_Pos_Y"),
            PlayerPrefs.GetFloat("Player_Pos_Z")
            );
    }
}
