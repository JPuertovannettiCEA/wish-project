using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image PORT1;
    public Image PORT2;
    public Image PORT3;
    public Image PORT4;

    private void Start()
    {
        PORT1.sprite = playerSprites[0].sprite;
        PORT2.sprite = playerSprites[1].sprite;
        PORT3.sprite = playerSprites[2].sprite;
        PORT4.sprite = playerSprites[3].sprite;
        statusAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            statusAnim.SetTrigger("Show");
        }
        if(GameManager.instance.player.GetComponent<PlayerController>().x > 0 || GameManager.instance.player.GetComponent<PlayerController>().x < 0 || GameManager.instance.player.GetComponent<PlayerController>().y > 0 || GameManager.instance.player.GetComponent<PlayerController>().y < 0)
        {
            statusAnim.SetTrigger("Hide");
        }
        
    }
}
