using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject enemyProjectile;
    public GameObject playerShip;
    public GameObject self;
    
    [Header("Fire Settings")]
    public float range;
    public float frequency; //how many shots per second.

    protected float lastFired = 0.0f;//time of last firing.
    protected RaycastHit lineOfFire; //what's directly in the path between the player and the blaster. Will return with the player if the path is clear.
    // Start is called before the first frame update
    void Start()
    {
        if(frequency == 0.0f)
        {
            frequency = 1.0f; //make sure that we don't divide by zero. 
        }
    }

    // Update is called once per frame
    void Update()
    {
        AcquireTarget();
    }

    protected void AcquireTarget()
    {
        Vector3 shipDirection = playerShip.transform.position - transform.position;
        Ray targetRay = new Ray(transform.position, shipDirection);
        bool hit = Physics.Raycast(targetRay, out lineOfFire, range);
        if (hit)
        {
            Fire(shipDirection);
        }
    }

    protected void Fire(Vector3 targetPosition)
    {
        if (Time.time - lastFired > (1.0f/frequency))
        {
            Quaternion rotation = Quaternion.LookRotation(targetPosition);
            LaserScript laser = Object.Instantiate(enemyProjectile, transform.position, rotation).GetComponent<LaserScript>();
            laser.Initialize(self);
            lastFired = Time.time;
        }
    }
}
