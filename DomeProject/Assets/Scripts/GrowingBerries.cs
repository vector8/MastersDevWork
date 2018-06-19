using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBerries : MonoBehaviour
{
    public float startFraction;
    public float growthTime; // Time to fully grow in seconds
    public GameObject berryPrefab;

    private float u = 0.0f;
    private float randomizedGrowthTime;

    private Vector3 startScale, endScale;

    // Use this for initialization
    void Start()
    {
        startScale = startFraction * transform.localScale;
        endScale = transform.localScale;

        float margin = 0.1f * growthTime;
        randomizedGrowthTime = Random.Range(growthTime - margin, growthTime + margin);
    }

    // Update is called once per frame
    void Update()
    {
        u += (Time.deltaTime / randomizedGrowthTime);

        transform.localScale = Vector3.Lerp(startScale, endScale, u);

        if(u >= 1f)
        {
            GameObject berryInstance = Instantiate(berryPrefab);
            berryInstance.transform.SetParent(transform.parent);
            berryInstance.transform.position = transform.position;
            berryInstance.transform.rotation = transform.rotation;
            berryInstance.transform.localScale = transform.localScale;
            berryInstance.transform.SetParent(null);

            resetScale();
        }
    }

    private void resetScale()
    {
        transform.localScale = startScale;
        u = 0.0f;
    }
}
