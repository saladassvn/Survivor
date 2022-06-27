using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KimProjectile : Projectile
{
    //public GameObject kimPref;
    ObjectPool objectPool;    

    public int maxPassEnemy;
    float nextSpawnTime;
    public GameObject player;
    CheckRadius checkRadius;

    private void Start()
    {
        objectPool = ObjectPool.Instance;
        checkRadius = GameObject.FindGameObjectWithTag("CheckRadius").GetComponent<CheckRadius>();

    }

    private void Update()
    {
        if (Time.time > nextSpawnTime && checkRadius.closetEnemy != null)
        {
            Shooting(checkRadius.closetEnemy, player);
            nextSpawnTime = Time.time + coolDown;
        }
    }


    public void Shooting(GameObject enemy, GameObject player)
    {
        Vector3 look = new Vector2(enemy.transform.position.x - player.transform.position.x, enemy.transform.position.y - player.transform.position.y);
        float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;


        //GameObject kim = Instantiate(kimPref, player.transform.position, Quaternion.identity);

        GameObject kim = objectPool.SpawnFromPool("KimProjectile", player.transform.position, Quaternion.identity);
        Rigidbody2D rb = kim.GetComponent<Rigidbody2D>();
        rb.rotation = angle;
        rb.AddForce(look.normalized * speed, ForceMode2D.Impulse);
    }


}
