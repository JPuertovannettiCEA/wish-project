using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitPoint;
    public int maxHitPoint;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    // All fighters can receive damage and Die
    protected virtual void ReceiveDamage(Damage damage)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= damage.damageAmount;
            pushDirection = (transform.position - damage.origin).normalized * damage.pushForce;

            GameManager.instance.ShowText("BATTLE!", 35, Color.red, transform.position, Vector3.zero, 1.0f);

            //DEATH
            if(hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }

        //GameManager.instance.ShowText("BATTLE!", 35, Color.red, transform.position, Vector3.zero, 1.0f);
    }

    protected virtual void Death()
    {
        Debug.Log("YOU ARE DEAD");
    }
}
