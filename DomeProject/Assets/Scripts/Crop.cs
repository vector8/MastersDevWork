using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class Crop : MonoBehaviour
{

    public LinearMapping[] dirtMovedValue = new LinearMapping[2];
    public FarmManager myFM;
    public bool seedPlaced;
    public bool spawnCrop;
    public GameObject CornGO;
    public GameObject PotaGO;        
    public float waterAmount;
    public float fertBonus;

    private int cornorpot;
    // Use this for initialization
    void Start()
    {
        CornGO.SetActive(false);
        PotaGO.SetActive(false);
        myFM = GameObject.Find("FarmManager").GetComponent<FarmManager>();
    }

   

    public void OnDirtHeld()
    {
        if (seedPlaced == true)
        {
            if (dirtMovedValue[0].value == 1 && dirtMovedValue[1].value == 1 && spawnCrop == false)
            {
                spawnCrop = true;

                Debug.Log("Spawn Crop");
            }
            else if (dirtMovedValue[0].value != 1 || dirtMovedValue[1].value != 1)
            {
                spawnCrop = false;
                Debug.Log("Don't Spawn Crop");
            }
        }
    }



    public void WaterCrop()
    {
       if(spawnCrop == true && seedPlaced == true)
        {
            waterAmount++;
            if(waterAmount > 100)
            {
                if(cornorpot == 1)
                {
                    PotaGO.SetActive(true);
                    CornGO.SetActive(false);
                }
                else if(cornorpot == 0)
                {
                    CornGO.SetActive(true);
                    PotaGO.SetActive(false);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "cornseed")
        {
            Debug.Log("CORN PLANTED");
            seedPlaced = true;
            myFM.OnCornPlanted();

            //Spawn corn
            cornorpot = 0;

            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "potatoseed")
        {
            Debug.Log("POTATO PLANTED");
            seedPlaced = true;
            myFM.OnPotatoPlanted();

            //Spawn Potato
            cornorpot = 1;

            Destroy(collision.collider.gameObject);
        }
    }
}
