using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 leaderPosition;
    public GameObject leader;
    LeaderObject LeaderObject;

    public GameObject Ship;
    public Vector3 shipPosition;

    float offset;

    float w = 20.0f;//distance allowed to travel on the x axis.
    float h = 18.0f;//distance allowed to travel on the y axis.

    AudioSource cameraSound;
    public AudioClip GameOverYeah;
    bool gameOver = false;

    public GameObject GameController;
    private GameController gcScript;

    float smooth = 2.5f;
    float tiltAngle = 20.0f;

    float neutralZ = -0.075f;
    float accelZ = -0.10f;
    float slowZ = -0.05f;

    // Start is called before the first frame update
    void Start()
    {
        leaderPosition = leader.transform.position;
        shipPosition = Ship.transform.position;
        offset = Vector3.Distance(leaderPosition, transform.position);
        LeaderObject = leader.GetComponent<LeaderObject>();
        cameraSound = GetComponent<AudioSource>();
        gcScript = GameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gcScript.dead == true)
        {
            GameOver();
        }
        else
        {
            CameraDistance();
            //CameraSpeed();
            //CameraPosition();
        }
    }
    private void LateUpdate()
    {
        float tiltAroundZ = Input.GetAxis("X-Turn") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Y-Turn") * tiltAngle;
        var target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
    }

    void CameraDistance()
    {
        bool brake = false;
        if (Input.GetButton("Jump"))
        {
            brake = true;
        }
        bool accelerate = false;
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
        Vector3 positionTarget = transform.localPosition;
        if(!brake && !accelerate)
        {
            positionTarget.z = neutralZ;
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionTarget, smooth * Time.deltaTime);
        }
        else if(brake)
        {
            positionTarget.z = slowZ;
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionTarget, smooth * Time.deltaTime);
        }
        else if(accelerate)
        {
            positionTarget.z = accelZ;
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionTarget, smooth * Time.deltaTime);
        }


    }
    void CameraSpeed()
    {
        leaderPosition = leader.transform.position;
        shipPosition = Ship.transform.position;
        Vector3 localPosition = transform.localPosition;    
        //Giving the ability to brake and speed up. Actual speed will be controlled by leader object.
        //The following really just places the ship in a position relative to the leader.
        bool brake = false;
        if (Input.GetButton("Jump"))
        {
            brake = true;
        }
        bool accelerate = false;
        if (Input.GetButton("Fire3"))
        {
            accelerate = true;
        }
        bool boost = LeaderObject.boostPossible;
        if(!boost)
        {
            brake = false;
            accelerate = false;
        }
        if (!brake && !accelerate)
        {
            Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);
            Vector3 Center = new Vector3(transform.position.x, transform.position.y, shipPosition.z - 5.0f);
            transform.position = Vector3.MoveTowards(transform.position, Center, 30.0f * Time.deltaTime);
            //transform.position = Vector3.SmoothDamp(transform.position, Center, ref velocity, 0.1f);
        }
        if (brake)
        {
            Vector3 Center = new Vector3(transform.position.x, transform.position.y, shipPosition.z - 3.0f);
            if (!Range(transform.position.z, Center.z, .2f))
            {
                transform.position = Vector3.MoveTowards(transform.position, Center, 15.0f * Time.deltaTime);
            }
            else
            {
                transform.position = Center;
            }
        }
        if (accelerate)
        {
            Vector3 Center = new Vector3(transform.position.x, transform.position.y, shipPosition.z - 7.0f);
            if (!Range(transform.position.z, Center.z, .2f))
            {
                transform.position = Vector3.MoveTowards(transform.position, Center, 15.0f * Time.deltaTime);
            }
            else
            {
                transform.position = Center;
            }

        }
    }

    void CameraPosition() //follow the ship to a point.
    {
        float xTarget = Ship.transform.position.x;
        float yTarget = Ship.transform.position.y;

        //float xLimit = leader.transform.position.x + w;
        //float yLimit = leader.transform.position.y + h;

        //float xClamp = Mathf.Clamp(xTarget, -xLimit, xLimit);
        //float yClamp = Mathf.Clamp(yTarget, -yLimit, yLimit);

        //Vector3 targetPosition = new Vector3(xClamp, yClamp + .5f, transform.position.z);
        Vector3 targetPosition = new Vector3(xTarget, yTarget + .5f, transform.position.z); //the .5 is the offset to make it not directly behind the ship. Looks better that way.
        transform.position = Vector3.Lerp(transform.position, targetPosition, 10.0f * Time.deltaTime);
        Vector3 velocity = Vector3.zero;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.01f);
        
    }

    bool Range(float Value, float Target, float Tolerance)
    {
        if ((Mathf.Abs(Target - Value) < (Tolerance)) && (Mathf.Abs(Target + Value) > (Tolerance)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    void GameOver()
    {
        if (gameOver == false)
        {
            gameOver = true;
            transform.parent = null;
            cameraSound.clip = GameOverYeah;
            cameraSound.Play();
        }
    }
}
