using UnityEngine;

public class FurnaceHeat : MonoBehaviour
{
    public float temperature = 1300.0f;

    private float heatingRate;

    private void Start()
    {
        heatingRate = temperature / 1000.0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Metal"))
        {
            Metal metal = other.GetComponent<Metal>();
            if (metal != null)
            {
                metal.HeatMetal(heatingRate);
                Debug.Log("Metal entered furnace and is being heated.");
            }
        }
    }
}
