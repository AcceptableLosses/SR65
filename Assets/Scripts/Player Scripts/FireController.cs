using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    public GameObject FriendlyProjectile;
    public GameObject Ship;
    public GameObject LockedProjectile;

    public GameObject Lock;
    LockedOnMaster LockedOnMaster;

    Quaternion FireAngle;

    public GameObject AimingCrosshair;
    AimingScript AimingScript;

    bool fireUponRelease = false;
    ParticleSystem Charging;
    ParticleSystem.EmissionModule em;
    // Start is called before the first frame update
    void Start()
    {
        LockedOnMaster = Lock.GetComponent<LockedOnMaster>();
        Charging = GetComponent<ParticleSystem>();
        em = Charging.emission;
        em.enabled = false;
        AimingScript = AimingCrosshair.GetComponent<AimingScript>();


    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Space))
        {
           LaserScript laser = Object.Instantiate(FriendlyProjectile, transform.position, Ship.transform.rotation).GetComponent<LaserScript>();
            laser.Initialize(Ship);
        }
        if (LockedOnMaster.searching)//enable the charge effect.
        {
            em.enabled = true;
        }
        else if (!LockedOnMaster.searching && !LockedOnMaster.lockedOn)
        {
            em.enabled = false;
        }
        if (LockedOnMaster.lockedOn)
        {
            fireUponRelease = true;


        }
        if(fireUponRelease)
        {
            fireUponRelease = false;
            if (Input.GetKeyUp(KeyCode.Space))
            {
                fireUponRelease = false;
                em.enabled = false;
                Quaternion targetRotation = CreateRotation();
                if(AimingScript.locked)
                {
                    LaserScript laser = Object.Instantiate(LockedProjectile, transform.position, targetRotation).GetComponent<LaserScript>();
                    laser.Initialize(Ship);
                }
                else
                {
                   LaserScript laser =  Object.Instantiate(FriendlyProjectile, transform.position, targetRotation).GetComponent<LaserScript>();
                   laser.Initialize(Ship);
                }
            }
        }

    }

    Quaternion CreateRotation()
    {
        if(LockedOnMaster.currentTarget != null)
        {
            Vector3 lockedPosition = LockedOnMaster.currentTarget.transform.position;
            string name = gameObject.name;
            if (name == "BlasterUR") //adjust so that the lasers fly straight, to some extent.
            {
                lockedPosition = new Vector3(lockedPosition.x + .351f, lockedPosition.y + .25f, lockedPosition.z);
            }
            else if (name == "BlasterLR")
            {
                lockedPosition = new Vector3(lockedPosition.x + .351f, lockedPosition.y - .25f, lockedPosition.z);
            }
            else if (name == "BlasterUL")
            {
                lockedPosition = new Vector3(lockedPosition.x - .351f, lockedPosition.y + .25f, lockedPosition.z);
            }
            else if (name == "BlasterLL")
            {
                lockedPosition = new Vector3(lockedPosition.x - .351f, lockedPosition.y - .25f, lockedPosition.z);
            }
            Vector3 targetPosition = lockedPosition - transform.position;
            Quaternion rotation = Quaternion.LookRotation(targetPosition, Vector3.up);
            return rotation;
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(transform.position, Vector3.up);
            return rotation;
        }
    }
}
