using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    // TEXT FIELDS
    public TMP_Text healthText, levelText, description, useitemText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image itemSprite;
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
    public void OnItemClick()
    {
        // REFERENCE THE ITEM 
    }

    //Update character information 

    public void UpdateMenu()
    {
        

        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        //hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        
        // xp Bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if(currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + " total expereince points"; // Display total XP
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
