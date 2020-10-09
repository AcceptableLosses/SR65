using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSideways : MonoBehaviour
{
    private bool pressedOnce = false;
    private string lastPressed = "startingstring";
    private string keyPressed;
    private float timePassed;
    private float timeSinceBoost; //make sure you don't boost repeatedly
    private float threshold = 0.25f;//how fast you need to press the keys to activate a double tap.
    private float boostAmount = 5.0f;
    private Ship shipScript;
    private Rigidbody shipRig;
    // Update is called once per frame
    private void Awake()
    {
        shipScript = GetComponent<Ship>(); //might need it in order to determine if we're at the limits.
        shipRig = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if(Input.anyKeyDown)
        {
            
            SetKeyPressed();
            if (keyPressed == lastPressed)
            {
                if(Time.time - timePassed < threshold)
                {
                    Dodge(keyPressed);
                }
                timePassed = Time.time;

            }
            else if (keyPressed != "none")
            {
                timePassed = Time.time;
                lastPressed = keyPressed;
            }
            keyPressed = "none";
        }
    }

    void SetKeyPressed()
    {
        
        if (Input.GetKeyDown("w"))
        {
            keyPressed = "w";
            
        }
        else if (Input.GetKeyDown("a"))
        {
            keyPressed = "a";
            
        }
        else if (Input.GetKeyDown("s"))
        {
            keyPressed = "s";
            
        }
        else if (Input.GetKeyDown("d"))
        {
            keyPressed = "d";
            
        }
         
    }

    private void Dodge(string d)
    {
        Vector3 impulseVector = transform.position;
        if (d == "w")
        {
            impulseVector = new Vector3(transform.position.x, transform.position.y - boostAmount, transform.position.z);
        }
        else if (d == "s")
        {
            impulseVector = new Vector3(transform.position.x, transform.position.y + boostAmount, transform.position.z);
        }
        else if (d == "a")
        {
            impulseVector = new Vector3(transform.position.x - boostAmount, transform.position.y, transform.position.z);
        }
        else if (d == "d")
        {
            impulseVector = new Vector3(transform.position.x + boostAmount, transform.position.y, transform.position.z);
        }
        transform.position = Vector3.Lerp(transform.position, impulseVector, 4.0f);


    }
}
