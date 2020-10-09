using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    [SerializeField]
    private float lowPoint;

    [SerializeField]
    private float highPoint;

    private float startingPoint;
    private bool directionUp;
    // Start is called before the first frame update
    void Start()
    {
        float randF = Random.Range(0.0f, 1.0f);
        if (randF > 0.5f)
        {
            directionUp = true;
        }
        startingPoint = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        DetectEdges();
        MovePosition();
    }

    void MovePosition()
    {
        float change;
        if (directionUp == true)
        {
            change = 0.1f;
        }
        else
        {
            change = -0.1f;
        }
        transform.Translate(new Vector3(change, 0, 0));

    }

    void DetectEdges()
    {
        float currentPos = transform.position.y;
        if (currentPos >= highPoint)
        {
            directionUp = false;
        }
        else if (currentPos <= lowPoint)
        {
            directionUp = true;
        }

    }

}
