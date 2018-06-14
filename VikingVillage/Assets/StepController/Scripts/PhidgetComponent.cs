using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Phidgets;
using UnityStandardAssets.CrossPlatformInput;

public class PhidgetComponent : MonoBehaviour
{
    public GameObject calibratingPanel;
    public float calibrationTime = 5.0f;
    public float stepThreshold = 0.02f;
    public float stepCooldown = 0.5f;

    private Bridge bridge;

    private float calibrationTimer = 0;
    private float bridge1Calibration = 0;
    private float bridge2Calibration = 0;
    private float calibrationCount = 0;

    private bool lastStepRight = false;

    private float stepCooldownTimer = 0f;

    // Use this for initialization
    void Start()
    {
        bridge = new Bridge();
        bridge.open();
        bridge.waitForAttachment(1000);

        if (bridge.Attached)
        {
            bridge.bridges[0].Enabled = true;
            bridge.bridges[3].Enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bridge.Attached)
        {
            if (calibrationTimer < calibrationTime)
            {
                // still calibrating
                calibrationTimer += Time.deltaTime;

                calibrationCount++;
                bridge1Calibration += (float)bridge.bridges[0].BridgeValue;
                bridge2Calibration += (float)bridge.bridges[3].BridgeValue;

                if (calibrationTimer >= calibrationTime)
                {
                    bridge1Calibration /= calibrationCount;
                    bridge2Calibration /= calibrationCount;
                    calibratingPanel.SetActive(false);
                }
            }
            else
            {
                // done calibrating
                float bridge1Value = Mathf.Abs((float)(bridge.bridges[0].BridgeValue - bridge1Calibration));
                float bridge2Value = Mathf.Abs((float)(bridge.bridges[3].BridgeValue - bridge2Calibration));

                //print("" + bridge1Value + ", " + bridge2Value);
                //print(Input.GetAxis("Horizontal"));

                float delta = bridge1Value - bridge2Value;

                if (lastStepRight && delta < -stepThreshold)
                {
                    lastStepRight = !lastStepRight;
                    print("Step left!");
                    CustomInput.SetAxis("Vertical", 1f);
                    stepCooldownTimer = 0f;
                }
                else if (!lastStepRight && delta > stepThreshold)
                {
                    lastStepRight = !lastStepRight;
                    print("Step right!");
                    CustomInput.SetAxis("Vertical", 1f);
                    stepCooldownTimer = 0f;
                }
                else if (stepCooldownTimer < stepCooldown)
                {
                    stepCooldownTimer += Time.deltaTime;
                    if (stepCooldownTimer > stepCooldown)
                    {
                        CustomInput.SetAxis("Vertical", 0f);
                    }
                }
            }
        }
    }
}
