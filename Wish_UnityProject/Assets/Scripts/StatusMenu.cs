using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusMenu : MonoBehaviour
{
    public List<Image> playerSprites;
    /**
    0 - ZEPH
    1 - HALI
    2 - BRENT 
    3 - LEE
    4 - ADREAM
    **/

    public Animator statusAnim;

    //CHARACTER IMAGES
    public Image PORT1;
    public Image PORT2;
    public Image PORT3;
    public Image PORT4;

    //CHARACTER STATS
    public TMP_Text levelPlayer1, healthPlayer1, nameplayer1;
    public TMP_Text levelPlayer2, healthPlayer2, nameplayer2;
    public TMP_Text levelPlayer3, healthPlayer3, nameplayer3;
    public TMP_Text levelPlayer4, healthPlayer4, nameplayer4;
    public Slider xpBar1;
    public Slider xpBar2;
    public Slider xpBar3;
    public Slider xpBar4;

    private void Start()
    {
        /**PORT1.sprite = playerSprites[0].sprite;
        PORT2.sprite = playerSprites[1].sprite;
        PORT3.sprite = playerSprites[2].sprite;
        PORT4.sprite = playerSprites[3].sprite;
        **/

        //PLAYER 1
        if(GameManager.instance.player.GetComponent<Unit>().unitName == "Zeph")
        {
            PORT1.sprite = playerSprites[0].sprite;
        }
        xpBar1.maxValue = GameManager.instance.player.GetComponent<Unit>().maxHP;
        xpBar1.value = GameManager.instance.player.GetComponent<Unit>().currentHP;

        //PLAYER 2
        if(GameManager.instance.player2.GetComponent<Unit>().unitName == "Hali")
        {
            PORT2.sprite = playerSprites[1].sprite;
        }
        if(GameManager.instance.player2.GetComponent<Unit>().unitName == "Brent")
        {
            PORT2.sprite = playerSprites[2].sprite;
        }
        if(GameManager.instance.player2.GetComponent<Unit>().unitName == "Lee")
        {
            PORT2.sprite = playerSprites[3].sprite;
        }
        if(GameManager.instance.player2.GetComponent<Unit>().unitName == "Adream")
        {
            PORT2.sprite = playerSprites[4].sprite;
        }
        xpBar2.maxValue = GameManager.instance.player2.GetComponent<Unit>().maxHP;
        xpBar2.value = GameManager.instance.player2.GetComponent<Unit>().currentHP;

        //PLAYER 3
        if(GameManager.instance.player3.GetComponent<Unit>().unitName == "Hali")
        {
            PORT3.sprite = playerSprites[1].sprite;
        }
        if(GameManager.instance.player3.GetComponent<Unit>().unitName == "Brent")
        {
            PORT3.sprite = playerSprites[2].sprite;
        }
        if(GameManager.instance.player3.GetComponent<Unit>().unitName == "Lee")
        {
            PORT3.sprite = playerSprites[3].sprite;
        }
        if(GameManager.instance.player3.GetComponent<Unit>().unitName == "Adream")
        {
            PORT3.sprite = playerSprites[4].sprite;
        }
        xpBar3.maxValue = GameManager.instance.player3.GetComponent<Unit>().maxHP;
        xpBar3.value = GameManager.instance.player3.GetComponent<Unit>().currentHP;

        //PLAYER 4
        if(GameManager.instance.player4.GetComponent<Unit>().unitName == "Hali")
        {
            PORT4.sprite = playerSprites[1].sprite;
        }
        if(GameManager.instance.player4.GetComponent<Unit>().unitName == "Brent")
        {
            PORT4.sprite = playerSprites[2].sprite;
        }
        if(GameManager.instance.player4.GetComponent<Unit>().unitName == "Lee")
        {
            PORT4.sprite = playerSprites[3].sprite;
        }
        if(GameManager.instance.player4.GetComponent<Unit>().unitName == "Adream")
        {
            PORT4.sprite = playerSprites[4].sprite;
        }
        xpBar4.maxValue = GameManager.instance.player4.GetComponent<Unit>().maxHP;
        xpBar4.value = GameManager.instance.player4.GetComponent<Unit>().currentHP;

        statusAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            statusAnim.SetTrigger("Show");
            UpdateStatus();
        }
        if(GameManager.instance.player.GetComponent<PlayerController>().x > 0 || GameManager.instance.player.GetComponent<PlayerController>().x < 0 || GameManager.instance.player.GetComponent<PlayerController>().y > 0 || GameManager.instance.player.GetComponent<PlayerController>().y < 0)
        {
            statusAnim.SetTrigger("Hide");
        }
        
    }

    void UpdateStatus()
    {
        nameplayer1.text = GameManager.instance.player.GetComponent<Unit>().unitName;
        levelPlayer1.text = "LEVEL: " + GameManager.instance.player.GetComponent<Unit>().unitLevel;
        healthPlayer1.text = "HEALTH: " + GameManager.instance.player.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player.GetComponent<Unit>().maxHP;
        xpBar1.value = GameManager.instance.player.GetComponent<Unit>().currentHP;

        nameplayer2.text = GameManager.instance.player2.GetComponent<Unit>().unitName;
        levelPlayer2.text = "LEVEL: " + GameManager.instance.player2.GetComponent<Unit>().unitLevel;
        healthPlayer2.text = "HEALTH: " + GameManager.instance.player2.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player2.GetComponent<Unit>().maxHP;
        xpBar2.value = GameManager.instance.player2.GetComponent<Unit>().currentHP;

        nameplayer3.text = GameManager.instance.player3.GetComponent<Unit>().unitName;
        levelPlayer3.text = "LEVEL: " + GameManager.instance.player3.GetComponent<Unit>().unitLevel;
        healthPlayer3.text = "HEALTH: " + GameManager.instance.player3.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player3.GetComponent<Unit>().maxHP;
        xpBar3.value = GameManager.instance.player3.GetComponent<Unit>().currentHP;

        nameplayer4.text = GameManager.instance.player4.GetComponent<Unit>().unitName;
        levelPlayer4.text = "LEVEL: " + GameManager.instance.player4.GetComponent<Unit>().unitLevel;
        healthPlayer4.text = "HEALTH: " + GameManager.instance.player4.GetComponent<Unit>().currentHP + " / " + GameManager.instance.player4.GetComponent<Unit>().maxHP;
        xpBar4.value = GameManager.instance.player4.GetComponent<Unit>().currentHP;

    }
}
