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
        if(col.tag == "Fighter" && col.name == "Player")
        {
            //HERE IS WHEN THE BATTLE SCENE COMES IN
            SceneManager.LoadScene("BattleScene");
            //Create a new damage object 
            /**Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };**/
            //GameManager.instance.ShowText("BATTLE!", 35, Color.red, transform.position, Vector3.zero, 1.0f);

            //col.SendMessage("ReceiveDamage", dmg);
        }
    }
}
