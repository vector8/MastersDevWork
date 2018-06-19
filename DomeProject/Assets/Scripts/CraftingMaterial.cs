using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMaterial : MonoBehaviour
{
    public enum MaterialType
    {
        Wood,
        Apple,
        Corn,
        Potato,
        Berry
    };

    public MaterialType type;
}
