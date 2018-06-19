using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsArea : MonoBehaviour
{
    public Crafting crafting;

    private void OnTriggerEnter(Collider other)
    {
        CraftingMaterial material = other.GetComponent<CraftingMaterial>();
        if (material != null)
        {
            crafting.addMaterial(material);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CraftingMaterial material = other.GetComponent<CraftingMaterial>();
        if (material != null)
        {
            crafting.removeMaterial(material);
        }
    }
}
