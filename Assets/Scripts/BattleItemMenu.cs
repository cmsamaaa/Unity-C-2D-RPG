using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleItemMenu : MonoBehaviour
{
    public GameObject theMenu;

    public ItemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;

    public GameObject itemCharChoiceMenu;
    public Text[] itemCharChoiceNames;

    public static BattleItemMenu instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ShowItems()
	{
		GameManager.instance.SortItems();
		for (int i = 0; i < itemButtons.Length; i++)
		{
			itemButtons[i].buttonValue = i;

			if (GameManager.instance.itemsHeld[i] != "")
			{
				itemButtons[i].buttonImage.gameObject.SetActive(true);
				itemButtons[i].buttonImage.sprite = GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[i]).itemSprite;
				itemButtons[i].amountText.text = GameManager.instance.numberOfItems[i].ToString();
			}
			else
			{
				itemButtons[i].buttonImage.gameObject.SetActive(false);
				itemButtons[i].amountText.text = "";
			}
		}
	}

	public void SelectItem(Item newItem)
	{
		activeItem = newItem;
		if (activeItem.isItem)
		{
			useButtonText.text = "Use";
		}

		if (activeItem.isWeapon || activeItem.isArmor)
		{
			useButtonText.text = "Equip";
		}

		itemName.text = activeItem.itemName;
		itemDescription.text = activeItem.description;
	}

	public void DiscardItem()
	{
		if (activeItem != null)
		{
			GameManager.instance.RemoveItem(activeItem.itemName);
		}
	}

	public void OpenItemCharChoice()
	{
		itemCharChoiceMenu.SetActive(true);

		for (int i = 0; i < itemCharChoiceNames.Length; i++)
		{
			itemCharChoiceNames[i].text = GameManager.instance.playerStats[i].charName;
			itemCharChoiceNames[i].transform.parent.gameObject.SetActive(GameManager.instance.playerStats[i].gameObject.activeInHierarchy);
		}
	}

	public void CloseItemCharChoice()
	{
		itemCharChoiceMenu.SetActive(false);
	}

	public void UseItem(int selectChar)
	{

		activeItem.UseInBattle(selectChar);
		CloseItemCharChoice();
		BattleManager.instance.itemMenu.SetActive(false);
		BattleManager.instance.NextTurn();

	}
}
