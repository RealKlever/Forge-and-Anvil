using UnityEngine;

public class FurnaceHeat : MonoBehaviour
{
    public float temperature = 1300.0f;

    private float heatingRate;
    private Coroutine tempRoutine;

    private void Start()
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
                tempRoutine = StartCoroutine(metal.HeatMetal(heatingRate));
                if (metal.isHeated)
                {
                    StopCoroutine(tempRoutine);
                    tempRoutine = null;
                }

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
                StopCoroutine(tempRoutine);
                tempRoutine = null;
                Debug.Log("Metal exited furnace.");
            }
        }
    }
}
