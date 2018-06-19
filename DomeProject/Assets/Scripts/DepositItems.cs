using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositItems : MonoBehaviour {

    public UIManager UIManager;
    public DiscoverItems discoverItems;
    public GameObject smokePrefab;

    bool wood = true;
    bool apples = true;
    bool apple_seeds = true;
    bool corn = true;
    bool biodiesel = true;
    bool potato = true;
    bool berry = true;
    bool solar_panel = true;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void OnColli
    public void OnTriggerStay(Collider other)
    {
        Debug.Log("Collided! ");

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided 2! ");
        //TODO: Adjust the amounts so it is balanced
        //TODO: update the UI to display the list of discovered deposit and its affect on the bars
        //Detect the item being deposited through the tag then updae the values of the bar
        switch (collision.gameObject.tag)
        {
            case "wood":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("power", 0.3f);
                UIManager.decreasebar("air", 0.15f);
                if (wood) { discoverItems.wood.display_once = true; wood = false; }
                
                break;
            case "apple":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("food", 0.1f);
                if (apples) { discoverItems.apples.display_once = true; apples = false; }
                break;
            case "apple_fertz":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("food", 0.15f);
                break;
            case "apple_seeds":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("air", 0.3f);
                if (apple_seeds) { discoverItems.apple_seeds.display_once = true; apple_seeds = false; }
                break;
            case "corn":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("food", 0.15f);
                if (corn) { discoverItems.corn.display_once = true; corn = false; }
                break;
            case "corn_fertz":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("food", 0.2f);
                break;
            case "biodiesel":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("power", 0.5f);
                UIManager.decreasebar("food", 0.3f);
                if (biodiesel) { discoverItems.biodiesel.display_once = true; biodiesel = false; }
                break;
            case "potato":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("food", 0.3f);
                if (potato) { discoverItems.potato.display_once = true; potato = false; }
                break;
            case "berry":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("food", 0.1f);
                if (berry) { discoverItems.berry.display_once = true; berry = false; }
                break;
            case "berry_fertz":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("food", 0.15f);
                break;
            case "solar_panel":
                createrSmoke();
                Destroy(collision.gameObject);
                UIManager.increasebar("power", 0.4f); //Ongoing?
                if (solar_panel) { discoverItems.solar_panel.display_once = true; solar_panel = false; }
                break;
            
            //water generator is something you turn on and is already provided.
            //case "water_generator":
            //    UIManager.decreasebar("power", 0.3f);
            //    UIManager.increasebar("water", 0.15f); //Ongoing? //if power is 0 then water goes down?
            //    break;

        }


    }

    public void createrSmoke()
    {
        GameObject smokeInstance = Instantiate(smokePrefab);
        smokeInstance.transform.position = transform.position;
    }
}
