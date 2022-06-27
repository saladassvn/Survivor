using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    bool facingRight = true;
    Vector2 movement;

    public ParticleSystem dust;

    PlayerStat playerStat;

    private void Start()
    {
        playerStat = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStat>();
        moveSpeed = playerStat.speed.GetValue();
    }

    private void Update()
    {
        //Get Input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

     
        Animation();
      
        FlipCharacter();

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    void Animation()
    {
        if (Mathf.Abs(movement.x) + Mathf.Abs(movement.y) == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
    }

    void FlipCharacter()
    {
        if (movement.x < 0 && facingRight)
        {
            Flip();
        }
        else if(movement.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        CreateDust();  
    }

    void CreateDust()
    {
        dust.Play();
    }
}
