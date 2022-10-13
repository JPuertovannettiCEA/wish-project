using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter //ABSTRACT MEANS THAT THIS CAN ONLY BE USED BY INHERITING IT
{
    protected BoxCollider2D boxCollider;
    
    protected Vector3 moveDelta;

    protected RaycastHit2D hit;

    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {

    }

    //FUNCTION FOR PLAYER INPUT
    protected virtual void UpdateMotor(Vector3 Input)
    {
        // Reset moveDelta
        moveDelta = new Vector3(Input.x * xSpeed, Input.y * ySpeed, 0);

        // Swap sprite direction to right or left
        if(moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        } 
        else if(moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }

        // Add push vector, if any
        //moveDelta += pushDirection;

        //Reduce push force every frame, based off recovery speed
        //pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        // Make sure we can move in this direction, by casting a box there first, if the box returns null, were free to move
        // HORIZONTAL
        hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(moveDelta.x,0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Players","Blocking"));
        if(hit.collider == null)
        {
            // Make this thing move! 
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }

        // VERTICAL
        hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Players","Blocking"));
        if(hit.collider == null)
        {
            // Make this thing move! 
            transform.Translate(0,moveDelta.y * Time.deltaTime, 0);
        }
    }
}
