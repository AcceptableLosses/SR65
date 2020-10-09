using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterScript : MonoBehaviour //Script that controls the boost/brake meter.
{
    public Image meter;
    public GameObject lead;
    LeaderObject LeaderObject;
    float fillAmount; //how much boost power you have.
    // Start is called before the first frame update
    void Start()
    {
        LeaderObject = lead.GetComponent<LeaderObject>();
        
    }

    // Update is called once per frame
    void Update()
    {
        meter.fillAmount = LeaderObject.meter/100;
    }
}
