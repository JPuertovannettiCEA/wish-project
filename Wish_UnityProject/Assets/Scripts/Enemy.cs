using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 1;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPos;
    //public Damage damage;

    // Hitbox for weapon but not that important for this game
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        //damage.damageAmount = 1;
        playerTransform = GameManager.instance.player.transform;
        startingPos = transform.position;
        //hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Is the player in range? 
        if(Vector3.Distance(playerTransform.position, startingPos) < chaseLength)
        {
            if(Vector3.Distance(playerTransform.position,startingPos) < triggerLength)
            {
                chasing = true;
            }

            if(chasing)
            {
                if(!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPos - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPos - transform.position);
            chasing = true;
        }

        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter,hits);
        for (int i = hits.Length - 1; i >= 0 ; i--)
        {
            if(hits[i] == null)
            {
                continue;
            }

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //The array is not cleaned up, so we do it ourself
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        //Reward for XP to the player
        //GameManager.instance.experience += xpValue;
        //GameManager.instance.ShowText("+" + xpValue + " Experience", 35, Color.mageneta, transform.position, Vector3.up * 49, 1.0f);
    }

}
