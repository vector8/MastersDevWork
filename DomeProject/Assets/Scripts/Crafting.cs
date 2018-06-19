using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class Crafting : MonoBehaviour
{
    public enum Recipes
    {
        AppleSeed,
        Fertilizer,
        Biodiesel,
        SolarPanel
    };

    public Dictionary<CraftingMaterial.MaterialType, int> materialTypes;
    public GameObject[] craftingProductPrefabs;
    public Text screenText;
    public Image screenOutline;
    public int[] screenOutlinePositions;
    public CircularDrive circularDrive;
    public LinearDrive craftButtonLinearDrive;
    public GameObject applePrefab;
    public Transform productOutput;

    private int selectedRecipeIndex = 0;
    private bool[] recipesCraftable;
    private int currentAngle;
    private bool craftSwitch = false;
    private List<CraftingMaterial> materials;

    private const string DISABLED_COLOR = "<color=#bbbbbbff>";
    private const string ENABLED_COLOR = "<color=#ffff00ff>";
    private const string COLOR_END = "</color>";
    private const int ANGLE_DIFF = 90;

    // Use this for initialization
    void Start()
    {
        materialTypes = new Dictionary<CraftingMaterial.MaterialType, int>();
        var matTypes = Enum.GetValues(typeof(CraftingMaterial.MaterialType)).Cast<CraftingMaterial.MaterialType>();

        foreach (CraftingMaterial.MaterialType type in matTypes)
        {
            materialTypes[type] = 0;
        }

        var recipes = Enum.GetValues(typeof(Recipes)).Cast<Recipes>();
        recipesCraftable = new bool[recipes.Count()];
        for(int i = 0; i < recipes.Count(); i++)
        {
            recipesCraftable[i] = false;
        }

        materials = new List<CraftingMaterial>();
    }

    // Update is called once per frame
    void Update()
    {
        // update screen ui
        while(circularDrive.outAngle - currentAngle > ANGLE_DIFF)
        {
            currentAngle += ANGLE_DIFF;
            moveOutlineDown();
        }
        while (currentAngle - circularDrive.outAngle > ANGLE_DIFF)
        {
            currentAngle -= ANGLE_DIFF;
            moveOutlineUp();
        }

        // Check recipes
        // HARD CODING YEAAAAAAA
        string text = "";

        if (materialTypes[CraftingMaterial.MaterialType.Apple] > 0)
        {
            text += ENABLED_COLOR;
            recipesCraftable[(int)Recipes.AppleSeed] = true;
        }
        else
        {
            text += DISABLED_COLOR;
            recipesCraftable[(int)Recipes.AppleSeed] = false;
        }
        text += "Apple -> Apple Seed" + COLOR_END + "\n";

        if (materialTypes[CraftingMaterial.MaterialType.Wood] > 0)
        {
            text += ENABLED_COLOR;
            recipesCraftable[(int)Recipes.Fertilizer] = true;
        }
        else
        {
            text += DISABLED_COLOR;
            recipesCraftable[(int)Recipes.Fertilizer] = false;
        }
        text += "Wood -> Fertilizer" + COLOR_END + "\n";

        if (materialTypes[CraftingMaterial.MaterialType.Corn] > 0)
        {
            text += ENABLED_COLOR;
            recipesCraftable[(int)Recipes.Biodiesel] = true;
        }
        else
        {
            text += DISABLED_COLOR;
            recipesCraftable[(int)Recipes.Biodiesel] = false;
        }
        text += "Corn -> Biodiesel" + COLOR_END + "\n";

        if (materialTypes[CraftingMaterial.MaterialType.Berry] > 0)
        {
            text += ENABLED_COLOR;
            recipesCraftable[(int)Recipes.SolarPanel] = true;
        }
        else
        {
            text += DISABLED_COLOR;
            recipesCraftable[(int)Recipes.SolarPanel] = false;
        }
        text += "Berries -> Solar Panel" + COLOR_END;

        screenText.text = text;

        if(!craftSwitch && Mathf.Approximately(craftButtonLinearDrive.linearMapping.value, 1f))
        {
            // TODO: change to craft selected recipe ONLY IF enough material present
            if(recipesCraftable[selectedRecipeIndex])
            {
                // The correct way to do this would be to make a Recipe class that has a dictionary of material types and quantities
                // as well as an output product.
                // I'm not doing it the correct way.
                switch((Recipes)(selectedRecipeIndex))
                {
                    case Recipes.AppleSeed:
                        {
                            CraftingMaterial mat = materials.Find(m => m.type == CraftingMaterial.MaterialType.Apple);
                            removeMaterial(mat);
                            Destroy(mat.gameObject);
                        }
                        break;
                    case Recipes.Fertilizer:
                        {
                            CraftingMaterial mat = materials.Find(m => m.type == CraftingMaterial.MaterialType.Wood);
                            removeMaterial(mat);
                            Destroy(mat.gameObject);
                        }
                        break;
                    case Recipes.Biodiesel:
                        {
                            CraftingMaterial mat = materials.Find(m => m.type == CraftingMaterial.MaterialType.Corn);
                            removeMaterial(mat);
                            Destroy(mat.gameObject);
                        }
                        break;
                    case Recipes.SolarPanel:
                        {
                            CraftingMaterial mat = materials.Find(m => m.type == CraftingMaterial.MaterialType.Berry);
                            removeMaterial(mat);
                            Destroy(mat.gameObject);
                        }
                        break;
                    default:
                        break;
                }

                GameObject instance = Instantiate(craftingProductPrefabs[selectedRecipeIndex]);
                instance.transform.position = productOutput.position;
                craftSwitch = true;
            }
        }
        else if(craftButtonLinearDrive.linearMapping.value < 0.5f)
        {
            craftSwitch = false;
        }
    }

    public void addMaterial(CraftingMaterial m)
    {
        materialTypes[m.type]++;
        materials.Add(m);
    }

    public void removeMaterial(CraftingMaterial m)
    {
        if (materialTypes[m.type] > 0)
        {
            materialTypes[m.type]--;
            materials.Remove(m);
        }
    }

    public void moveOutlineUp()
    {
        selectedRecipeIndex = (selectedRecipeIndex - 1) % screenOutlinePositions.Length;

        while (selectedRecipeIndex < 0)
        {
            selectedRecipeIndex += screenOutlinePositions.Length;
        }

        Vector3 pos = screenOutline.rectTransform.localPosition;
        pos.y = screenOutlinePositions[selectedRecipeIndex];
        screenOutline.rectTransform.localPosition = pos;
    }

    public void moveOutlineDown()
    {
        selectedRecipeIndex = (selectedRecipeIndex + 1) % screenOutlinePositions.Length;

        Vector3 pos = screenOutline.rectTransform.localPosition;
        pos.y = screenOutlinePositions[selectedRecipeIndex];
        screenOutline.rectTransform.localPosition = pos;
    }

    public void onDetachCraftButtonFromHand()
    {
        craftButtonLinearDrive.linearMapping.value = 0;
        craftButtonLinearDrive.transform.position = craftButtonLinearDrive.startPosition.position;
    }
}