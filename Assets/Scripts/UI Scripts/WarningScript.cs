using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour //Controls the warning arrows that flash when too far in one direction.
{
    public GameObject Ship;
    public Renderer wRend;
    public GameObject Leader;
    Ship shipScript;
    string wName;
    float xLimit;
    float yLimit;
    int flash = 15;
    // Start is called before the first frame update
    void Start()
    {
        wName = gameObject.name;
        wRend.enabled = false;
        shipScript = Ship.GetComponent<Ship>();


    }

    // Update is called once per frame
    void Update()
    {
       

    }

    void FixedUpdate()
    {
        wRend.enabled = false;
        int warningFlag = CheckPosition();
        if (warningFlag == 1)
        {
            if (wName == "WarningUp")
            {
                if (flash == 15)
                {
                    wRend.enabled = false;
                    flash = 0;
                }
                else
                {
                    wRend.enabled = true;
                    flash++;
                }

            }
        }
        if (warningFlag == 2)
        {
            if (wName == "WarningDown")
            {
                if (flash == 15)
                {
                    wRend.enabled = false;
                    flash = 0;
                }
                else
                {
                    wRend.enabled = true;
                    flash++;
                }

            }
        }
        if (warningFlag == 3)
        {
            if (wName == "WarningRight")
            {
                if (flash == 15)
                {
                    wRend.enabled = false;
                    flash = 0;
                }
                else
                {
                    wRend.enabled = true;
                    flash++;
                }

            }
        }
        if (warningFlag == 4)
        {
            if (wName == "WarningLeft")
            {
                if (flash == 15)
                {
                    wRend.enabled = false;
                    flash = 0;
                }
                else
                {
                    wRend.enabled = true;
                    flash++;
                }

            }
        }
        if (warningFlag == 0)
        {
            wRend.enabled = false;
        }
    }

    int CheckPosition()
    {
        xLimit = Leader.transform.position.x + shipScript.w;
        yLimit = Leader.transform.position.y + shipScript.h;

        if (Ship.transform.localPosition.y > (yLimit - 1.0f))
        {
            return 1; //WarningUp
        }
        else if (Ship.transform.localPosition.y < (-yLimit + 1.0f))
        {
            return 2; //WarningDown
        }
        else if (Ship.transform.localPosition.x > (xLimit - 1.0f))
        {
            return 3; //WarningRight
        }
        else if (Ship.transform.localPosition.x < (-xLimit + 1.0f))
        {
            return 4; //Warning Left
        }
        else
        {
            return 0;
        }
    }
}
