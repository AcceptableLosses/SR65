using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public AudioClip TargetSmash;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "FProjectile")
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(TargetSmash, transform.position);




        }
    }
}
