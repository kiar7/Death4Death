using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.TextCore.Text;

public class MenuManager : MonoBehaviour
{

    [SerializeField] Image imageToFade;

    public GameObject menu;

    

    [SerializeField] TextMeshProUGUI[] nameText, hpText, manaText, lvlText;
    [SerializeField] Slider[] xpSlider;
    [SerializeField] Image[] charaterImage;
    [SerializeField] GameObject[] charaterPanel;
    [SerializeField] GameObject[] statsButton;

    [SerializeField] TextMeshProUGUI statName, statHP, statMana, statDex, statDef, statEquippedWeaponPower, statEquippedArmorDefence, statEquippedWeaponName, statEquippedArmorName;
    [SerializeField] TextMeshProUGUI statWeaponDex, statArmorDef;
    [SerializeField] Image characterStatImage;

    [SerializeField] GameObject itemSlotContainer;
    [SerializeField] Transform itemSlotContainerParent;

    [SerializeField] GameObject characterChoicePanel;
    [SerializeField] TextMeshProUGUI[] itemsCharacterChoiceName;

    public GameObject inputPanel;

    public static MenuManager menuInstance;

    public TextMeshProUGUI itemName, itemDescription;
    public ItemsManager activeItem;

    private PlayerStats[] playerStats;
     
    // Start is called before the first frame update
    void Start()
    {
        menuInstance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            if(menu.activeInHierarchy)
            {
                menu.SetActive(false);
                MenuManager.menuInstance.HandleInputVisibility(true);
                GameManager.gameManagerInstance.gameMenuOpened = false;
            }
            else
            {
                UpdateStats();
               
                menu.SetActive(true);
                GameManager.gameManagerInstance.gameMenuOpened = true;
                MenuManager.menuInstance.HandleInputVisibility(false);
            }
        }
    }

    public void UpdateStats()
    {
        playerStats = GameManager.gameManagerInstance.GetPlayerStats();

        for(int i = 0; i < playerStats.Length; i++)
        {
            charaterPanel[i].SetActive(true);

            charaterImage[i].sprite = playerStats[i].charaterImage;

            nameText[i].text = playerStats[i].playerName;
            hpText[i].text = "HP: " + playerStats[i].currentHP + "/ " + playerStats[i].maxHP;
            manaText[i].text = "Mana: " + playerStats[i].currentMana + "/ " + playerStats[i].maxMana;

            xpSlider[i].maxValue = playerStats[i].xpForNextLevel[playerStats[i].playerLevel];
            xpSlider[i].value = playerStats[i].currentXP;
            
        }
    }

    public void StatsMenu()
    {
        for(int i = 0; i < playerStats.Length; i++)
        {
            statsButton[i].SetActive(true);

            statsButton[i].GetComponentInChildren<TextMeshProUGUI>().text = playerStats[i].playerName;
        }
        StatsMenuUpdate(0);
    }

    public void StatsMenuUpdate(int playerSelectedNumber)
    {
        PlayerStats playerSelected = playerStats[playerSelectedNumber];

        statName.text = playerSelected.playerName;
        statHP.text = playerSelected.currentHP.ToString() + "/" + playerSelected.maxHP;
        statMana.text = playerSelected.currentMana.ToString() + "/" + playerSelected.maxMana;
        statDex.text = playerSelected.dexterity.ToString();
        statDef.text = playerSelected.defence.ToString();
        characterStatImage.sprite = playerSelected.charaterImage;
        statEquippedWeaponPower.text = playerSelected.equippedWeaponName;
        statEquippedArmorDefence.text = playerSelected.equippedArmorName;
        statWeaponDex.text = playerSelected.weaponPower.ToString();
        statArmorDef.text = playerSelected.armorDef.ToString();

    }
    
    public void UpdateItemsInInventory()
    {
        //distruggiamo tutti gli slot prima di ricrearli per evitare bug
        foreach (Transform itemSlot in itemSlotContainerParent)
        {
            Destroy(itemSlot.gameObject);
        }

        foreach (ItemsManager item in Inventory.inventoryInstance.GetItemsList())
        {
            RectTransform itemSlot = Instantiate(itemSlotContainer, itemSlotContainerParent).GetComponent<RectTransform>();

            Image itemImage = itemSlot.Find("ItemImage").GetComponent<Image>();
            itemImage.sprite = item.itemSprite;

            TextMeshProUGUI itemAmountText = itemSlot.Find("AmountText").GetComponent<TextMeshProUGUI>();

            if(item.amount > 1)
            {
                itemAmountText.text  = item.amount.ToString();
            }
            else
            {
                itemAmountText.text = "";
            }

            itemSlot.GetComponent<ItemButton>().itemOnButton = item;
        }
    }

    public void DiscardItem()
    {
        Inventory.inventoryInstance.RemoveItemFromList(activeItem);
        UpdateItemsInInventory();
        AudioManager.instance.PlaySFX(3);
    }

    public void UseItem(int selectedCharacter)
    {
        activeItem.useItem(selectedCharacter);
        OpenCharacterChoicePanel();

        Inventory.inventoryInstance.RemoveItemFromList(activeItem);
        UpdateItemsInInventory();

        AudioManager.instance.PlaySFX(8);
    }

    public void OpenCharacterChoicePanel()
    {
        characterChoicePanel.SetActive(true);

        if(activeItem)
        {
            for (int i = 0; i < playerStats.Length; i++)
            {
                PlayerStats activePlayer = GameManager.gameManagerInstance.GetPlayerStats()[i];
                itemsCharacterChoiceName[i].text = activePlayer.playerName;

                bool activePlayerAvailable = activePlayer.gameObject.activeInHierarchy;
                itemsCharacterChoiceName[i].transform.parent.gameObject.SetActive(activePlayerAvailable);
            }
        }
    }

    public void CloseCharacterChoicePanel()
    {
        characterChoicePanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("We've quit the game");
    }

    public void FadeImage()
    {
        imageToFade.GetComponent<Animator>().SetTrigger("Start Fading");
    }

    public void HandleInputVisibility(bool active)
    {
        inputPanel.SetActive(active);
        //for (int i = 0; i < inputPanel.transform.childCount; i++)
        //{
        //    inputPanel.transform.GetChild(i).gameObject.SetActive(active);
        //}
    }
}
