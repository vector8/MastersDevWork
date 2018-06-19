using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmManager : MonoBehaviour {
    
    public Transform CseedSpawner;
    public Transform PseedSpawner;
    public GameObject cornSeedGO;
    public GameObject potatoSeedGO;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCornPlanted()
    {
        Instantiate(cornSeedGO, CseedSpawner,true);
        //Change texture,material or w.e
    }

    public void OnPotatoPlanted()
    {
        Instantiate(potatoSeedGO, PseedSpawner,true);
        //Change texture,material or w.e
    }
}
