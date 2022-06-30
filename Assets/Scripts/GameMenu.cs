using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    public GameObject theMenu;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (theMenu.activeInHierarchy)
            {
                theMenu.SetActive(false);
                GameManager.instance.gameMenuOpen = false;
            }
            else
            {
                theMenu.SetActive(true);
                GameManager.instance.gameMenuOpen = true;
            }
        }
    }
}
