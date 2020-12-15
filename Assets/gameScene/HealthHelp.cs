using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHelp : MonoBehaviour
{
    public int hp;
    public int numofHppoints;

    public Sprite redSprite;
    public Sprite greenSprite;

    public Image[] Points;

    //Health bar reduced one point when player detects collision with bullet
    public void reduceHealth()
    {
        hp--;
    }

    //returns current health to decide if player is dead
    public int getHP()
    {
        return hp;
    }

    //Updates the color of the battery slots depending on how many life points player has
    private void Update()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            if (i < hp)
            {
                Points[i].sprite = greenSprite;
            }
            else
            {
                Points[i].sprite = redSprite;
            }


            if (i < numofHppoints)
            {
                Points[i].enabled = true;
            }
            else
            {
                Points[i].enabled = false;
            }
        }
    }
}
