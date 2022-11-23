using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(player2.gameObject);
            Destroy(player3.gameObject);
            Destroy(player4.gameObject);
            Destroy(floatingTextManager.transform.parent.gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();

        instance = this;
        //inventory.slots[] = 3;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    /**
    0 ZEPH
    1 HALI
    2 BRENT
    3 LEE
    4 ADREAM
    **/
    public List<Sprite> itemSprites;
    /**
    0 HEALTH POTION
    1 MAGIC POTION
    2 POWER POTION
    **/
    //public List<int> weaponPrices;
    public List<int> xpTable; 
    /**
    50 = 1st SKILL
    80 = 2nd SKILL
    100 = 3rd SKILL
    150 = 4th SKILL
    300 = 5th SKILL
    **/

    // References
    public GameObject player;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    //public weapon weapon etc
    public FloatingTextManager floatingTextManager;
    public DialogueTextManager dialogue;

    // Logic
    public int money;
    public int experience;
    public int experience1;
    public int experience2;
    public int experience3;
    public int experience4;

    public bool isMagicEffect;
    public bool isPowerEffect;

    //INVENTORY
    public int RedPotions; 
    public int GreenPotions; 
    public int BluePotions; 
    //public bool[] inventoryisFull;
    //public GameObject[] inventorySlots;

    //FloatingText
    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.ShowText(msg,fontSize,color,pos,motion,duration);

    }
    /**public void ShowDialogue(string msg, string character, Image Image, Color color, float duration)
    {
        dialogue.ShowDialogue(msg,character,Image,color,duration);
    }**/

    //EXPERIENCE SYSTEM
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while(experience >= add)
        {
            add =+ xpTable[r];
            r++;

            if(r == xpTable.Count) // MAX LEVEL
            {
                return r;
            }
        }

        return r;
    }

    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;

    }

    public void GrantXP(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if(currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        Debug.Log("LEVEL UP");
        player.GetComponent<Unit>().OnLevelUp();
    }
    // Save state
    /*
    * INT preferedSkin
    * INT money
    * INT experience
    * INT weaponLevel
    */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += money.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState", s);
        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if(!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|'); 
        //EXAMPLE = 0|10|15|2 which means that the pipe DIVIDES EACH VALUE 

        //EXPERIENCE
        money = int.Parse(data[1]); 
        experience = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
        {
            player.GetComponent<Unit>().SetLevel(GetCurrentLevel()); 
        }
        //weaponlevel = int.Parse(data[3]); 

        //Debug.Log("LoadState");

        //if(SceneManager.GetActiveScene() == )
        //player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

}
