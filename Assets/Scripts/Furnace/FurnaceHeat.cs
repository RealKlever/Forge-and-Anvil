using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class FurnaceHeat : MonoBehaviour
{
    public float temperature = 1300.0f;

    public float heatingRate;
    public bool furnaceActive = false;

    private Metal metal;

    private void Start()
    {
        heatingRate = temperature/25.0f;
        metal = new Metal();
    }

    private void Update()
    {
        if (metal.isHeated || !furnaceActive)
           StopAllCoroutines();
    }

    // When metal enters the furnace, start heating it
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal"))
        {
            // Tie-in to the visual script (separate file)
            MetalTemperatureColor visual = other.GetComponent<MetalTemperatureColor>();
            if (visual != null)
            {
                visual.NotifyEnteredFurnace(this);
            }

            metal = other.GetComponent<Metal>();
            if (metal != null)
            {
                if(!metal.isHeated && furnaceActive)
                    StartCoroutine(metal.HeatMetal(heatingRate));

                Debug.Log("Metal entered furnace and is being heated.");
            }
        }
    }

    // When metal exits the furnace, stop heating it
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Metal"))
        {
            // Tie-in to the visual script (separate file)
            MetalTemperatureColor visual = other.GetComponent<MetalTemperatureColor>();
            if (visual != null)
            {
                visual.NotifyExitedFurnace(this);
            }

            if (metal != null)
            {
                StopAllCoroutines();
                Debug.Log("Metal exited furnace.");
            }
        }
    }

    public void ActivateFurnace()
    {
        furnaceActive = true;
    }

    public void DeactivateFurnace()
    {
        furnaceActive = false;
    }
}
