﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class ArduinoComponent : MonoBehaviour
{
    public float stepThreshold = 50f;
    public float stepCooldown = 0.25f;
    public string port;

    private bool lastStepRight = false;
    private float stepCooldownTimer = 0f;
    private SerialPort sp;

    // Use this for initialization
    void Start()
    {
        sp = new SerialPort(port, 9600);
        sp.Open();
        sp.ReadTimeout = 10;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            //char[] seps = { ',' };
            //string[] arduinoOutput = (sp.ReadLine()).Split(seps, System.StringSplitOptions.None);
            //if(arduinoOutput.Length == 2)
            //{
            //    int fsrValue1 = int.Parse(arduinoOutput[0]);
            //    int fsrValue2 = int.Parse(arduinoOutput[1]);

            //    print("" + fsrValue1 + ", " + fsrValue2);

            //    float delta = fsrValue1 - fsrValue2;

            //    if (lastStepRight && delta < -stepThreshold)
            //    {
            //        lastStepRight = !lastStepRight;
            //        print("Step left!");
            //        CustomInput.SetAxis("Vertical", 1f);
            //        stepCooldownTimer = 0f;
            //    }
            //    else if (!lastStepRight && delta > stepThreshold)
            //    {
            //        lastStepRight = !lastStepRight;
            //        print("Step right!");
            //        CustomInput.SetAxis("Vertical", 1f);
            //        stepCooldownTimer = 0f;
            //    }
            //}
            string arduinoOutput = sp.ReadLine();
            print(arduinoOutput);
            if (arduinoOutput[0] == '0' && !lastStepRight && arduinoOutput[1] == '1')
            {
                lastStepRight = !lastStepRight;
                print("Step right!");
                CustomInput.SetAxis("Vertical", 1f);
                stepCooldownTimer = 0f;
            }
            else if (arduinoOutput[1] == '0' && lastStepRight && arduinoOutput[0] == '1')
            {
                lastStepRight = !lastStepRight;
                print("Step left!");
                CustomInput.SetAxis("Vertical", 1f);
                stepCooldownTimer = 0f;
            }
        }
        catch(System.Exception)
        {
        }

        if (stepCooldownTimer < stepCooldown)
        {
            stepCooldownTimer += Time.deltaTime;
            if (stepCooldownTimer > stepCooldown)
            {
                CustomInput.SetAxis("Vertical", 0f);
            }
        }

        //print("" + bridge1Value + ", " + bridge2Value);
        //print(Input.GetAxis("Horizontal"));

        //float delta = bridge1Value - bridge2Value;

        //if (lastStepRight && delta < -stepThreshold)
        //{
        //    lastStepRight = !lastStepRight;
        //    print("Step left!");
        //    CustomInput.SetAxis("Vertical", 1f);
        //    stepCooldownTimer = 0f;
        //}
        //else if (!lastStepRight && delta > stepThreshold)
        //{
        //    lastStepRight = !lastStepRight;
        //    print("Step right!");
        //    CustomInput.SetAxis("Vertical", 1f);
        //    stepCooldownTimer = 0f;
        //}
        //else if (stepCooldownTimer < stepCooldown)
        //{
        //    stepCooldownTimer += Time.deltaTime;
        //    if (stepCooldownTimer > stepCooldown)
        //    {
        //        CustomInput.SetAxis("Vertical", 0f);
        //    }
        //}
    }

    private void OnApplicationQuit()
    {
        sp.Close();
    }
}