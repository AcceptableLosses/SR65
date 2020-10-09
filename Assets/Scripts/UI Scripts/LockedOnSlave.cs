using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedOnSlave : MonoBehaviour
{
    public GameObject target;
    GameObject LockedOnCHM;
    LockedOnMaster LockedOnMaster;
    // Start is called before the first frame update
    void Start()
    {
        LockedOnCHM = GameObject.Find("LockedOnCHM");
        LockedOnMaster = LockedOnCHM.GetComponent<LockedOnMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        target = LockedOnMaster.currentTarget;
        if(target != null)
        {
            transform.position = target.transform.position;
        }
        
    }
}
