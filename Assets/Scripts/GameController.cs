using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Ship;
    public bool dead = false;

    private Ship shipScript;
    private Health shipHealthScript;
    // Start is called before the first frame update
    void Start()
    {
        shipScript = Ship.GetComponent<Ship>();
        shipHealthScript = Ship.GetComponent<Health>();
        dead = false;

    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        float life = shipHealthScript.health;
        if (life <= 0)
        {
          dead = true;
        }
    }
}
