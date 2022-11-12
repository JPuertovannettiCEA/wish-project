using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    // TEXT FIELDS
    public TMP_Text healthText, hitpointText, description, useitemText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image itemSprite;
    public RectTransform xpBar;

    // Character Selection
    public void OnArrowClick()
    {
        
    }
}
