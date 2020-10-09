using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretConeScript : MonoBehaviour
{
    public GameObject turretTop;
    TurretScript parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = turretTop.GetComponent<TurretScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "EnemyProjectile")
        {
            parent.Scan(collider);
        }
    }
}
