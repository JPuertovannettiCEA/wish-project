using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyHitBox : Collidable
{
    // Damage
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D col)
    {
        if(this.name == "Enemy_Forest" && col.tag == "Fighter" && col.name == "Player")
        {
            //HERE IS WHEN THE BATTLE SCENE COMES IN
            GameManager.instance.SaveState();
            SceneManager.LoadScene("BattleSceneForest");
        }
        if(this.name == "Enemy_LightForest" && col.tag == "Fighter" && col.name == "Player")
        {
            //HERE IS WHEN THE BATTLE SCENE COMES IN
            GameManager.instance.SaveState();
            SceneManager.LoadScene("BattleSceneLightForest");
        }
        if(this.name == "Enemy_Dungeon" && col.tag == "Fighter" && col.name == "Player")
        {
            //HERE IS WHEN THE BATTLE SCENE COMES IN
            GameManager.instance.SaveState();
            SceneManager.LoadScene("BattleSceneDungeon");
        }
        if(this.name == "Boss" && col.tag == "Fighter" && col.name == "Player")
        {
            GameManager.instance.SaveState();
            SceneManager.LoadScene("BattleSceneBoss");
        }
    }
}
