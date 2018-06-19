using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSwitcher : MonoBehaviour
{
    public enum Modes
    {
        Phidgets,
        Arduino
    }

    public Modes mode;
    public PhidgetComponent phidgetComponent;
    public ArduinoComponent arduinoComponent;
    public GameObject phidgetCallibrationPanel;

    // Use this for initialization
    void Start()
    {
        switch (mode)
        {
            case Modes.Phidgets:
                arduinoComponent.enabled = false;
                break;
            case Modes.Arduino:
                phidgetComponent.enabled = false;
                phidgetCallibrationPanel.SetActive(false);
                break;
            default:
                break;
        }
    }
}
