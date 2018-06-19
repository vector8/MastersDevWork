using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{

    public ParticleSystem PSystem;
    public List<ParticleCollisionEvent> CollisionEvents = new List<ParticleCollisionEvent>(8);
    public FarmManager myFM;
    public GameObject WateringCanObject;
 

    // Use this for initialization
    void Start()
    {
        
        WateringCanObject = GameObject.Find("Watering Can");
        myFM = GameObject.Find("FarmManager").GetComponent<FarmManager>();
        PSystem =this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {


       
        if (WateringCanObject.transform.eulerAngles.z < 360 && WateringCanObject.transform.eulerAngles.z > 180)
        {
            Debug.Log("Watering");
            if (PSystem.isPlaying == false)
            {
                PSystem.Play(true);
            }

        }
        if (WateringCanObject.transform.eulerAngles.z < 181 && WateringCanObject.transform.eulerAngles.z > -10)
        {
            Debug.Log("Not Watering");
            if (PSystem.isPlaying == true)
            {
                PSystem.Play(false);
            }


        }
    }

    public void OnParticleCollision(GameObject other)
    {
        int collCount = PSystem.GetSafeCollisionEventSize();

        if (collCount > CollisionEvents.Count)
            CollisionEvents.Add(new ParticleCollisionEvent());


        int eventCount = PSystem.GetCollisionEvents(other, CollisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            if (other.tag == "Water")
            {
                if (other.GetComponent<Crop>().spawnCrop == true)
                {

                    other.GetComponent<Crop>().WaterCrop();
                }
            }
            //TODO: Do your collision stuff here. 
            // You can access the CollisionEvent[i] to obtaion point of intersection, normals that kind of thing
            // You can simply use "other" GameObject to access it's rigidbody to apply force, or check if it implements a class that takes damage or whatever
        }
    }
}
