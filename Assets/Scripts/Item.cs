using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isWeapon;
    public bool isArmor;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;

    [Header("Item Details")]
    public int amountToChange;
    public bool affectHP, affectMP, affectStr;

    [Header("Weapon/Armor Details")]
    public int weaponStrength;
    public int armorStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Use(int charToUseOn)
    {
        CharStats selectedChar = GameManager.instance.playerStats[charToUseOn];

        if (isItem)
        {
            if (affectHP)
            {
                selectedChar.currentHP += amountToChange;
                selectedChar.currentHP = selectedChar.currentHP > selectedChar.maxHP ? selectedChar.maxHP : selectedChar.currentHP;

                //if (selectedChar.currentHP > selectedChar.maxHP)
                //{
                //    selectedChar.currentHP = selectedChar.maxHP;
                //}
            }

            if (affectMP)
            {
                selectedChar.currentMP += amountToChange;
                selectedChar.currentMP = selectedChar.currentMP > selectedChar.maxMP ? selectedChar.maxMP : selectedChar.currentMP;
            }

            if (affectStr)
            {
                selectedChar.strength += amountToChange;
            }
        }

        if (isWeapon)
        {
            if (selectedChar.equippedWeapon != "")
            {
                GameManager.instance.AddItem(selectedChar.equippedWeapon);
            }

            selectedChar.equippedWeapon = itemName;
            selectedChar.weaponPower = weaponStrength;
        }

        if(isArmor)
        {
            if (selectedChar.equippedArmor != "") 
            { 
                GameManager.instance.AddItem(selectedChar.equippedArmor);
            }

            selectedChar.equippedArmor = itemName;
            selectedChar.armorPower = armorStrength;
        }

        GameManager.instance.RemoveItem(itemName);
    }
    public void UseInBattle(int charToUseOn)
    {
        string charName = "";

        for (int i = 0; i < GameManager.instance.playerStats.Length; i++)
        {
            if (i == charToUseOn)
            {
                charName = GameManager.instance.playerStats[i].charName;
            }
        }

        Debug.Log(charName);
        for (int i = 0; i < BattleManager.instance.activeBattlers.Count; i++)
        {
            if (charName == BattleManager.instance.activeBattlers[i].charName)
            {
                BattleChar selectedChar = BattleManager.instance.activeBattlers[i];
                Debug.Log("Found Character: " + selectedChar.charName + " has " + selectedChar.currentHP + "hp left");
                if (isItem)
                {
                    if (affectHP)
                    {
                        if (selectedChar.currentHP != selectedChar.maxHP)
                        {
                            selectedChar.currentHP += amountToChange;

                            if (selectedChar.currentHP > selectedChar.maxHP)
                            {
                                selectedChar.currentHP = selectedChar.maxHP;
                            }
                        }
                        else
                        {
                            Debug.LogError("You are already on full health");
                            //didntUse = true;
                        }
                    }
                    if (affectMP)
                    {
                        selectedChar.currentMP += amountToChange;

                        if (selectedChar.currentMP > selectedChar.maxMP)
                        {
                            selectedChar.currentMP = selectedChar.maxMP;
                        }
                    }
                }
            }
            //BattleManager.instance.UpdateUIStats ();
        }
        GameManager.instance.RemoveItem(itemName);
    }

}
