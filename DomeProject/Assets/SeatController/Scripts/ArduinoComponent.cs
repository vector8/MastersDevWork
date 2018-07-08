using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoComponent : MonoBehaviour
{
    public float stepCooldown = 0.25f;
    public string port;

    public Transform directionFacing;
    public float angle;

    private bool canMove = false;
    private float stepCooldownTimer = 0f;
    private SerialPort sp;

    // Use this for initialization
    void Start()
    {
        sp = new SerialPort(port, 9600);
        sp.Open();
        sp.ReadTimeout = 10;
        stepCooldownTimer = stepCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            string arduinoOutput = sp.ReadLine();
            print(arduinoOutput);
            if (arduinoOutput[0] == '1')
            {
                canMove = true;
            }
            else 
            {
                canMove = false;
            }
        }
        catch(System.Exception)
        {
        }

        if (canMove)
        {
            // check angle of vive tracker
            angle = directionFacing.localRotation.eulerAngles.x;

            if (angle < 23f)
            {
                // move forward
                Vector3 direction = Vector3.ProjectOnPlane(-1f * directionFacing.forward, Vector3.up);
                CustomInput.SetAxis("Vertical", direction.z);
                CustomInput.SetAxis("Horizontal", direction.x);

            }
            else if(angle > 33f)
            {
                // move backward
                Vector3 direction = Vector3.ProjectOnPlane(directionFacing.forward, Vector3.up);
                CustomInput.SetAxis("Vertical", direction.z);
                CustomInput.SetAxis("Horizontal", direction.x);
            }
            else
            {
                // stop moving
                CustomInput.SetAxis("Vertical", 0);
                CustomInput.SetAxis("Horizontal", 0);
            }
        }
        else
        {
            CustomInput.SetAxis("Vertical", 0);
            CustomInput.SetAxis("Horizontal", 0);
        }
    }

    private void OnApplicationQuit()
    {
        sp.Close();
    }
}