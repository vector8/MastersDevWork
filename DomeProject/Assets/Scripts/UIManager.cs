using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image power_bar;
    public Image air_bar;
    public Image food_bar;
    public Image water_bar;

    struct barData
    {
       public bool ON; //bool for checking if decreasing fill amount (false) or increasing fill amount (true)
       public float amount; //how much to change the fill by       

       public barData(bool on, float amnt)
        {
            ON = on;
            amount = amnt;
        }

    }

    barData power;
    barData air;
    barData food;
    barData water;

    //Booleans
    public bool solarpanel = false;
    public bool watergenerator = false;

	// Use this for initialization
	void Start () {
        power = new barData(false, 0);
        air   = new barData(false, 0);
        food  = new barData(false, 0);
        water = new barData(false, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //If value for POWER_BAR is triggered to change:
        if (power.ON)
        { 
            power_bar.fillAmount = Mathf.Lerp(power_bar.fillAmount, power.amount, Time.deltaTime/10);
            if (areSimilar(power_bar.fillAmount, power.amount)) { power.ON = false; }
        }
        else if(power.ON == false  && solarpanel == false || power_bar.fillAmount >= 1 || power_bar.fillAmount <= 1 )
        {
            power_bar.fillAmount = power_bar.fillAmount - 0.00000001f;
        }
        if (solarpanel)
        {
            power_bar.fillAmount = Mathf.Lerp(power_bar.fillAmount, 1, Time.deltaTime/10);
        }
        if (watergenerator)
        {
            power_bar.fillAmount = Mathf.Lerp(power_bar.fillAmount, 0, Time.deltaTime / 30);
        }


        //If value for AIR_BAR is triggered to change:
        if (air.ON)
        {
            air_bar.fillAmount = Mathf.Lerp(air_bar.fillAmount, air.amount, Time.deltaTime);
            if (areSimilar(air_bar.fillAmount, air.amount)) { air.ON = false; }
        }
    

        //If value for FOOD_BAR is triggered to change:
        if (food.ON)
        {
            food_bar.fillAmount = Mathf.Lerp(food_bar.fillAmount, food.amount, Time.deltaTime);
            if (areSimilar(food_bar.fillAmount, food.amount)) { food.ON = false; }
        }
       

        //If value for WATER_BAR is triggered to change:
        if (water.ON)
        {
            water_bar.fillAmount = Mathf.Lerp(water_bar.fillAmount, water.amount, Time.deltaTime);
            if (areSimilar(water_bar.fillAmount, water.amount)) { water.ON = false; }
        }
        else if(watergenerator)
        {
            water_bar.fillAmount = Mathf.Lerp(water_bar.fillAmount, 1, Time.deltaTime/50);
        }
       


    }

    //checks if two floats are approximately the same and returns true if that's the case
    public bool areSimilar (float a, float b)
    {
        if (Mathf.Abs(a - b) < 0.1)
        {
            return true;
        }
        else { return false; }
    }

    public void decreasebar(string bar_name, float amount)
    {
        Debug.Log("Decrease bar: " + bar_name);
        switch (bar_name)
        {
            case "power":
                power.amount = power_bar.fillAmount - amount;
                power.ON = true;                
                break;
            
            case "air":
                air.amount = air_bar.fillAmount - amount;
                air.ON = true;
                break;

            case "food":
                food.amount = food_bar.fillAmount - amount;
                food.ON = true;
                break;

            case "water":
                water.amount = water_bar.fillAmount - amount;
                power.ON = true;
                break;
        }
       
    }

    public void increasebar(string bar_name, float amount)
    {
        switch (bar_name)
        {
            case "power":
                power.amount = power_bar.fillAmount + amount;
                power.ON = true;
                break;

            case "air":
                air.amount = air_bar.fillAmount + amount;
                air.ON = true;
                break;

            case "food":
                food.amount = food_bar.fillAmount + amount;
                food.ON = true;
                break;

            case "water":
                water.amount = water_bar.fillAmount + amount;
                power.ON = true;
                break;
        }
    }

    void setSolarpanel(bool on)
    {
        solarpanel = on;
    }
}
