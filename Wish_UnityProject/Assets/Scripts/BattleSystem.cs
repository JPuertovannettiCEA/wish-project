using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleStates
{
    START,
    PLAYER1TURN,
    PLAYER2TURN,
    PLAYER3TURN,
    PLAYER4TURN,
    ENEMYTURN,
    WON,
    LOST
}

public class BattleSystem : MonoBehaviour
{
    //LEVEL LOAD PARAMETERS
    public Animator transition;

    public float transitionTime = 1f;

    /**
    7 = FOREST
    8 = LIGHT FOREST
    9 = DUNGEON
    **/

    public GameObject playerPrefab1;
    public GameObject playerPrefab2;
    public GameObject playerPrefab3;
    public GameObject playerPrefab4;
    public GameObject enemyPrefab;

    public Transform playerBattleStation1;
    public Transform playerBattleStation2;
    public Transform playerBattleStation3;
    public Transform playerBattleStation4;
    public Transform enemyBattleStation;

    Unit playerUnit_1;
    Unit playerUnit_2;
    Unit playerUnit_3;
    Unit playerUnit_4;
    Unit enemyUnit;
    private GameObject enemy_type;
    private int enemyXP;

    public TMP_Text dialogueText;

    public TMP_Text partyText;

    public BattleHUD player1HUD;
    public BattleHUD player2HUD;
    public BattleHUD player3HUD;
    public BattleHUD player4HUD;

    public BattleHUD enemyHUD;
    private bool is1Dead = false;
    private bool is2Dead = false;
    private bool is3Dead = false;
    private bool is4Dead = false;
    private bool allDead = false;

