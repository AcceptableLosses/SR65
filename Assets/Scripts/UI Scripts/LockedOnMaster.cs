using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedOnMaster : MonoBehaviour
{
    public Renderer mRend;
    public bool lockedOn;
    public bool searching = false;
    public float time = 0.0f;
    public GameObject currentTarget = null;
    public GameObject lockedOnS;
    public GameObject Ship;
    GameObject currentLock;
    int lockedTargets = 0;
    // Start is called before the first frame update
    void Start()
    {
        mRend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MakeVisible();
        if (searching)
        {
            AcquireTarget();
        }
        if(lockedOn)
        {
            MissileLock();
        }
    }

    void MakeVisible() //determines when the crosshair is shown, it's always present.
    {
        if (Input.GetKey(KeyCode.Space))
        {

            time += Time.deltaTime;
            if (time > 1.0f)
            {
                if (!lockedOn)
                {
                    mRend.enabled = true;
                    searching = true;

                }
            }

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            mRend.enabled = false;
            searching = false;
            lockedOn = false;
            Destroy(currentLock);
            lockedTargets = 0;
            time = 0.0f;
        }
    }

    public void AcquireTarget() //while in "searching" mode, find the first target that the crosshair hovers over.
    {

        RaycastHit target = isOverTarget();
        if (target.collider != null && searching == true && !target.collider.CompareTag("Environment"))
        {
            currentTarget = target.collider.gameObject;
            lockedOn = true;
            searching = false;
            mRend.enabled = false;
        }
    }

    void MissileLock() //"I have you now"
    {
        if (lockedTargets < 1)
        {
            lockedTargets++;
            currentLock = Instantiate(lockedOnS, currentTarget.transform.position, Quaternion.identity);
        }
    }

    RaycastHit isOverTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray(Ship.transform.position, transform.forward);
        Physics.Raycast(ray, out hit);
        return hit;
    }
}
