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

        SceneManager.LoadScene("GameplayScene");
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
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("GameplayScene");
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
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("GameplayScene");
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
}
