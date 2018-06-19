using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ChoppableTree : MonoBehaviour
{
    private int chopCount = 0;
    private float appleSpawnTimer = 0f;
    private float currentSpawnChance = 0.01f;

    public GameObject logPrefab;
    public GameObject applePrefab;
    public GameObject woodChipPrefab;
    public GameObject smokePrefab;
    public float appleSpawnDelay = 60f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        appleSpawnTimer += Time.deltaTime;

        if(appleSpawnTimer >= appleSpawnDelay)
        {
            float roll = Random.value;
            if(roll <= currentSpawnChance)
            {
                // spawn an apple
                GameObject appleInstance = Instantiate(applePrefab);
                appleInstance.transform.position = transform.position + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(2.1f, 6f), Random.Range(-1.5f, 1.5f));
                currentSpawnChance = 0.01f;
            }
            else
            {
                currentSpawnChance *= 2;
            }

            appleSpawnTimer = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("on collision enter: " + collision.transform.tag);

        if (collision.transform.tag == "Ax")
        {
            SoundPlayOneshot soundPlayOneshot = collision.transform.GetComponent<SoundPlayOneshot>();
            if(soundPlayOneshot != null)
            {
                soundPlayOneshot.Play();
            }
            else
            {
                Debug.LogError("SoundPlayOneshot component not found on axe.");
            }

            GameObject woodchipInstance = Instantiate(woodChipPrefab);
            woodchipInstance.transform.position = collision.contacts[0].point;

            chopCount++;

            if (chopCount > 2)
            {
                // Spawn logs and delete this.
                for (int i = 0; i < 3; i++)
                {
                    GameObject logInstance = Instantiate(logPrefab);
                    logInstance.transform.position = transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 0.5f), Random.Range(-1f, 1f));
                }

                GameObject smokeInstance = Instantiate(smokePrefab);
                smokeInstance.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }
}
