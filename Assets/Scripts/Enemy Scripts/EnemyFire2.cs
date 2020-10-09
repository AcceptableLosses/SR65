using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire2 : MonoBehaviour
{
    public GameObject enemyProjectile;

    public GameObject self;

    GameObject target;

    [Header("Fire Settings")]
    public float range;
    public float frequency; //how many shots per second.

    public float lastFired = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 targetPosition)
    {
        if (Time.time - lastFired > (1.0f / frequency))
        {
            Quaternion rotation = Quaternion.LookRotation(targetPosition);
            LaserScript laser = Object.Instantiate(enemyProjectile, transform.position, rotation).GetComponent<LaserScript>();
            laser.Initialize(self);
            lastFired = Time.time;
        }
    }

}
