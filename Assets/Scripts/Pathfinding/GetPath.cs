using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPath : MonoBehaviour ///from Sebastian Lague
{
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    public Transform target;
    public float speed = 5.0f;
    public float turnDistance = 5;
    public float turnSpeed = 3;

    public GameObject[] Nodes;
    bool pathComplete = false;
    int nodeIterator = 0;

    Path path;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdatePath());
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
    }

    // Update is called once per frame
    void Update()
    {
        if (pathComplete)
        {
            target = ChooseTarget().transform;
            pathComplete = false;
        }
        
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = new Path(waypoints, transform.position, turnDistance);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    public GameObject ChooseTarget()
    {
        nodeIterator++;
        if (nodeIterator < Nodes.Length)
        {
            return Nodes[nodeIterator];
        }
        else
        {
            nodeIterator = 0;
            return Nodes[nodeIterator];
        }

    }

    IEnumerator UpdatePath()
    {
        if(Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if((target.position- targetPosOld).sqrMagnitude > sqrMoveThreshold)
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
            targetPosOld = target.position;
        }
    }

    IEnumerator FollowPath()
    {
        bool followingPath = true;
        followingPath = true;
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]);

        while (followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);

            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    pathComplete = true;
                    break;
                }
                else
                {
                    pathIndex++;
                }

            }
            if (followingPath)
            {
                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
                transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
            }
            yield return null;
        }




    }
}
