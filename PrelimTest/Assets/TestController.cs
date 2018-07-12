using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour
{
    public GameObject pathGroup1, pathGroup2, pathGroup3;
    public GameObject instructionSign1, instructionSign2;
    public GameObject ball, cube;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBallPickup()
    {
        if (pathGroup1.activeSelf)
        {
            pathGroup1.SetActive(false);
            instructionSign1.SetActive(false);

            pathGroup2.SetActive(true);
            instructionSign2.SetActive(true);
            cube.SetActive(true);
        }
    }

    public void onBallDetach()
    {
        if(pathGroup2.activeSelf)
        {
            // maybe do something to tell them to pick up the ball again?
        }
    }

    public void onCubePickup()
    {
        if (pathGroup2.activeSelf)
        {
            pathGroup2.SetActive(false);
            instructionSign2.SetActive(false);

            pathGroup3.SetActive(true);
        }
    }

    public void onCubeDetach()
    {
        if (pathGroup2.activeSelf)
        {
            // maybe do something to tell them to pick up the ball again?
        }
    }
}
