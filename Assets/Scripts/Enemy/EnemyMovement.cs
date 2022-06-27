using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    public float moveSpeed = 2f;
    Rigidbody2D rb;
    Vector2 movement;
    public float maxDistance = 10f;
    EnemyStat enemyStat;



    bool facingRight = true;

    public bool getKnock;
    IEnumerator knockOutCoroutine;
    Vector2 direction = Vector2.zero;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        enemyStat = this.gameObject.GetComponent<EnemyStat>();
        rb = this.GetComponent<Rigidbody2D>();
        moveSpeed = enemyStat.speed.GetValue();

    }

    private void Update()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
        movement = direction;

        if (player.transform.position.x < gameObject.transform.position.x && facingRight)
            Flip();
        if (player.transform.position.x > gameObject.transform.position.x && !facingRight)
            Flip();
        //moveCharacter();
        Reposition();

    }

    private void FixedUpdate()
    {
        if (!getKnock)
        {
            moveCharacter(movement);
        }       
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 tmpScale = gameObject.transform.localScale;
        tmpScale.x *= -1;
        gameObject.transform.localScale = tmpScale;
    }

    void Reposition()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 15f)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position = player.transform.position + (direction * maxDistance);
        }
    }

    public void Impact(Vector2 impactPosition,float impactEffect, float knockOutTime)
    {
        Vector2 impactDirection = ((Vector2)transform.position - impactPosition).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(impactDirection * impactEffect, ForceMode2D.Impulse);
        KnockOut(knockOutTime);
    }

    void KnockOut(float seconds)
    {
        if (knockOutCoroutine != null)
            StopCoroutine(knockOutCoroutine);

        knockOutCoroutine = KnockOutCoroutine(seconds);
        StartCoroutine(knockOutCoroutine);
    }

    IEnumerator KnockOutCoroutine(float seconds)
    {
        direction = Vector2.zero;
        getKnock = true;
        yield return new WaitForSeconds(seconds);
        getKnock = false;
    }

}
