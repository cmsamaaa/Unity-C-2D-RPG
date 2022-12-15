using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleChar : MonoBehaviour
{
    public bool isPlayer;
    public string[] movesAvailable;

    public string charName;
    public int currentHP, maxHP, currentMP, maxMP, strength, defence, wpnPower, armrPower;
    public bool hasdDied;

    public SpriteRenderer theSprite;
    public Sprite deadSprite, aliveSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
