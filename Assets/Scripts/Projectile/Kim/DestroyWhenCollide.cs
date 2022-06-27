using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWhenCollide : MonoBehaviour
{
    public float timeBeforeDestroy = 3f;
    public float timer = 0.0f;
    ParticleSystem emit;
    ParticleSystem smoke;
    [HideInInspector]
    public KimProjectile kimProjectile;
    int currentPassEnemy = 0;

    private void Awake()
    {
        emit = this.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        kimProjectile = GameObject.FindGameObjectWithTag("Kim").GetComponent<KimProjectile>();
        smoke = this.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeBeforeDestroy)
        {
            PlayBlast();
            StopTrail();
            this.gameObject.SetActive(false);

            timer = 0;
        }


        CheckParticleSystem();
        DestroyMissle();


    }


    void DestroyMissle()
    {
        if(currentPassEnemy >= kimProjectile.maxPassEnemy)
        {
            PlayBlast();
            StopTrail();
            this.gameObject.SetActive(false);

            currentPassEnemy = 0;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentPassEnemy++;
        }
    }

    void PlayBlast()
    {
        smoke.transform.parent = null;
        smoke.Play();
    }


    private void OnEnable()
    {
        if (emit.isPlaying == false && emit.transform.parent == null)
        {
            emit.transform.parent = this.transform;
            emit.gameObject.SetActive(true);
        }
    }


    void CheckParticleSystem()
    {
        //Smoke Blast
        if(smoke.isPlaying == false && smoke.transform.parent == null)
        {
            smoke.transform.parent = this.transform;
            smoke.transform.position = this.transform.position;
            smoke.gameObject.SetActive(true);
        }
    }
    

    void StopTrail()
    {
        emit.transform.parent = null;
        emit.Stop();
    }

}
