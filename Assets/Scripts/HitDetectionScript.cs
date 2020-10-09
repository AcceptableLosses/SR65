using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetectionScript : MonoBehaviour//this script needs to be used in conjunction with Health.cs
{
    public GameObject parent;
    Health parentHealth;
    // Start is called before the first frame update
    void Start()
    {
        parentHealth = parent.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.parent != gameObject.transform.parent && collider.tag != "Environment")
        {
            Debug.Log(collider.name);
            parentHealth.calculateDamage(collider.gameObject);
        }
    }
}
