using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : CharacterStat
{
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;

    [HideInInspector]
    public Stat damage;
    GameObject expGem;
    

    public WaveSpawner waveSpawner;
    Material material;
    float fade = 1f;
    float loss = 2f;

    #region flash

    private SpriteRenderer spriteRenderer;


    private Material originalMaterial;

    private Coroutine flashRoutine;

    #endregion

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        expGem = this.gameObject.transform.GetChild(0).gameObject;
        originalMaterial = spriteRenderer.material;
        waveSpawner = GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>();

        material = GetComponent<SpriteRenderer>().material;
    }

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }
    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Flash();
            TakeDamage(collision.gameObject.GetComponent<DestroyWhenCollide>().kimProjectile.damage);
        }
    }

    private void Update()
    {
        EnemyDie();
    }

    void EnemyDie()
    {
        if (currentHealth <= 0)
        {
            //currentHealth = health.GetValue();
            fade -= loss * Time.deltaTime ;
            if(fade <= 0f)
            {
                fade = 0f;
            }
            StartCoroutine(WaitBeforeDie());
        }
    }

    IEnumerator WaitBeforeDie()
    {

        material.SetFloat("_Fade", fade);
        yield return new WaitForSeconds(.32f);
        Dying();
    }

    void Dying()
    {

        //waveSpawner.QueuedEnemy.Enqueue(gameObject);
        expGem.transform.parent = null;
        expGem.transform.localScale = new Vector3(1, 1, 1);
        expGem.SetActive(true);
        Destroy(this.gameObject);
        
        
    }
}
