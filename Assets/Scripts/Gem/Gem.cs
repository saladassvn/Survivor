using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasTarget;
    Vector3 targetPosition;
    public float speed = 5f;
    public float recoilForce = .001f;
    public int expAmmount;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Collect()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        if (hasTarget)
        {
            Vector2 targetDirection = (targetPosition - transform.position).normalized;

            //rb.AddForce(-targetDirection * recoilForce, ForceMode2D.Impulse);

            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * speed;
        }
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        hasTarget = true;
    }
}
