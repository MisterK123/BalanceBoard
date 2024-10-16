using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using TMPro;
using UnityEngine.UIElements;

public class movement : MonoBehaviour
{
    // Bluetooth serial setup
    SerialPort stream = new SerialPort("COM4", 57600);
    string[] startValues;

    // Movement variables
    float currentX;
    float currentY;
    float changeX;
    float changeY;
    float velX = 0;
    public float velY = 0;
    float difference;

    public bool running = true;
    
   
    // debug text + score
    public TMP_Text forwardText;
    public TMP_Text horizontalText;
    public TMP_Text xVelocity;
    public TMP_Text yVelocity;
    public TMP_Text scoreText;
    float score;

    // respawn things
    public bool destroyObstacle = false;
    float destroyTimer;
    
    //game over text
    public TMP_Text gameOverText;
    public GameObject playAgainButton;
    public GameObject exitButton;

    public GameObject[] spawners;

    // difficulty scaling
    float difficultyTimer = 0f;
    public float spawnerSpeed = 1.5f;

    void Start()
    {
        stream.Open();
        string startValue = stream.ReadLine();
        Debug.Log(startValue.ToString());
        startValues = startValue.Split(",");
        Debug.Log("Start Values: " + startValue.ToString());
        scoreText.enabled = true;


        hideDeathScreen();
        
        
    }
    void OnCollisionEnter(Collision collision)
    {
        gameOver();
    }
    public void gameOver()
    {
        playAgainButton.SetActive(true);
        exitButton.SetActive(true );
        gameOverText.enabled = true;
        for( int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SetActive(false);
        }
        running = false;
        spawnerSpeed = 1.5f;
    }
    public void hideDeathScreen()
    {
        playAgainButton.SetActive(false);
        exitButton.SetActive(false);
        gameOverText.enabled = false;
        score = 0;
        transform.position = new Vector3(62,36.71f, 0);
        for (int i = 0; i < spawners.Length; i++)
        {
            spawners[i].SetActive(true);
        }
        running = true;
        velX = 0;
        velY = 0;
        destroyObstacle = true;
        destroyTimer = 0;
        spawnerSpeed = 1.5f;
    }

    void Update()
    {
        destroyTimer += 1 * Time.deltaTime;
        difficultyTimer += 1f * Time.deltaTime;

        spawnerSpeed -= (spawnerSpeed * 0.02f)*Time.deltaTime;
        Debug.Log(spawnerSpeed.ToString());

        // respawn destroy obstacles
        if (destroyTimer > 1)
        {
            destroyObstacle = false;
            
        }
            // Get info from serial port
        string value = stream.ReadLine();
        string[] values = value.Split(",");
        currentX = float.Parse(values[0]) - float.Parse(startValues[0]);
        currentY = float.Parse(values[1]) - float.Parse(startValues[1]);
        
        // Debug text
        horizontalText.text = "X angle: " + (Mathf.Floor(currentX * 10f) / 10f).ToString();
        forwardText.text = "Y angle: " + (Mathf.Floor(currentY * 10f) / 10f).ToString();
        xVelocity.text = "X velocity: " + (Mathf.Floor(velX * 10f) / 10f).ToString();
        yVelocity.text = "Y velocity: " + (Mathf.Floor(velY * 10f) / 10f).ToString();

        // Horizontal accleration and deceleration
        if (currentX > 5 || currentX < -5){changeX = (currentX * Time.deltaTime) / 20;}
        else {changeX = (0 - velX) / 10;}
        velX += changeX;

        // Forward accleration and deceleration
        if (currentY > 10 || currentY < -10) { changeY = (currentY * Time.deltaTime) / 20; }
        else {changeY = (0 - velY) / 10;}
        if (changeY < 0 || velY <= 10)
        {
            velY += changeY;
            if (velY < 0) { velY = 0; }
            if(velY > 10) { velY = 10; }
        }

        //score calculation
        if (running) { 
            if (currentX > 30 || currentX < -30) { score += 5 * Time.deltaTime; }
            score += (1+1 * velY ) * Time.deltaTime;
            scoreText.text = (Mathf.Floor(score * 10f) / 10f).ToString();
        }


        // Update Objects position
        if (running) { transform.position += new Vector3(0, 0, velX); }

    }
}
