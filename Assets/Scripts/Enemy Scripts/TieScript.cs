using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieScript : MonoBehaviour
{
    float thrust = 20.0f;
    double rotationInterval = 2.0f;
    double timeCounter = 0.0f;
    public GameObject Leader;
    float w = 11.0f; //width and height for how far it can move.
    float h = 13.0f;
    private Animator anim;
    public ParticleSystem smoke;
    public ParticleSystem fire;
    public Health healthScript;
    public float collisionDamage = 5.0f;
    Rigidbody tieRig;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tieRig = GetComponent<Rigidbody>();
        healthScript = GetComponent<Health>();

    }

    // Update is called once per frame
    void Update()
    {
       // RotateShip();
        //transform.Translate(transform.forward * Time.deltaTime * 2.0f);
        ClampPosition();

    }

    private void FixedUpdate()
    {
        AddForce();
    }

    void AddForce()
    {
        float acceleration = 1000.0f;
        if (tieRig.velocity.z < 8.0)
        {
            acceleration = 1000.0f;
        }
        else if (tieRig.velocity.z >= 10.0f)
        {
            acceleration = 0.0f;
        }
        tieRig.AddForce(new Vector3(0.0f, 0.0f, acceleration));
        timeCounter += Time.deltaTime;
        if (timeCounter >= rotationInterval)
        {

            timeCounter = 0.0f;
            rotationInterval = Random.Range(2.0f, 6.0f);
            float xRand = Random.Range(-10.0f, 10.0f);
            float yRand = Random.Range(-10.0f, 10.0f);
            Vector3 newDirection = new Vector3(xRand, yRand, 0.0f);
            tieRig.AddForce(newDirection * thrust);
        }
        else if (timeCounter >= 1.0f)
        {
            CheckEdges();
        }
    }

    void RotateShip()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter >= rotationInterval)
        {

            timeCounter = 0.0f;
            rotationInterval = Random.Range(2.0f, 6.0f);
            float xRand = Random.Range(-10.0f, 10.0f);
            float yRand = Random.Range(-10.0f, 10.0f);
            float zRand = Random.Range(-10.0f, 10.0f);
            Quaternion newRotation = Quaternion.Euler(new Vector3(xRand, yRand, zRand));
            transform.rotation = newRotation;
        }
        else if (timeCounter >= 1.0f)
        {
            CheckEdges();
        }
    }

    void ClampPosition() //keep it within certain limits.
    {
        float xLimit = Leader.transform.position.x + w;
        float yLimit = Leader.transform.position.y + h;
        //Vector3 shipPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 shipPos = transform.position;
        shipPos.x = Mathf.Clamp(shipPos.x, -xLimit, xLimit);
        shipPos.y = Mathf.Clamp(shipPos.y, -yLimit, yLimit);
        transform.position = shipPos;

    }

    void CheckEdges()
    {
        float xLimit = Leader.transform.position.x + w;
        float yLimit = Leader.transform.position.y + h;
        Vector3 currentPosition = transform.position;
        bool atEdge = false;
        if (currentPosition.x > (xLimit - .2) || currentPosition.x < (-xLimit + .2))
        {
            atEdge = true;

        }
        else if (currentPosition.y > (yLimit - .2) || currentPosition.y < (-yLimit + .2))
        {
            atEdge = true;
        }
        if (atEdge)
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "EnemyProjectile")
        {
            healthScript.calculateDamage(collision.gameObject);
            anim.SetTrigger("damagePlay");
            if (healthScript.health <= 0)
            {
                Crash();
            }
        }
    }

    void Crash() //when the ship is going down, play animation and particle system.
    {
        anim.SetTrigger("down");
        smoke.Play();
        fire.Play();//has a 2 second delay baked in, so it should only play right when the ship disappears.
        tieRig.AddForce(new Vector3(0.0f, -3000.0f, 0.0f));
        Destroy(gameObject, 3.0f);
    }





}
