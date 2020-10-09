using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HMeterScript : MonoBehaviour
{
    public Image hMeter; 
    public GameObject player;
    public GameObject GameController;
    private GameController gcScript;
    Health shipHealthScript;
    float fillAmount; //how much boost power you have.
    // Start is called before the first frame update
    void Start()
    {
        shipHealthScript = player.GetComponent<Health>();
        gcScript = GameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        hMeter.fillAmount = shipHealthScript.health/100;
    }
}
