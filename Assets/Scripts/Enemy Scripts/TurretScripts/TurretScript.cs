using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    GameObject CurrentTarget;
    public GameObject TargetingField;
    public GameObject turretBase;
    public GameObject barrel1;
    public GameObject barrel2;
    public int health = 5;
    public float effectiveRange = 40.0f;
    EnemyFire2 blaster1;
    // Start is called before the first frame update
    void Start()
    {
        blaster1 = barrel1.GetComponent<EnemyFire2>();

    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTarget != null)
        {
            Aim(CurrentTarget);
        }
    }

    public GameObject Scan(Collider collider)
    {

        CurrentTarget = collider.gameObject;
        return collider.gameObject;
    }

    private void Aim(GameObject target) //First check to see if target is in range, if not, then end. If so, aim at target.
    {
        float distance = (target.transform.position - transform.position).magnitude;
        if (distance <= effectiveRange)
        {
            Vector3 targetPath = target.transform.position - transform.position;

            Vector3 firePath = targetPath; //get a version that we don't set the y component to zero.

            targetPath.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(targetPath);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
            blaster1.Fire(firePath);
        }
    }



}
