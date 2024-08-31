using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using TMPro;
using UnityEngine.UIElements;
public class movement : MonoBehaviour
{
    SerialPort stream = new SerialPort("COM9", 57600);
    float currentX;
    float currentY;
    float changeX;
    float changeY;
    float velX = 0;
    public float velY= 0;
    float difference;
    string[] startValues;
    public TMP_Text forwardText;
    public TMP_Text horizontalText;
    public TMP_Text xVelocity;
    public TMP_Text yVelocity;

    void Start()
    {
        stream.Open();
        string startValue = stream.ReadLine();
        startValues = startValue.Split(",");
        Debug.Log("Start Values: " + startValue.ToString());

    }

    void Update()
    {
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
        if (currentX > 20 || currentX < -20){changeX = (currentX * Time.deltaTime) / 20;}
        else {changeX = (0 - velX) / 10;}
        velX += changeX;

        // Forward accleration and deceleration
        if (currentY > 20 || currentY < -20) { changeY = (currentY * Time.deltaTime) / 20; }
        else {changeY = (0 - velY) / 10;}
        if(changeY<0 || velY<= 10) { velY += changeY; }
        

        // Update Objects position
        transform.position += new Vector3(0, 0, velX);

    }
}
