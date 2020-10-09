using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour //this one is somewhat specific to the Ship script.
{
    private Ship shipScript;
    // Start is called before the first frame update
    void Start()
    {
        shipScript = GetComponent<Ship>();
    }

    public void Activate()
    {
        shipScript.TakeDamageParticles();
        shipScript.Die();

    }
}