    public BattleStates state;
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 7) // FOREST
        {
            enemyXP = 5;
        }
        if(SceneManager.GetActiveScene().buildIndex == 8) // LIGHTFOREST
        {
            enemyXP = 3;
        }
        if(SceneManager.GetActiveScene().buildIndex == 9) // DUNGEON
        {
            enemyXP = 10;
        }
        if(GameManager.instance.isMagicEffect == true)
        {
            enemyUnit.damage = enemyUnit.damage / 2;
        }
        if(GameManager.instance.isPowerEffect == true)
        {
            playerUnit_1.damage = playerUnit_1.damage * 2;
            playerUnit_2.damage = playerUnit_2.damage * 2;
            playerUnit_3.damage = playerUnit_3.damage * 2;
            playerUnit_4.damage = playerUnit_4.damage * 2;
        }
        state = BattleStates.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO_1 = Instantiate(playerPrefab1, playerBattleStation1);
        playerUnit_1 = playerGO_1.GetComponent<Unit>();
        GameObject playerGO_2 = Instantiate(playerPrefab2, playerBattleStation2);
        playerUnit_2 = playerGO_2.GetComponent<Unit>();
        GameObject playerGO_3 = Instantiate(playerPrefab3, playerBattleStation3);
        playerUnit_3 = playerGO_3.GetComponent<Unit>();
        GameObject playerGO_4 = Instantiate(playerPrefab4, playerBattleStation4);
        playerUnit_4 = playerGO_4.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches!";

        player1HUD.SetHUD(playerUnit_1);
        player2HUD.SetHUD(playerUnit_2);
        player3HUD.SetHUD(playerUnit_3);
        player4HUD.SetHUD(playerUnit_4);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1f);

        state = BattleStates.PLAYER1TURN;
        StartCoroutine(Player1Turn());

    }

    IEnumerator Player1Turn()
    {
        if(is1Dead == true)
        {
            dialogueText.text = playerUnit_1.unitName + "can't attack anymore!";
            partyText.text = "...";
            yield return new WaitForSeconds(2f);
            state = BattleStates.PLAYER2TURN;
            StartCoroutine(Player2Turn());
        }
        else
        {        
            dialogueText.text = "Choose an action!";
            partyText.text = "What should " + playerUnit_1.unitName + " do?";
        }
    }
    IEnumerator Player2Turn()
    {
        if(is2Dead == true)
        {
            dialogueText.text = playerUnit_2.unitName + "can't attack anymore!";
            partyText.text = "...";
            yield return new WaitForSeconds(2f);
            state = BattleStates.PLAYER3TURN;
            StartCoroutine(Player3Turn());
        }
        else
        {
            dialogueText.text = "Choose an action!";
            partyText.text = "What should " + playerUnit_2.unitName + " do?";
        }
    }
    IEnumerator Player3Turn()
    {
        if(is3Dead == true)
        {
            dialogueText.text = playerUnit_3.unitName + "can't attack anymore!";
            partyText.text = "...";
            yield return new WaitForSeconds(2f);
            state = BattleStates.PLAYER4TURN;
            StartCoroutine(Player4Turn());
        }
        else
        {
            dialogueText.text = "Choose an action!";
            partyText.text = "What should " + playerUnit_3.unitName + " do?";
        }
    }
    IEnumerator Player4Turn()
    {
        if(is4Dead == true)
        {
            dialogueText.text = playerUnit_4.unitName + "can't attack anymore!";
            partyText.text = "...";
            yield return new WaitForSeconds(2f);
            state = BattleStates.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            dialogueText.text = "Choose an action!";
            partyText.text = "What should " + playerUnit_4.unitName + " do?";
        }
    }

    public void OnAttackButton()
    {
        //if(state != BattleStates.PLAYER1TURN)
        if(state == BattleStates.PLAYER1TURN && is1Dead == false)
        {
            StartCoroutine(Player1Attack());
        }
        if(state == BattleStates.PLAYER2TURN && is2Dead == false)
        {
            StartCoroutine(Player2Attack());
        }
        if(state == BattleStates.PLAYER3TURN && is3Dead == false)
        {
            StartCoroutine(Player3Attack());
        }
        if(state == BattleStates.PLAYER4TURN && is4Dead == false)
        {
            StartCoroutine(Player4Attack());
        }
        else
        {
            return;
        }

    }
    public void OnHealButton()
    {
        //if(state != BattleStates.PLAYER1TURN)
        if(state == BattleStates.PLAYER1TURN && is1Dead == false)
        {
            StartCoroutine(Player1Heal());
        }
        if(state == BattleStates.PLAYER2TURN && is2Dead == false)
        {
            StartCoroutine(Player2Heal());
        }
        if(state == BattleStates.PLAYER3TURN && is3Dead == false)
        {
            StartCoroutine(Player3Heal());
        }
        if(state == BattleStates.PLAYER4TURN && is4Dead == false)
        {
            StartCoroutine(Player4Heal());
        }
        else
        {
            return;
        }

    }

    public void OnEscapeButton()
    {
        if(state == BattleStates.ENEMYTURN)
        {
            return;
        }

        //FOREST
        if(SceneManager.GetActiveScene().buildIndex == 7)
        {
            StartCoroutine(LoadLevel(2));
        }
        //LIGHT FOREST
        if(SceneManager.GetActiveScene().buildIndex == 8)
        {
            StartCoroutine(LoadLevel(3));
        }
        //DUNGEON
        if(SceneManager.GetActiveScene().buildIndex == 9)
        {
            StartCoroutine(LoadLevel(5));
        }
        //SceneManager.LoadScene("GameplayScene");
    }

    IEnumerator Player1Heal()
    {
        playerUnit_1.Heal(5);

        player1HUD.SetHP(playerUnit_1.currentHP);
        dialogueText.text = playerUnit_1.unitName + " heals themselves! Now " + playerUnit_1.unitName + " is ready to keep fighting!";

        yield return new WaitForSeconds(2f);

        state = BattleStates.PLAYER2TURN;
        StartCoroutine(Player2Turn());
        //state = BattleStates.ENEMYTURN;
        //StartCoroutine(EnemyTurn());

    }
    IEnumerator Player2Heal()
    {
        playerUnit_2.Heal(5);

        player2HUD.SetHP(playerUnit_2.currentHP);
        dialogueText.text = playerUnit_2.unitName + " heals themselves! Now " + playerUnit_2.unitName + " is ready to keep fighting!";

        yield return new WaitForSeconds(2f);

        state = BattleStates.PLAYER3TURN;
        StartCoroutine(Player3Turn());
        //state = BattleStates.ENEMYTURN;
        //StartCoroutine(EnemyTurn());

    }
    IEnumerator Player3Heal()
    {
        playerUnit_3.Heal(5);

        player3HUD.SetHP(playerUnit_3.currentHP);
        dialogueText.text = playerUnit_3.unitName + " heals themselves! Now " + playerUnit_3.unitName + " is ready to keep fighting!";

        yield return new WaitForSeconds(2f);

        state = BattleStates.PLAYER4TURN;
        StartCoroutine(Player4Turn());
        //state = BattleStates.ENEMYTURN;
        //StartCoroutine(EnemyTurn());

    }
    IEnumerator Player4Heal()
    {
        playerUnit_4.Heal(5);

        player4HUD.SetHP(playerUnit_4.currentHP);
        dialogueText.text = playerUnit_4.unitName + " heals themselves! Now " + playerUnit_4.unitName + " is ready to keep fighting!";

        yield return new WaitForSeconds(2f);

        state = BattleStates.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator Player1Attack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit_1.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit_1.unitName + " attacks!";
        //damage the enemy
        yield return new WaitForSeconds(1f);

        //Check if enemy is dead
        if(isDead)
        {
            //END BATTLE
            state = BattleStates.WON;
            EndBattle();
            //FOREST
            if(SceneManager.GetActiveScene().buildIndex == 7)
            {
                StartCoroutine(LoadLevel(2));
            }
            //LIGHT FOREST
            if(SceneManager.GetActiveScene().buildIndex == 8)
            {
                StartCoroutine(LoadLevel(3));
            }
            //DUNGEON
            if(SceneManager.GetActiveScene().buildIndex == 9)
            {
                StartCoroutine(LoadLevel(5));
            }
            //yield return new WaitForSeconds(2f);
            //SceneManager.LoadScene("GameplayScene");
        }
        else
        {
            state = BattleStates.PLAYER2TURN;
            StartCoroutine(Player2Turn());
            // ENEMY TURN ORIGINALLY
        }
        //Change state based on what happened
    }
    IEnumerator Player2Attack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit_2.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit_2.unitName + " attacks!";
        //damage the enemy
        yield return new WaitForSeconds(1f);

        //Check if enemy is dead
        if(isDead)
        {
            //END BATTLE
            state = BattleStates.WON;
            EndBattle();
            //FOREST
            if(SceneManager.GetActiveScene().buildIndex == 7)
            {
                StartCoroutine(LoadLevel(2));
            }
            //LIGHT FOREST
            if(SceneManager.GetActiveScene().buildIndex == 8)
            {
                StartCoroutine(LoadLevel(3));
            }
            //DUNGEON
            if(SceneManager.GetActiveScene().buildIndex == 9)
            {
                StartCoroutine(LoadLevel(5));
            }
            //yield return new WaitForSeconds(2f);
            //SceneManager.LoadScene("GameplayScene");
        }
        else
        {
            state = BattleStates.PLAYER3TURN;
            StartCoroutine(Player3Turn());
            // ENEMY TURN
        }
        //Change state based on what happened
    }
    IEnumerator Player3Attack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit_3.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit_3.unitName + " attacks!";
        //damage the enemy
        yield return new WaitForSeconds(1f);

        //Check if enemy is dead
        if(isDead)
        {
            //END BATTLE
            state = BattleStates.WON;
            EndBattle();
            //FOREST
            if(SceneManager.GetActiveScene().buildIndex == 7)
            {
                StartCoroutine(LoadLevel(2));
            }
            //LIGHT FOREST
            if(SceneManager.GetActiveScene().buildIndex == 8)
            {
                StartCoroutine(LoadLevel(3));
            }
            //DUNGEON
            if(SceneManager.GetActiveScene().buildIndex == 9)
            {
                StartCoroutine(LoadLevel(5));
            }
            //yield return new WaitForSeconds(2f);
            //SceneManager.LoadScene("GameplayScene");
        }
        else
        {
            state = BattleStates.PLAYER4TURN;
            StartCoroutine(Player4Turn());
            // ENEMY TURN
        }
        //Change state based on what happened
    }
    IEnumerator Player4Attack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit_4.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = playerUnit_4.unitName + " attacks!";
        //damage the enemy
        yield return new WaitForSeconds(1f);

        //Check if enemy is dead
        if(isDead)
        {
            //END BATTLE
            state = BattleStates.WON;
            EndBattle();
            //FOREST
            if(SceneManager.GetActiveScene().buildIndex == 7)
            {
                StartCoroutine(LoadLevel(2));
            }
            //LIGHT FOREST
            if(SceneManager.GetActiveScene().buildIndex == 8)
            {
                StartCoroutine(LoadLevel(3));
            }
            //DUNGEON
            if(SceneManager.GetActiveScene().buildIndex == 9)
            {
                StartCoroutine(LoadLevel(5));
            }
            //yield return new WaitForSeconds(2f);
            //SceneManager.LoadScene("GameplayScene");
        }
        else
        {
            state = BattleStates.ENEMYTURN;
            StartCoroutine(EnemyTurn());
            // ENEMY TURN
        }
        //Change state based on what happened
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        int unit = Random.Range(1,5);
        switch(unit)
        {
            case 1: 
                is1Dead = playerUnit_1.TakeDamage(enemyUnit.damage);

                player1HUD.SetHP(playerUnit_1.currentHP);

                yield return new WaitForSeconds(1f);
                break;

            case 2:
                is2Dead = playerUnit_2.TakeDamage(enemyUnit.damage);

                player2HUD.SetHP(playerUnit_2.currentHP);

                yield return new WaitForSeconds(1f);
                break;
            case 3:
                is3Dead = playerUnit_3.TakeDamage(enemyUnit.damage);

                player3HUD.SetHP(playerUnit_3.currentHP);

                yield return new WaitForSeconds(1f);
                break;
            case 4:
                is4Dead = playerUnit_4.TakeDamage(enemyUnit.damage);

                player4HUD.SetHP(playerUnit_4.currentHP);

                yield return new WaitForSeconds(1f);
                break;
        }

        if(is1Dead == true && is2Dead == true && is3Dead == true && is4Dead == true)
        {
            allDead = true;
        }
        else
        {
            allDead = false;
        }

        if(allDead)
        {
            state = BattleStates.LOST;

            EndBattle();
            //FOREST
            if(SceneManager.GetActiveScene().buildIndex == 7)
            {
                StartCoroutine(LoadLevel(2));
            }
            //LIGHT FOREST
            if(SceneManager.GetActiveScene().buildIndex == 8)
            {
                StartCoroutine(LoadLevel(3));
            }
            //DUNGEON
            if(SceneManager.GetActiveScene().buildIndex == 9)
            {
                StartCoroutine(LoadLevel(5));
            }
            //yield return new WaitForSeconds(2f);
            //SceneManager.LoadScene("GameplayScene");
        }
        else
        {
            state = BattleStates.PLAYER1TURN;
            StartCoroutine(Player1Turn());
        }
    }

    void EndBattle()
    {
        if(state == BattleStates.WON)
        {
            dialogueText.text = " Y O U W O N !";
            GameManager.instance.GrantXP(enemyXP);
        }
        else
        {
            dialogueText.text = "You were defeated...";
        }
        if(GameManager.instance.isMagicEffect == true)
        {
            GameManager.instance.isMagicEffect = false;
        }
        if(GameManager.instance.isPowerEffect == true)
        {
            GameManager.instance.isPowerEffect = false;
        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait to stop playing
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);

    }
}
