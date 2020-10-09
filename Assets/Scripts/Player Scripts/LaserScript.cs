using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public GameObject self;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(10, 10);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * 1.5f,Space.World);
        Destroy(gameObject, 3.0f);
    }

    public void Initialize(GameObject self_)
    {
        self = self_;
        Physics.IgnoreCollision(self.GetComponent<Collider>(), gameObject.GetComponent<Collider>());

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != self.name && collision.gameObject.tag != "FProjectile" && collision.gameObject.tag != "EnemyProjectile")
        {
            Destroy(gameObject);
        }
    }
}
