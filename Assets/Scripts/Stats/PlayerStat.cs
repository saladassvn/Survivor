using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : CharacterStat
{
    HealthBar healthBar;
    public ParticleSystem bloodShot;

    public SpriteRenderer spriteRenderer = null;
    public Color flickerColor = Color.red;
        
    private Color startingColor = Color.clear;


    private void Start()
    {
        healthBar = GameObject.Find("Healthbar").GetComponent<HealthBar>();
        healthBar.SexMaxHealth(health.GetValue());

        startingColor = spriteRenderer.color;

    }

    

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            float damage = collision.gameObject.GetComponent<EnemyStat>().damage.GetValue();
            TakeDamage(damage);
            healthBar.SetHealth(currentHealth);
            spriteRenderer.color = flickerColor;
            bloodShot.Play();
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            spriteRenderer.color = startingColor;
        }
    }



}
