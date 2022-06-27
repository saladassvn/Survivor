using System.Collections;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{

    public Stat speed;
    public Stat health;
    public Stat armor;


    public float currentHealth { get; protected set; }

    private void Awake()
    {
        currentHealth = health.GetValue();
    }



    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;

    }
     

}
