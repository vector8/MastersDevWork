using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiscoverItems : MonoBehaviour {

    public class DiscoveredItemView
    {
        public Image item_deposited_icon;
        public Image bar_decreasing_icon;
        public Image bar_increasing_icon;

        //public DiscoveredItemView(Image item_icon, Image bar_icon1, Image bar_icon2)
        //{
        //    item_deposited_icon = item_icon;
        //    bar_decreasing_icon = bar_icon1;
        //    bar_increasing_icon = bar_icon2;
        //}

        public DiscoveredItemView(Transform rootView)
        {
            item_deposited_icon = rootView.Find("ItemAdded_icon").GetComponent<Image>();
            bar_decreasing_icon = rootView.Find("BarIncrease_icon").GetComponent<Image>();
            bar_increasing_icon = rootView.Find("BarDecrease_icon").GetComponent<Image>();
        }
    }

    public Sprite [] bar_icons;
    public Sprite[] item_icons;
    public GameObject ListItemPrefab;
    public GameObject ContentPanel;
    public bool check = false;

    public struct deposited_element
    {
        public bool display_once;
        public Sprite item_index;
        public Sprite decrease_index;
        public Sprite increase_index;

        public deposited_element(Sprite item, Sprite decrease, Sprite increase)
        {
            display_once = false;
            item_index = item;
            decrease_index = decrease;
            increase_index = increase;
        }
    }

    public deposited_element wood;
    public deposited_element apples;
    public deposited_element apple_seeds;
    public deposited_element corn;
    public deposited_element biodiesel;
    public deposited_element potato;
    public deposited_element berry;
    public deposited_element solar_panel;

    // Use this for initialization
    void Start () {

        //check = false;

        ////Assign all the children of the content panel to an array.
        //LayoutElement[] myLayoutElements = ContentPanel.GetComponentsInChildren<LayoutElement>();

        ////For each child in the array change its LayoutElement's preferred height size to 100.
        //foreach (LayoutElement element in myLayoutElements)
        //{
        //    element.preferredHeight = 100f;
        //}

        //0=power, 1=oxygen, 2=food, 3=water, 4=non
        wood = new deposited_element(item_icons[0], bar_icons[0], bar_icons[1]);
        apples = new deposited_element(item_icons[0], bar_icons[4], bar_icons[2]);
        apple_seeds = new deposited_element(item_icons[0], bar_icons[4], bar_icons[1]);
        corn = new deposited_element(item_icons[0], bar_icons[4], bar_icons[2]);
        biodiesel = new deposited_element(item_icons[0], bar_icons[2], bar_icons[0]);
        potato = new deposited_element(item_icons[0], bar_icons[4], bar_icons[2]);
        berry = new deposited_element(item_icons[0], bar_icons[4], bar_icons[2]);
        solar_panel = new deposited_element(item_icons[0], bar_icons[4], bar_icons[0]);
    }
	
	// Update is called once per frame
	void Update () {

        if (wood.display_once)          { addItem(wood);        wood.display_once = false; }
        if (apples.display_once)        { addItem(apples);      apples.display_once = false; }
        if (apple_seeds.display_once)   { addItem(apple_seeds); apple_seeds.display_once = false; }
        if (corn.display_once)          { addItem(corn);        corn.display_once = false; }
        if (biodiesel.display_once)     { addItem(biodiesel);   biodiesel.display_once = false; }
        if (potato.display_once)        { addItem(potato);      potato.display_once = false; }
        if (berry.display_once)         { addItem(berry);       berry.display_once = false; }
        if (solar_panel.display_once)   { addItem(solar_panel); solar_panel.display_once = false; }

    }

    void addItem(deposited_element element)
    {

        var instance = GameObject.Instantiate(ListItemPrefab) as GameObject;
        instance.transform.SetParent(ContentPanel.transform, false);
        DiscoveredItemView view = new DiscoveredItemView(instance.transform);
        view.bar_decreasing_icon.sprite = element.decrease_index;  //bar_icons[decrease_index];
        view.bar_increasing_icon.sprite = element.increase_index; //bar_icons[increase_index];
        view.item_deposited_icon.sprite = element.item_index;//bar_icons[item_index];
        instance.transform.localScale = Vector3.one;
        element.display_once = false;
       
    }
}
