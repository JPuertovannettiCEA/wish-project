using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    // TEXT FIELDS
    public TMP_Text healthText, levelText, description, useitemText, xpText, itemText, itemQuantText1, itemQuantText2, itemQuantText3;
    public Image characterImage;

    public List<Image> playerArtwork;
    /**
    0 - ZEPH
    1 - HALI
    2 - BRENT 
    3 - LEE
    4 - ADREAM
    **/

    public List<Sprite> notActive;
    /**
    0 NOT ACTIVE ADREAM
    1 ACTIVE ADREAM
    2 NOT ACTIVE LEE
    3 ACTIVE LEE
    **/

    public Image AdreamMenu;
    public Image LeeMenu;

    public bool isAdreamActive;

    public GameObject activePanel;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    //public Image itemSprite;
    public RectTransform xpBar;

    public Animator anim;

    private bool isPaused;

    private void Start()
    {
        //isPaused = false;
        activePanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(!isPaused)
            {
                //isPaused = true;
                GameManager.instance.isPaused = true;
                anim.GetComponent<Animator>().SetTrigger("Show");
                UpdateMenu();
                GameManager.instance.player.GetComponent<PlayerController>().enabled = false;
                //Time.timeScale = 0;
            }
            else
            {
                GameManager.instance.isPaused = false;
                anim.GetComponent<Animator>().SetTrigger("Hide");
                GameManager.instance.player.GetComponent<PlayerController>().enabled = true;
                //Time.timeScale = 1;
            }
            isPaused = !isPaused;
        }

    }

    // Character Selection
    public void OnActiveClick()
    {
        OnSelectionChanged();
    }

    public void OnZephClick()
    {
        activePanel.SetActive(false);
        characterImage.sprite = playerArtwork[0].sprite;
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(358.5f, 640.6f);
        rt.anchoredPosition = new Vector2(0f,rt.anchoredPosition.y);
        healthText.text = GameManager.instance.player.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player.GetComponent<Unit>().maxHP;
        levelText.text = GameManager.instance.player.GetComponent<Unit>().unitLevel.ToString();
        description.text = "The lead one!";
    }

    public void OnHaliClick()
    {
        activePanel.SetActive(false);
        characterImage.sprite = playerArtwork[1].sprite;
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(358.5f, 640.6f);
        rt.anchoredPosition = new Vector2(0f,rt.anchoredPosition.y);
        healthText.text = GameManager.instance.player2.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player2.GetComponent<Unit>().maxHP;
        levelText.text = GameManager.instance.player2.GetComponent<Unit>().unitLevel.ToString();
        description.text = "The happy one!";
    }
    public void OnBrentClick()
    {
        activePanel.SetActive(false);
        characterImage.sprite = playerArtwork[2].sprite;
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(358.5f, 740.6f);
        rt.anchoredPosition = new Vector2(0f,rt.anchoredPosition.y);
        healthText.text = GameManager.instance.player3.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player3.GetComponent<Unit>().maxHP;
        levelText.text = GameManager.instance.player3.GetComponent<Unit>().unitLevel.ToString();
        description.text = "The rough one!";
    }
    public void OnLeeClick()
    {
        if(isAdreamActive == true)
        {
            activePanel.SetActive(true);
        }
        else
        {
            activePanel.SetActive(false);
        }
        characterImage.sprite = playerArtwork[3].sprite;
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(358.5f, 640.6f);
        rt.anchoredPosition = new Vector2(0f,rt.anchoredPosition.y);
        healthText.text = GameManager.instance.player4.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player4.GetComponent<Unit>().maxHP;
        levelText.text = GameManager.instance.player4.GetComponent<Unit>().unitLevel.ToString();
        description.text = "The mischievous one!";
    }
    public void OnAdreamClick()
    {
        if(isAdreamActive == false)
        {
            activePanel.SetActive(true);
        }
        else
        {
            activePanel.SetActive(false);
        }
        characterImage.sprite = playerArtwork[4].sprite;
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(200.7f, 640.6f);
        rt.anchoredPosition = new Vector2(80f,rt.anchoredPosition.y);
        healthText.text = GameManager.instance.player5.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player5.GetComponent<Unit>().maxHP;
        levelText.text = GameManager.instance.player5.GetComponent<Unit>().unitLevel.ToString();
        description.text = "The timid one!";
    }

    private void OnSelectionChanged()
    {
        //characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        if(isAdreamActive == false)
        {
            AdreamMenu.sprite = notActive[1];
            LeeMenu.sprite = notActive[2];
            isAdreamActive = true;
            GameManager.instance.isAdreamActive = true;
            GameManager.instance.hasSwitched = true;
            activePanel.SetActive(false);
        }
        else
        {
            AdreamMenu.sprite = notActive[0];
            LeeMenu.sprite = notActive[3];
            isAdreamActive = false;
            GameManager.instance.isAdreamActive = false;
            GameManager.instance.hasSwitched = true;
            activePanel.SetActive(false);
        }
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
        //GameManager.instance.player.GetComponent<PlayerController>().enabled = false;
        //CHANGE CHARACTER
        if(GameManager.instance.isAdreamActive == false)
        {
            isAdreamActive = false;
            AdreamMenu.sprite = notActive[0];
            LeeMenu.sprite = notActive[3];
        }
        else //ADREAM IS TRUE
        {
            isAdreamActive = true;
            AdreamMenu.sprite = notActive[1];
            LeeMenu.sprite = notActive[2];
        }
        //
        itemText.text = "Select an item to see their effect";
        itemQuantText1.text = "x" + GameManager.instance.RedPotions;
        itemQuantText2.text = "x" + GameManager.instance.GreenPotions;
        itemQuantText3.text = "x" + GameManager.instance.BluePotions;
        characterImage.sprite = playerArtwork[0].sprite;
        RectTransform rt = characterImage.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(358.5f, 640.6f);
        rt.anchoredPosition = new Vector2(0f,rt.anchoredPosition.y);
        healthText.text = " "; //GameManager.instance.player.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player.GetComponent<Unit>().maxHP;
        levelText.text = " "; //GameManager.instance.player.GetComponent<Unit>().unitLevel.ToString();
        description.text = "Select a character to see their description";

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
