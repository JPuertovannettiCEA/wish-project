using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if(GameManager.instance  != null)
        {
            Destroy(gameObject);
            return;
        }

        //PlayerPrefs.DeleteAll();

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable; 

    // References
    public PlayerController player;
    //public weapon weapon etc

    // Logic
    public int money;
    public int experience; 

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
        //Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if(!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }

        string[] data = PlayerPrefs.GetString("SaveState").Split('|'); 
        //EXAMPLE = 0|10|15|2 which means that the pipe DIVIDES EACH VALUE 

        //Change player skin
        money = int.Parse(data[1]); 
        experience = int.Parse(data[2]); 
        //weaponlevel = int.Parse(data[3]); 

        Debug.Log("LoadState");
    }

}
