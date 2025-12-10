using UnityEngine;

public class FurnaceHeat : MonoBehaviour
{
    public float temperature = 1300.0f;

    public float heatingRate;

    private void Start()
    {
        heatingRate = temperature/40.0f;
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

            Metal metal = other.GetComponent<Metal>();
            if (metal != null)
            {
                if (metal.isHeated)
                {
                    StopCoroutine(metal.HeatMetal(heatingRate));
                }
                else
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

            Metal metal = other.GetComponent<Metal>();
            if (metal != null)
            {
                StopCoroutine(metal.HeatMetal(heatingRate));
                Debug.Log("Metal exited furnace.");
            }
        }
    }
}
