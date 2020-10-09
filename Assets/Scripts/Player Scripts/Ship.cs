using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 currentPos;
    Rigidbody rig;
    public float acceleration = 5.0f; //how fast the ship accelerates to top speed
    public float topSpeed = 10.0f;
    public float lowSpeed = 4.0f;
    public float turnspeed = .5f;
    public float axisSpeed = 0f;
    float maximumTurnX = 0.8f;
    float maximumTurnY = 1.0f;
    public bool Move = false;
    Vector3 leaderPos;
    public float leaderDistance;
    public GameObject Leader;
    public float w = 12.0f;
    public float h = 15.0f;
    public float health = 100.0f;
    private Animator anim;
    public ParticleSystem sparks;
    public ParticleSystem smoke;
    public ParticleSystem flames;
    public GameObject GameController;
    private GameController gcScript;
    private Health healthScript;
    bool gameOver;
    Rigidbody leaderRig;
    GameObject MainCamera;


    
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        currentPos = transform.position;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
        //Leader = GameObject.Find("LeaderObject");
        leaderRig = Leader.GetComponent<Rigidbody>();
        MainCamera = gameObject.transform.Find("Main Camera").gameObject;

        healthScript = GetComponent<Health>();
        leaderPos = Leader.transform.position;

        gcScript = GameController.GetComponent<GameController>();


    }




    // Update is called once per frame
    void Update()
    {
        if (healthScript.health <= 0)
        {
            Die();
        }
        else
        {
            MoveShip();
            ClampPosition();
        }

    }


    public void MoveShip()
    {
        Vector3 cursor = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        cursor.x = cursor.x - .5f;
        cursor.y = cursor.y - .5f;
        Vector3 iCursor = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        float speed = (float)(Time.deltaTime * 8.0); //speed at which you move on the X/Y Plane
        //WASDRotate(iCursor);
        RotateShip2();
        //Vector3 movementTarget = new Vector3(transform.localPosition.x + (Input.GetAxis("Horizontal")), transform.localPosition.y + (-Input.GetAxis("Vertical")), transform.localPosition.z);
        //transform.localPosition = Vector3.Lerp(transform.localPosition, movementTarget, speed);
        //rig.MovePosition(transform.position + (iCursor * speed * Time.deltaTime));
    }
    void ClampPosition()
    {
        float xLimit = Leader.transform.position.x + w;
        float yLimit = Leader.transform.position.y + h;
        //Vector3 shipPos = Camera.main.WorldToViewportPoint(transform.position);
        Vector3 shipPos = transform.position;
        shipPos.x = Mathf.Clamp(shipPos.x, -xLimit, xLimit);
        shipPos.y = Mathf.Clamp(shipPos.y, -yLimit, yLimit);
        //transform.position = shipPos;
        //transform.position = Camera.main.ViewportToWorldPoint(shipPos);
    }

    void RotateShip(Vector3 iCursor)
    {
        //manually clamp the rotation:
        if(iCursor.x > maximumTurnX)
        {
            iCursor.x = maximumTurnX;
        }
        if(iCursor.y > maximumTurnY)
        {
            iCursor.y = maximumTurnY;
        }
        if(iCursor.x < -maximumTurnX)
        {
            iCursor.x = -maximumTurnX;
        }
        if(iCursor.y < -maximumTurnY)
        {
            iCursor.y = -maximumTurnY;
        }
        float xCursor = (iCursor.x * turnspeed * 15.0f * Mathf.Deg2Rad);
        float yCursor = iCursor.y * turnspeed * 15.0f * Mathf.Deg2Rad;
        float zCursor = iCursor.x * turnspeed * Mathf.Deg2Rad;
        float xAngleAround = Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.right);
        float yAngleAround = Vector3.SignedAngle(transform.forward, Vector3.forward, Vector3.up);
        float zAngleAround = Vector3.SignedAngle(transform.right, Vector3.right, Vector3.forward);
        //rotation around z-axis
        if (xCursor < 0 && -zAngleAround > -15.0f)
        {
            transform.Rotate(Vector3.forward, xCursor);
        }
        else if (xCursor > 0 && -zAngleAround < 15.0f)
        {
            transform.Rotate(Vector3.forward, xCursor);
        }

        //rotation around y axis
        if (xCursor < 0 && -yAngleAround > -20.0f)
        {
            transform.Rotate(Vector3.up, xCursor);
        }
        else if (xCursor > 0 && -yAngleAround < 20.0f)
        {
            transform.Rotate(Vector3.up, xCursor);

        }

        //rotation around x-axis
        if (yCursor > 0 && xAngleAround > -30.0f)
        {
            transform.Rotate(Vector3.right, yCursor);
        }

        if (yCursor < 0 && xAngleAround < 30.0f)
        {
            transform.Rotate(Vector3.right, yCursor);
        }

        Quaternion cached_rotation = transform.rotation;
        //Reset the rotation
        Quaternion zeroXRotate = Quaternion.FromToRotation(transform.position, new Vector3(transform.position.x, 0, transform.position.z));
        Quaternion zeroYRotate = Quaternion.FromToRotation(transform.position, new Vector3(0, transform.position.y, transform.position.z));
        Quaternion zeroZRotate = Quaternion.AngleAxis(-transform.eulerAngles.z, Vector3.forward);

        if (xCursor == 0)
        {
            //cached_rotation = Quaternion.Slerp(transform.rotation, zeroZRotate, Time.deltaTime * 5.0f);
            cached_rotation = Quaternion.Slerp(transform.rotation, zeroXRotate, Time.deltaTime * 5.0f);
        }
        if (yCursor == 0)
        {
           // cached_rotation = Quaternion.Slerp(transform.rotation, zeroZRotate, Time.deltaTime * 5.0f);
           cached_rotation = Quaternion.Slerp(transform.rotation, zeroYRotate, Time.deltaTime * 5.0f);

        }
    
        transform.rotation = cached_rotation;
    }

    void RotateShip2()
    {
        float xTurn = Input.GetAxis("X-Turn");
        float yTurn = Input.GetAxis("Y-Turn");
        float zTurn = Input.GetAxis("Horizontal");
        Quaternion cachedRotation = transform.rotation;
        float currentX = cachedRotation.eulerAngles.x;
        float currentY = cachedRotation.eulerAngles.y;
        transform.Rotate(xTurn * turnspeed, yTurn * turnspeed, zTurn * turnspeed * .5f);
    }

    bool Range(float Value, float Target, float Tolerance) 
    {
        if((Mathf.Abs(Target - Value) < (Tolerance)) && (Mathf.Abs(Target + Value) > (Tolerance)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void WASDRotate(Vector3 iCursor)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float rotationMax = 20.0f;
        int num1 = 1;
        int num2 = 1;
        if (vertical > 0)
        {
            num1 = 2;
        }
        else if (vertical < 0)
        {
            num1 = -2;
        }
        if (horizontal > 0)
        {
            num2 = 3;
        }
        else if (horizontal < 0)
        {
            num2 = -4;
        }
        int result = num1 * num2;
        Quaternion cached_rotation = transform.rotation;
        Quaternion neutralRot = Quaternion.Euler(0, 0, 0);
        Quaternion urRot = Quaternion.Euler(-rotationMax, rotationMax, 0);
        Quaternion drRot = Quaternion.Euler(rotationMax, rotationMax, 0);
        Quaternion ulRot = Quaternion.Euler(-rotationMax, -rotationMax, 0);
        Quaternion dlRot = Quaternion.Euler(rotationMax, -rotationMax, 0);
        Quaternion uRot = Quaternion.Euler(-rotationMax, 0, 0);
        Quaternion dRot = Quaternion.Euler(rotationMax, 0, 0);
        Quaternion lRot = Quaternion.Euler(0, -rotationMax, 0);
        Quaternion rRot = Quaternion.Euler(0, rotationMax, 0);
        //choose which rotation is needed
        float rotSpeed = 3.0f * Time.deltaTime;
        if(result == 1)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, neutralRot, rotSpeed * 3.0f);
        }
        else if (result == 8)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, ulRot, rotSpeed);
        }
        else if (result == -2)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, uRot, rotSpeed);
        }
        else if (result == -6)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, urRot, rotSpeed);
        }
        else if (result == 3)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, rRot, rotSpeed);
        }
        else if (result == -4)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, lRot, rotSpeed);
        }
        else if (result == -8)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, dlRot, rotSpeed);
        }
        else if (result == 2)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, dRot, rotSpeed);
        }
        else if (result == 6)
        {
            cached_rotation = Quaternion.Slerp(cached_rotation, drRot, rotSpeed);
        }
        transform.rotation = cached_rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "FProjectile")
        {
            healthScript.calculateDamage(collision.gameObject);
            anim.SetTrigger("damagePlay");
            TakeDamageParticles();
            StartCoroutine("VHSEffectActivate");

        }
    }
    public void TakeDamageParticles() //activates the smoke and sparks effects. 
    {
        smoke.Play();
        sparks.Play();
    }

    IEnumerator VHSEffectActivate()
    {
        MainCamera.GetComponent<VHSPostProcessEffect>().enabled = true;
        yield return new WaitForSeconds(2);
        MainCamera.GetComponent<VHSPostProcessEffect>().enabled = false;
        yield return new WaitForSeconds(0);
    }

    public void Die()
    {
        rig.AddForce(new Vector3(0, -6000.0f, 6000.0f));
        rig.AddRelativeTorque(new Vector3(10000.0f, 10000.0f, 10000.0f));
        Destroy(gameObject, 1.5f);
        
        if (gameOver == false)
        {
            flames.Play();
            anim.SetTrigger("dead");
            rig.AddForce(new Vector3(0, -6000.0f, 6000.0f));
            rig.AddRelativeTorque(new Vector3(10000.0f, 10000.0f, 10000.0f));
            TakeDamageParticles();
            gameOver = true;
        }

    }

}
