using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public enum ItemType {item, weapon, armor, coin}
    public enum AffectType {hp, mana}

    private const double V = 0.5;
    public ItemType itemType;
    public AffectType affectType;

    public string itemName, itemDescription;

    public int valueInCoins;
    public int amountOfAffect;
    public int weaponDexterity;
    public int armorDefence;
    public int amount = 1;

    public Sprite itemSprite;

    public bool isStackable;

    public void useItem(int characterToUseOn)
    {
        PlayerStats selectedCharacter = GameManager.gameManagerInstance.GetPlayerStats()[characterToUseOn];

        if(itemType == ItemType.item)
        {
            if(affectType == AffectType.hp)
            {
                selectedCharacter.AddHP(amountOfAffect);
            }
            else if(affectType == AffectType.mana)
            {
                selectedCharacter.AddMana(amountOfAffect);
            }
        }
        else if(itemType == ItemType.weapon)
        {
            if(selectedCharacter.equippedWeaponName != "")
            {
                Inventory.inventoryInstance.AddItems(selectedCharacter.equipWeapon);
            }
            selectedCharacter.EquipWeapon(this);
        }
        else if (itemType == ItemType.armor)
        {
            if (selectedCharacter.equippedArmorName != "")
            {
                Inventory.inventoryInstance.AddItems(selectedCharacter.equipArmor);
            }
            selectedCharacter.EquipWeapon(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(5);
            if (itemType == ItemType.coin)
            {
                GameManager.gameManagerInstance.currentCoins += 1;
                Debug.Log("CC" + GameManager.gameManagerInstance.currentCoins);
            }
            else
            {
                Inventory.inventoryInstance.AddItems(this);
            }
            SelfDestroy();
        }
    }

    public void SelfDestroy()
    {
        gameObject.SetActive(false);
    }

}
