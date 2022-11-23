using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    // TEXT FIELDS
    public TMP_Text healthText, levelText, description, useitemText, xpText, itemText, itemQuantText1, itemQuantText2, itemQuantText3;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    //public Image itemSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick()
    {

    }

    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        //GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Item usage
    public void OnRedPotionUseClick()
    {
        if(GameManager.instance.RedPotions > 0)
        {
            // Health Increase
            GameManager.instance.RedPotions--;
            UpdateMenu();
            itemText.text = "The team feels healthy!";
        }
        else
        {
            itemText.text = " You don't have more potions to use! ";
        }
    }
    public void OnRedPotionClick()
    {
        itemText.text = "This is the red potion, it can replenish the health to all the team by 10!";
    }
    public void OnGreenPotionUseClick()
    {
        if(GameManager.instance.GreenPotions > 0)
        {
            // Magic Increase
            GameManager.instance.isMagicEffect = true;
            GameManager.instance.GreenPotions--;
            UpdateMenu();
            itemText.text = "Damage received decreased for the next battle!";
        }
        else
        {
            itemText.text = " You don't have more potions to use! ";
        }
    }
    public void OnGreenPotionClick()
    {
        itemText.text = "This is the green potion, it doubles the defense (half damage received) of the team! Only for the next battle";
    }
    public void OnBluePotionUseClick()
    {
        if(GameManager.instance.BluePotions > 0)
        {
            // Power Increase
            GameManager.instance.isPowerEffect = true;
            GameManager.instance.BluePotions--;
            UpdateMenu();
            itemText.text = "The team feels more powerful for the next battle!";
        }
        else
        {
            itemText.text = " You don't have more potions to use! ";
        }
    }
    public void OnBluePotionClick()
    {
        itemText.text = "This is the blue potion, it doubles the power of the team! Only for the next battle";
    }

    //Update character information 

    public void UpdateMenu()
    {
        itemText.text = " ";
        itemQuantText1.text = "x" + GameManager.instance.RedPotions;
        itemQuantText2.text = "x" + GameManager.instance.GreenPotions;
        itemQuantText3.text = "x" + GameManager.instance.BluePotions;
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        //hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        
        // xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if(currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total experience points"; // Display total XP
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXP = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXP = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXP - prevLevelXP;
            int currXPIntoLevel = GameManager.instance.experience = prevLevelXP;

            float completionRatio = (float)currXPIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currXPIntoLevel.ToString() + " / " + diff;
        }
        //xpText.text = "NOT IMPLEMENTED";
        //xpBar.localScale = new Vector3(0.5f,0,0);

    }

}
