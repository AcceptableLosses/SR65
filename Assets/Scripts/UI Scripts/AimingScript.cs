using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimingScript : MonoBehaviour //will need to be directly connected to FireController.cs I think this is unavoidable.
{
    public bool missileLock = false;
    private Vector3 maxBoxSize = new Vector3(1.5f, 1.5f, 1.5f);
    private Vector3 minBoxSize = new Vector3(0.5f, 0.5f, 0.5f);
    public float currentSizeChange = 1.0f;//controls polarity of size change.
    private Vector3 sizeChange = new Vector3(.01f, .01f, .01f);
    public bool locked = false;
    public Camera cam;
    Image sRend;
    RectTransform boxTransform;
    GameObject LockedOnCHM;
    LockedOnMaster LockedOnMaster;    // Start is called before the first frame update
    RaycastHit hitObject;
    void Start()
    {
        LockedOnCHM = GameObject.Find("LockedOnCHM");
        LockedOnMaster = LockedOnCHM.GetComponent<LockedOnMaster>();
        boxTransform = GetComponent<RectTransform>();
        sRend = GetComponent<Image>();
        sRend.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCrosshair();
        LockOn();
    }
    private void MoveCrosshair()
    {
        Vector3 aimCursor = Input.mousePosition;
        boxTransform.SetPositionAndRotation(aimCursor, Quaternion.identity);
    }
    public void LockOn()
    {
        if (LockedOnMaster.lockedOn)
        {
            sRend.enabled = true;
            if (GetHit(LockedOnMaster.currentTarget))
            {
                currentSizeChange = -1.0f;
                ResizeBox();
                if (boxTransform.localScale.magnitude <= minBoxSize.magnitude)
                {
                    locked = true;
                }
                else
                {
                    locked = false;
                }
            }
            else
            {
                locked = false;
                currentSizeChange = 1.0f;
                ResizeBox();
            }
        }
        else
        {
            sRend.enabled = false;
            boxTransform.localScale = maxBoxSize;
            locked = false;
        }
    }

    public bool GetHit(GameObject target) //checks to see if we are over the same object being targeted.
    {
        Vector3 targetScreen = cam.WorldToScreenPoint(target.transform.position);
        Vector2 targetScreenPos = targetScreen;
        bool hit = RectTransformUtility.RectangleContainsScreenPoint(boxTransform, targetScreen);
        return hit;
    }

    public void ResizeBox() //shrinks boxSize and scale when target is inside, expands when not. 
    {
        if(currentSizeChange < 0 && boxTransform.localScale.magnitude > minBoxSize.magnitude)
        {
            boxTransform.localScale += sizeChange * currentSizeChange;
        }
        else if (currentSizeChange > 0 && boxTransform.localScale.magnitude < maxBoxSize.magnitude)
        {
            boxTransform.localScale += sizeChange * currentSizeChange;
        }
    }


}
