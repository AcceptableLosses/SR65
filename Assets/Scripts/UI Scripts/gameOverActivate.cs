using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameOverActivate : MonoBehaviour
{ 
    public GameObject GameController;
    private GameController gcScript;
    public Image gameOver;
    Gradient gradient;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = GetComponent<Image>();
        gameOver.enabled = false;
        gcScript = GameController.GetComponent<GameController>();

        gradient = new Gradient();
        colorKey = new GradientColorKey[3];
        colorKey[0].color = Color.red;
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.green;
        colorKey[1].time = 0.5f;
        colorKey[2].color = Color.red;
        colorKey[2].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[3];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 1.0f;
        alphaKey[1].time = 0.5f;
        alphaKey[2].alpha = 1.0f;
        alphaKey[2].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);
    }

    // Update is called once per frame
    void Update()
    {
        if(gcScript.dead == true)
        {
            gameOver.enabled = true;
            float colorDial;
            colorDial = Time.fixedTime % 1.0f;
            gameOver.color = gradient.Evaluate(colorDial);
        }
    }
}
