using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseTheForce : MonoBehaviour
{
    public float moveSpeed;
    public Transform playerPosition, playerDirection;
    public SampleApp muse;

    private bool connected = false;
    private bool connecting = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (muse.muses.Count > 0 && !connected && !connecting)
        {
            connecting = true;
            muse.connect();
        }
    }

    public void museConnected()
    {
        connecting = false;
        connected = true;
    }

    public void museDisconnected()
    {
        connecting = false;
        connected = false;
    }

    public void receiveMuseData(string data)
    {
        string toSearchFor = "DataPacketValue\":";
        string dataParsed = data.Substring(data.IndexOf(toSearchFor) + toSearchFor.Length);
        dataParsed = dataParsed.Substring(0, dataParsed.IndexOf(",\"TimeStamp\":"));

        string[] dataTokens = dataParsed.Split("[],".ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);

        List<float> dataFloats = new List<float>();
        for (int i = 0; i < 4; i++)
        {
            float token = float.Parse(dataTokens[i]);
            if (!Mathf.Approximately(token, 0))
            {
                dataFloats.Add(token);
            }
        }
        //Vector4 dataFloats = new Vector4();
        //dataFloats.x = float.Parse(dataTokens[0]);
        //dataFloats.y = float.Parse(dataTokens[1]);
        //dataFloats.z = float.Parse(dataTokens[2]);
        //dataFloats.w = float.Parse(dataTokens[3]);

        //float avg = (dataFloats.x + dataFloats.y + dataFloats.z + dataFloats.w) / 4.0f;
        float sum = 0;
        foreach (float f in dataFloats)
        {
            sum += f;
        }
        float avg = 0;
        if (dataFloats.Count > 0)
        {
            avg = sum / dataFloats.Count;
        }
        //avg -= 0.5f; // normalize between -0.5 and 0.5

        if (avg > 0.5f)
        {
            Vector3 pos = playerPosition.position;
            Vector3 forward = playerDirection.forward;
            forward.y = 0;
            forward = Vector3.Normalize(forward);
            pos += (avg * forward * moveSpeed * Time.deltaTime);
            playerPosition.position = pos;
        }
    }
}
