using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class LeaderObject : MonoBehaviour
{
    float topSpeed;
    float acceleration;
    public GameObject Ship;
    Ship shipScript;
    Rigidbody leaderRig;
    public float meter = 100.0f; // how long the ship can accelerate or brake for.
    public bool boostPossible;
    float lowSpeed;
    float highSpeed = 15f;
    public GameObject GameController;
    private GameController gcScript;
    public GameObject PostProcessing; //Need to access this in order to adjust it while boosting.
    ChromaticAberration chromeAb;
    LensDistortion lensDist;
    PostProcessVolume volume;
    // Start is called before the first frame update
    void Start()
    {
        Ship = GameObject.Find("Ship");
        shipScript = Ship.GetComponent<Ship>();
        topSpeed = shipScript.topSpeed;
        lowSpeed = shipScript.lowSpeed;
        acceleration = 10.0f;
        leaderRig = Ship.GetComponent<Rigidbody>();
        gcScript = GameController.GetComponent<GameController>();

        InitializePostProcessing();
    }

    void InitializePostProcessing()
    {
        volume = PostProcessing.GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out chromeAb);
        volume.profile.TryGetSettings(out lensDist);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 shipPos = Ship.transform.position;
        volume.profile.TryGetSettings(out chromeAb);
        volume.profile.TryGetSettings(out lensDist);
        if (gcScript.dead == true)
        {
            Stop();//cease movement
        }
        else
        {
            Brake();
        }
    }

    void HandleCoroutines(string startRoutine)
    {
        if (startRoutine == "BoostEffect")
        {
            StopAllCoroutines();
            StartCoroutine(ChromaticEffect(true));
            StartCoroutine(LensEffect(true));
        }
        else if (startRoutine == "BrakeEffect")
        {
            StopAllCoroutines();
            StartCoroutine(ChromaticEffect(false));
            StartCoroutine(LensEffect(false));
        }
    }

    void Brake() //brake or accelerate the ship
    {

        bool brake = false;
        if (Input.GetButton("Jump"))
        {
            HandleCoroutines("BrakeEffect");
            brake = true;
            if (meter > 0)
            {
                meter = meter - (40 * Time.deltaTime);
            }
        }
        bool accelerate = false;
        if (Input.GetButton("Fire3"))
        {
            HandleCoroutines("BoostEffect");
            accelerate = true;
            if (meter > 0)
            {
                meter = meter - (40 * Time.deltaTime);
            }
        }
        if (!brake && !accelerate && (meter < 100))
        {
            HandleCoroutines("BrakeEffect");
            meter = meter + (10 * Time.deltaTime);
        }
        boostPossible = false;
        if (meter > 0.1) //check to see if boost meter is empty
        {
            boostPossible = true;
        }
        if (boostPossible == false && accelerate == true)
        {
            HandleCoroutines("BrakeEffect");
        }
        if (!boostPossible) //if empty, don't brake/accelerate
        {
            brake = false;
            accelerate = false;
        }
        Vector3 velocity = transform.forward * acceleration * Time.deltaTime;
        if (leaderRig.velocity.z < topSpeed && !brake && !accelerate) //while accelerating to cruising speed.
        {
            //leaderRig.AddForce(transform.forward * acceleration);
            leaderRig.MovePosition(transform.localPosition + velocity);
        }
        if (brake)
        {
            //leaderRig.AddForce(-transform.forward * acceleration);
            leaderRig.MovePosition(transform.localPosition + (velocity * 0.5f));
        }
        if (leaderRig.velocity.z < highSpeed && accelerate && !brake)
        {
            //leaderRig.AddForce(transform.forward * acceleration * 10.0f);
            leaderRig.MovePosition(transform.localPosition + velocity * 2.0f);
        }
        if (leaderRig.velocity.z > topSpeed && !accelerate)
        {
            //leaderRig.AddForce(-transform.forward * acceleration);
            leaderRig.MovePosition(transform.localPosition + velocity * .5f);
        }
    }

    IEnumerator ChromaticEffect(bool boost)
    {
        float chromeNormal = 0.377f;
        float chromeHigh = 0.8f;
        if (boost)
        {
            for (float c = chromeAb.intensity.value; c <= chromeHigh; c = c + .3f)
            {
                chromeAb.intensity.value = chromeAb.intensity.value + .3f;
                yield return null;
            }
        }
        else
        {
            for (float c = chromeAb.intensity.value; c >= chromeNormal; c = c - .3f)
            {
                chromeAb.intensity.value = chromeAb.intensity.value - .3f;

                yield return null;
            }
        }
    }

    IEnumerator LensEffect(bool boost)
    {
        float lensNormal = 25.4f;
        float lensLow = -50.0f;
        if(boost)
        {
            for (float l = lensDist.intensity.value; l >= lensLow; l = l - 2.0f)
            {
                lensDist.intensity.value = lensDist.intensity.value - 2.0f;
                yield return null;
            }
        }
        else
        {
            for (float l = lensDist.intensity.value; l <= lensNormal; l = l + 2.0f)
            {
                lensDist.intensity.value = lensDist.intensity.value + 2.0f;
                yield return null;
            }
        }

    }

    void Stop()
    {
        leaderRig.velocity = Vector3.zero;
        leaderRig.angularVelocity = Vector3.zero;
    }


}
