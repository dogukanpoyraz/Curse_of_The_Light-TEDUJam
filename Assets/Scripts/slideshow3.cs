using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class slideshow3 : MonoBehaviour
{
    public Text text1;
    public Image image1;
    public Sprite[] SpriteArray;
    public string[] textArray;
    private int currentImage = 0;
    private int currentText = 0;
    public float fadeTime = 1f;
    public bool fadefinished = false;
    bool finished = false;


    private float deltaTime = 0.0f;

    public float timer1 = 5.0f;
    private float timer1Remaining = 5.0f;
    public bool timer1IsRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        image1.canvasRenderer.SetAlpha(0.0f);
        image1.sprite = SpriteArray[currentImage];
        text1.text = textArray[currentText];
        image1.CrossFadeAlpha(1, fadeTime, false);

        bool timer1IsRunning = false;
        // timer1 should be bigger than fade time 
        timer1Remaining = timer1;
    }

    // Update is called once per frame
    void Update()
    {

        if (timer1IsRunning)
        {
            if (timer1Remaining > 0)
            {
                //timer1Remaining -= Time.deltaTime;

                image1.CrossFadeAlpha(1, fadeTime, false);
            }

            /*
            else
            {
                UnityEngine.Debug.Log("Timer1 has finished!");
                timer1Remaining = timer1;
                fadefinished = true;
                //image1.sprite = SpriteArray[currentImage];
                timer1IsRunning = !timer1IsRunning;

                image1.CrossFadeAlpha(0, fadeTime, false);
            }
            */
        }

        if (Input.GetMouseButtonDown(0))
        {
            UnityEngine.Debug.Log("Pressed primary button.");
            currentImage++;
            currentText++;

            if (currentImage >= SpriteArray.Length)
            {
                SceneManager.LoadScene("DemoScene");
            }

            fade();
        }

        void fade()
        {
            image1.canvasRenderer.SetAlpha(0.0f);
            image1.sprite = SpriteArray[currentImage];
            text1.text = textArray[currentText];
            timer1Remaining = timer1;
            timer1IsRunning = true;
        }
    }
}