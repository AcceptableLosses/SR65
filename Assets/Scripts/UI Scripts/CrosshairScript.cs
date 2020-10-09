using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    public Color neutralColor = Color.green;
    public Color hotColor = Color.red;
    public Renderer rend;
    public Material chMat;
    float time;
    // Start is called before the first frame update
    void Start()
    {

        Material chMat = rend.material;


    }

    // Update is called once per frame
    void Update()
    {
        Material chMat = rend.material;
        if (isOverTarget())
        {
            chMat.color = hotColor;
        }
        else
        {
            chMat.color = neutralColor;
        }
        if(Input.GetKey(KeyCode.Space))
        {
        
            time += Time.deltaTime;
            if (time > 1.0f)
            {
                rend.enabled = false;
            }
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            rend.enabled = true;
            time = 0.0f;
        }
    }

    bool isOverTarget()
    {
        RaycastHit possibleTarget;
        bool hit = Physics.Raycast(transform.position, transform.forward, out possibleTarget, Mathf.Infinity);
        if(possibleTarget.collider != null && possibleTarget.collider.CompareTag("Environment"))
        {
            hit = false;
        }
        return hit;
    }
}
