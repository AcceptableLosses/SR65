//Controls the size and color of the exhaust.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhaustScript : MonoBehaviour
{
    // Start is called before the first frame update
    bool brake;
    bool accelerate;
    LeaderObject LeaderObject;
    public GameObject leader;
    float xSize = 0.025f;
    float ySize = 0.032f;
    float zSize = 0.24f;//The important one.
    Color standardColor = new Color(186.0f, 0.0f, 192.0f, 255.0f);
    public Material exhaustMaterial;
    ParticleSystem exhaustParticle;
    void Start()
    {
        exhaustParticle = GetComponent<ParticleSystem>();
        LeaderObject = leader.GetComponent<LeaderObject>();

    }

    // Update is called once per frame
    void Update()
    {
        brake = false;
        accelerate = false;
        if (Input.GetButton("Jump"))
        {
            brake = true;
        }
        if (Input.GetButton("Fire3"))
        {
            accelerate = true;
        }
        bool boost = LeaderObject.boostPossible;
        if (!boost)
        {
            brake = false;
            accelerate = false;
        }
        Resize();
    }

    void Resize()
    {
        if (brake)
        {
            transform.localScale = new Vector3(xSize, ySize, zSize/10);
            exhaustParticle.startColor = Color.red;
        }
        else if (accelerate)
        {
            transform.localScale = new Vector3(xSize, ySize, zSize * 10);
            exhaustParticle.startColor = Color.white;
        }
        else
        {
            transform.localScale = new Vector3(xSize, ySize, zSize);
            exhaustParticle.startColor = standardColor;
        }


    }
}
