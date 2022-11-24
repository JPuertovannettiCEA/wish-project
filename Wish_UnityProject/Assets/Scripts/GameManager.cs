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
            //if(isAdreamActive != false)
            //{
            Destroy(player4.gameObject);
            //}
            //else
            //{
            Destroy(player5.gameObject);
            //}
            Destroy(floatingTextManager.transform.parent.gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        hasSwitched = true;
        Enemy = GameObject.Find("Enemy_Forest");
        Enemy = GameObject.Find("Enemy_LightForest");
        Enemy = GameObject.Find("Enemy_Dungeon");
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(isAdreamActive == true)
        {
            //player5.SetActive(true);
            if(hasSwitched == true)
            {
                player5.transform.position = player4.transform.position;
                player5.GetComponent<FollowPlayer2>().enabled = true;
                player4.transform.position = new Vector3(100f,100f,0f);
                player4.GetComponent<FollowPlayer2>().enabled = false;
                hasSwitched = false;
            }
            //player4.SetActive(false);
        }
        else
        {
            //player5.SetActive(false);
            if(hasSwitched == true)
            {
                player4.transform.position = player5.transform.position;
                player4.GetComponent<FollowPlayer2>().enabled = true;
                player5.transform.position = new Vector3(100f,100f,0f);
                player5.GetComponent<FollowPlayer2>().enabled = false;
                hasSwitched = false;
            }
            //player4.SetActive(true);
        }

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
    public GameObject player5;
    //public weapon weapon etc
    public FloatingTextManager floatingTextManager;
    public DialogueTextManager dialogue;
    public GameObject Enemy;

    public bool isBossBattle;

    public bool isPaused;
    public bool isAdreamActive;

    public bool hasSwitched;

    //TEXT AVATARS
    public List<GameObject> NPCAvatarsForDialogue;

    public List<TMPro.TMP_FontAsset> TextFonts;

    // Logic
    public int money;
    public int experience;
    public int experience1;
    public int experience2;
    public int experience3;
    public int experience4;

    public bool isMagicEffect;
    public bool isPowerEffect;

    public bool isMonsterDefeated;

    public int bossBattleTurns;

    public bool bossBattleEnds;

    public bool dialogueBegins;

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
        player2.GetComponent<Unit>().OnLevelUp();
        player3.GetComponent<Unit>().OnLevelUp();
        if(isAdreamActive == true)
        {
            player5.GetComponent<Unit>().OnLevelUp();
        }
        else
        {
            player4.GetComponent<Unit>().OnLevelUp();
        }
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
        s += isMagicEffect.ToString() + "|";
        s += isPowerEffect.ToString() + "|";
        s += isAdreamActive.ToString() + "|";
        s += isMonsterDefeated.ToString() + "|";
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
        isMagicEffect = bool.Parse(data[3]);
        isPowerEffect = bool.Parse(data[4]);
        isAdreamActive = bool.Parse(data[5]);
        isMonsterDefeated = bool.Parse(data[6]);
        //enemyPos = new Vector3(float.Parse(data[6]),);
        if(GetCurrentLevel() != 1)
        {
            player.GetComponent<Unit>().SetLevel(GetCurrentLevel()); 
        }
        //weaponlevel = int.Parse(data[3]); 

        //Debug.Log("LoadState");

        string prevScene = LevelCheck.PreviousLevel;
        switch(prevScene)
        {
            //FROM START
            case "IntroductionScene":
                player.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            break;
            //FROM ROUTE 1
            case "GameplayScene_Route1":
            if(GameObject.Find("SpawnPoint_Route1"))
            {
                player.transform.position = GameObject.Find("SpawnPoint_Route1").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Route1").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Route1").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Route1").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Route1").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            break;
            //FROM ROUTE 2
            case "GameplayScene_Route2":
            if(GameObject.Find("SpawnPoint_Route2"))
            {
                player.transform.position = GameObject.Find("SpawnPoint_Route2").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Route2").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Route2").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Route2").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Route2").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            if(GameObject.Find("SpawnPoint_Route2_Forest"))
            {
                player.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            if(GameObject.Find("SpawnPoint_Route2_Forest_Route3"))
            {
                player.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            break;
            //FROM GAMEPLAY SCENE 1 
            case "GameplayScene":
            if(GameObject.Find("SpawnPoint"))
            {
                player.transform.position = GameObject.Find("SpawnPoint").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            if(GameObject.Find("SpawnPoint_Main"))
            {
                player.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            break;
            //FROM ROUTE 3
            case "GameplayScene_Route3":
            if(GameObject.Find("SpawnPoint_Route3"))
            {
                player.transform.position = GameObject.Find("SpawnPoint_Route3").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Route3").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Route3").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Route3").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Route3").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            break;
            //FROM DUNGEON
            case "GameplayScene_Route2_Dungeon":
            if(GameObject.Find("SpawnPoint_Dungeon"))
            {
                player.transform.position = GameObject.Find("SpawnPoint_Dungeon").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Dungeon").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Dungeon").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Dungeon").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Dungeon").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            }
            break;
            //FROM FOREST BATTLE
            case "BattleSceneForest":
                if(isMonsterDefeated == true)
                {
                    Destroy(GameObject.Find("Enemy_Forest"));
                    isMonsterDefeated = false;
                }
                //GAMEPLAY SCENE MAIN
                if(SceneManager.GetActiveScene().buildIndex == 2)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    player.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                    player2.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                    player3.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                    if(isAdreamActive == true)
                    {
                        player5.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                        player5.GetComponent<FollowPlayer2>().enabled = true;
                        player4.transform.position = new Vector3(100f,100f,0f);
                        player4.GetComponent<FollowPlayer2>().enabled = false;

                    }
                    else
                    {
                        player4.transform.position = GameObject.Find("SpawnPoint_Start").transform.position;
                        player4.GetComponent<FollowPlayer2>().enabled = true;
                        player5.transform.position = new Vector3(100f,100f,0f);
                        player5.GetComponent<FollowPlayer2>().enabled = false;

                    }
                }
                //ROUTE 2
                if(SceneManager.GetActiveScene().buildIndex == 4)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    player.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                    player2.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                    player3.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                    if(isAdreamActive == true)
                    {
                        player5.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                        player5.GetComponent<FollowPlayer2>().enabled = true;
                        player4.transform.position = new Vector3(100f,100f,0f);
                        player4.GetComponent<FollowPlayer2>().enabled = false;

                    }
                    else
                    {
                        player4.transform.position = GameObject.Find("SpawnPoint_Main").transform.position;
                        player4.GetComponent<FollowPlayer2>().enabled = true;
                        player5.transform.position = new Vector3(100f,100f,0f);
                        player5.GetComponent<FollowPlayer2>().enabled = false;

                    }
                }
                //ROUTE 3
                if(SceneManager.GetActiveScene().buildIndex == 6)
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    player.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                    player2.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                    player3.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                    if(isAdreamActive == true)
                    {
                        player5.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                        player5.GetComponent<FollowPlayer2>().enabled = true;
                        player4.transform.position = new Vector3(100f,100f,0f);
                        player4.GetComponent<FollowPlayer2>().enabled = false;

                    }
                    else
                    {
                        player4.transform.position = GameObject.Find("SpawnPoint_Route2_Forest_Route3").transform.position;
                        player4.GetComponent<FollowPlayer2>().enabled = true;
                        player5.transform.position = new Vector3(100f,100f,0f);
                        player5.GetComponent<FollowPlayer2>().enabled = false;

                    }
                }

            break;
            //FROM LIGHT FOREST BATTLE
            case "BattleSceneLightForest":
                if(isMonsterDefeated == true)
                {
                    Destroy(GameObject.Find("Enemy_LightForest"));
                    isMonsterDefeated = false;
                }
                player.GetComponent<PlayerController>().enabled = true;
                player.transform.position = GameObject.Find("SpawnPoint").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            break;
            //FROM DUNGEON BATTLE
            case "BattleSceneDungeon":
                if(isMonsterDefeated == true)
                {
                    Destroy(GameObject.Find("Enemy_Dungeon"));
                    isMonsterDefeated = false;
                }
                player.GetComponent<PlayerController>().enabled = true;
                player.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                player2.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                player3.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                if(isAdreamActive == true)
                {
                    player5.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                    player5.GetComponent<FollowPlayer2>().enabled = true;
                    player4.transform.position = new Vector3(100f,100f,0f);
                    player4.GetComponent<FollowPlayer2>().enabled = false;

                }
                else
                {
                    player4.transform.position = GameObject.Find("SpawnPoint_Route2_Forest").transform.position;
                    player4.GetComponent<FollowPlayer2>().enabled = true;
                    player5.transform.position = new Vector3(100f,100f,0f);
                    player5.GetComponent<FollowPlayer2>().enabled = false;

                }
            break;
        }
        /**
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
        player2.transform.position = GameObject.Find("SpawnPoint").transform.position;
        player3.transform.position = GameObject.Find("SpawnPoint").transform.position;
        if(isAdreamActive == true)
        {
            player5.transform.position = GameObject.Find("SpawnPoint").transform.position;
            player4.transform.position = new Vector3(100f,100f,0f);

        }
        else
        {
            player4.transform.position = GameObject.Find("SpawnPoint").transform.position;
            player5.transform.position = new Vector3(100f,100f,0f);

        }**/
    }

}
