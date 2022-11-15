using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] 
public class PlayerController : Mover
{
    private Animator animator;

    public float x, y;

    //private float speed = 1.0f;
    
    
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x,y,0));
    }

    private void Update()
    {
        animator.SetFloat("MoveX",x);
        animator.SetFloat("MoveY",y);
        animator.SetFloat("Speed",new Vector3(x,y,0).sqrMagnitude);
    }

    public void OnLevelUp()
    {

    }

    public void SetLevel(int level)
    {
        for(int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }

}
