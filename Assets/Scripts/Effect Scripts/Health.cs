using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startingHealth;
    public float health;
    public ParticleSystem deathParticles; //if there's an explosion upon death

    [SerializeField]
    public float deathDelay; //how long after object is killed before it disappears from screen. 
                             //Delay it if there are death effects (explosions, etc.)
    void Awake()
    {
        health = startingHealth;
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }


    }
    public void calculateDamage(GameObject collidedObject) //figure out how much damage should be done upon collision
    {
        collisionDamage cD = collidedObject.GetComponent<collisionDamage>();
        float resultingDamage;
        if (cD != null)
        {
            resultingDamage = cD.damage;
        }
        else
        {
            resultingDamage = 5.0f;
        }
        Damage(resultingDamage);

    }

    public void Die()
    {
        //This function needs to determine if there is a death animation available, to play. 
        Destroy(gameObject, deathDelay);
        if (deathParticles != null)
        {
            deathParticles.Play();
        }
    }
}
