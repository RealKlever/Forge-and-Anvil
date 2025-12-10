using UnityEngine;

public class FurnaceHeat : MonoBehaviour
{
    public float temperature = 1300.0f;

    private float heatingRate;

    private void Star1t()
    {
        heatingRate = temperature;
    }

    // When metal enters the furnace, start heating it
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal"))
        {
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
            Metal metal = other.GetComponent<Metal>();
            if (metal != null)
            {
                StopCoroutine(metal.HeatMetal(heatingRate));
                Debug.Log("Metal exited furnace.");
            }
        }
    }
}
