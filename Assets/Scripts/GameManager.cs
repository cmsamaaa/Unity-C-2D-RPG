using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public CharStats[] playerStats;

    public bool gameMenuOpen, dialogActive, fadingBetweenAreas;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameMenuOpen || dialogActive || fadingBetweenAreas)
        {
            PlayerController.instance.canMove = false;
        }
        else
        {
            PlayerController.instance.canMove = true;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            AddItem("Iron Armor");
            AddItem("blabla");

            RemoveItem("Health Potion");
            RemoveItem("Bleep");
        }
    }

    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if(referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }

        return null;
    }

    public void SortItems()
    {
        // 0: ""
        // 1: ""
        // 2: "a"
        // 3: ""
        // 4: ""

        bool itemAfterSpace = true;

        while(itemAfterSpace)
        {
            itemAfterSpace = false;

            for (int i = 0; i < itemsHeld.Length - 1; i++)
            {
                if (itemsHeld[i] == "")
                {
                    itemsHeld[i] = itemsHeld[i + 1];
                    itemsHeld[i + 1] = "";

                    numberOfItems[i] = numberOfItems[i + 1];
                    numberOfItems[i + 1] = 0;

                    if (itemsHeld[i] != "")
                    {
                        itemAfterSpace = true;
                    }
                }
            }
        }        
    }

    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {
                newItemPosition = i;
                foundSpace = true;
                i = itemsHeld.Length; // set i to the last index so for loop will stop
            }
        }

        if (foundSpace)
        {
            bool itemExists = false;

            for (int i = 0; i < referenceItems.Length; i++)
            {
                if (referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;
                    i = referenceItems.Length; // set i to the last index so for loop will stop
                }
            }

            if (itemExists)
            {
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.LogError(itemToAdd + " does not exist!!");
            }
        }

        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
           if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;
                i = itemsHeld.Length; // set i to the last index so for loop will stop
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.LogError("Couldn't find " + itemToRemove);
        }
    }
}
