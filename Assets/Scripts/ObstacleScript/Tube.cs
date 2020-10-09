using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube : MonoBehaviour
{
    [SerializeField]
    public float rotatingSpeed;
    Vector3 centerAxis;
    // Start is called before the first frame update
    void Start()
    {
        centerAxis = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, centerAxis, rotatingSpeed * Time.deltaTime);
    }
}
