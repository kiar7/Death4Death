using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int maxLevel = 50;
    [SerializeField] int baseLevelXP = 100;

    public static PlayerStats playerStatsInstance;

    public int dexterity;
    public int defence;

    public Sprite charaterImage;

    public string playerName;
    public int playerLevel = 1;

    public int currentHP;
    public int maxHP = 100;

    public int currentXP;
    public int[] xpForNextLevel;

    public int maxMana = 30;
    public int currentMana;

    public string equippedWeaponName;
    public string equippedArmorName;

    public int weaponPower;
    public int armorDef;

    public ItemsManager equipWeapon, equipArmor;



    // Start is called before the first frame update
    void Start()
    {
        playerStatsInstance = this;

        xpForNextLevel = new int[maxLevel];
        xpForNextLevel[1] = baseLevelXP;

        for(int i = 2; i < xpForNextLevel.Length; i++)
        {
            xpForNextLevel[i] = (int)(0.02f * i * i * i + 3.06f * i * i + 105.6f * i);//preso dal wiki di dark souls
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddXP(int amountXP)
    {
        currentXP += amountXP;

        if(currentXP >= xpForNextLevel[playerLevel])
        {
            currentXP -= xpForNextLevel[playerLevel];
            playerLevel++;

            if(playerLevel % 2 == 0)
            {
                dexterity++;
            }
            else
            {
                defence++;
            }

            maxHP = Mathf.FloorToInt(maxHP * 1.06f);
            //currentHP = maxHP; decommentare per far si che il player abbia il massimo della vita quando sale di livello

            currentMana = Mathf.FloorToInt(maxMana * 1.06f);
            //currentMana = maxMana; decommentare per far si che il player abbia il massimo della vita quando sale di livello
        }
    }

    public void AddHP(int amountHPToAdd)
    {
        currentHP += amountHPToAdd;
        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

    public void AddMana(int amountManaToAdd)
    {
        currentMana += amountManaToAdd;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    public void EquipWeapon(ItemsManager weaponToEquip)
    {
        equipWeapon = weaponToEquip;
        equippedWeaponName = equipWeapon.itemName;
        weaponPower = equipWeapon.weaponDexterity;
    }

    public void EquipArmor(ItemsManager armorToEquip)
    {
        equipArmor = armorToEquip;
        equippedArmorName = equipArmor.itemName;
        weaponPower = equipArmor.armorDefence;
    }
}
