using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walking : MonoBehaviour
{
    public Transform l_hip, r_hip, l_knee, r_knee, l_ankle, r_ankle;

    private float relativeVelocity, relativeCycleLength, cycleDuration, relativeCycleTime;

    // Use this for initialization
    void Start()
    {
        relativeVelocity = (l_hip.position.y + r_hip.position.y) / 2f;
        relativeCycleLength = 1.346f * Mathf.Sqrt(relativeVelocity);
        cycleDuration = relativeCycleLength / relativeVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        relativeCycleTime = Time.time / cycleDuration;
    }
}
