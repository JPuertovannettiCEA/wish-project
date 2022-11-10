using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleStates
{
    START,
    PLAYERTURN,
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

    public TMP_Text dialogueText;

    public BattleHUD playerHUD;

    public BattleHUD enemyHUD;

    public BattleStates state;
    private void Start()
    {
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

        playerHUD.SetHUD(playerUnit_1);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(3f);

        state = BattleStates.PLAYERTURN;
        PlayerTurn();

    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action!";
    }

    public void OnAttackButton()
    {
        if(state != BattleStates.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }
    public void OnHealButton()
    {
        if(state != BattleStates.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerHeal());
    }

    public void OnEscapeButton()
    {
        if(state != BattleStates.PLAYERTURN)
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

    IEnumerator PlayerHeal()
    {
        playerUnit_1.Heal(5);

        playerHUD.SetHP(playerUnit_1.currentHP);
        dialogueText.text = "You heal yourself! You feel renewed!";

        yield return new WaitForSeconds(3f);

        state = BattleStates.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit_1.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "Player attacks!";
        //damage the enemy
        yield return new WaitForSeconds(2f);

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

        yield return new WaitForSeconds(2f);

        bool isDead = playerUnit_1.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit_1.currentHP);

        yield return new WaitForSeconds(2f);

        if(isDead)
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
            state = BattleStates.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleStates.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else
        {
            dialogueText.text = "You were defeated";
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
