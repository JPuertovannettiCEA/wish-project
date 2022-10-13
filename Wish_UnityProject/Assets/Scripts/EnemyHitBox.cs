using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : Collidable
{
    // Damage
    public int damage;
    public float pushForce;

    protected override void OnCollide(Collider2D col)
    {
        if(col.tag == "Fighter" && col.name == "Player")
        {
            //Create a new damage object 
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            //GameManager.instance.ShowText("BATTLE!", 35, Color.red, transform.position, Vector3.zero, 1.0f);

            col.SendMessage("ReceiveDamage", dmg);
        }
    }
}
