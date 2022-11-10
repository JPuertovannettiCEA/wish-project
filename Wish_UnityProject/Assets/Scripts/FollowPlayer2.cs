using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer2 : Mover
{
    private Animator animator;

    public GameObject frontCharacter;

    private float xLast;

    private float yLast;

    private bool isCloser;
        
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        //lastRecord = frontCharacter.transform.position;
        //UpdateMotor(lastRecord);
    }

    private void FixedUpdate()
    {
        
        if(frontCharacter.GetComponent<FollowPlayer>().xLast != 0)
        {
            //UpdateMotor((frontCharacter.transform.position - transform.position).normalized);
            UpdateMotor((frontCharacter.transform.position - new Vector3((transform.position.x - 0.190f),transform.position.y, transform.position.z)).normalized);
            xLast = frontCharacter.GetComponent<FollowPlayer>().xLast;
            isCloser = false;
        }
        if(frontCharacter.GetComponent<FollowPlayer>().yLast != 0)
        {
            //UpdateMotor((frontCharacter.transform.position - new Vector3(transform.position.x,(transform.position.y - 0.306f), transform.position.z).normalized));
            UpdateMotor((frontCharacter.transform.position - transform.position).normalized);
            yLast = frontCharacter.GetComponent<FollowPlayer>().yLast;
            isCloser = false;
            //xLast = 0f;
            //yLast = 0f;
        }
        if(frontCharacter.GetComponent<FollowPlayer>().yLast == 0)
        {
            if(isCloser == false)
            {
                //UpdateMotor((frontCharacter.transform.position - new Vector3(transform.position.x,(transform.position.y - 0.306f), transform.position.z).normalized));
                isCloser = true;
            }
            yLast = 0f;
        }
        if(frontCharacter.GetComponent<FollowPlayer>().xLast == 0)
        {
            if(isCloser == false)
            {
                //UpdateMotor((frontCharacter.transform.position - new Vector3((transform.position.x - 0.243f),transform.position.y, transform.position.z)).normalized);
                isCloser = true;
            }
            xLast = 0f;
        }
        /**xLast = frontCharacter.GetComponent<PlayerController>().x;
        yLast = frontCharacter.GetComponent<PlayerController>().y;

        //FRONT PLAYER LOOKING RIGHT
        if(xLast > 0)
        {
            UpdateMotor(new Vector3(xLast - 0.243f,yLast,0));
        }

        //FRONT PLAYER LOOKING LEFT
        if(xLast < 0)
        {
            UpdateMotor(new Vector3(xLast + 0.243f,yLast,0));
        }
        //FRONT PLAYER LOOKING UP
        if(yLast > 0)
        {
            UpdateMotor(new Vector3(xLast,yLast - 0.306f,0));
        }

        //FRONT PLAYER LOOKING DOWN
        if(yLast < 0)
        {
            UpdateMotor(new Vector3(xLast,yLast + 0.306f,0));
        }


       // UpdateMotor(new Vector3(xLast - 0.243f, yLast, 0));
        //record.Enqueue(lastRecord);
        //if(record.Count > steps)
        //{
        //    UpdateMotor(record.Dequeue());
        //}

        //UpdateMotor(new Vector3(x,y,0));**/
    }

    private void Update()
    {
        
        animator.SetFloat("MoveX",xLast);
        animator.SetFloat("MoveY",yLast);
        animator.SetFloat("Speed",new Vector3(xLast,yLast,0).sqrMagnitude);
        
    }
}
