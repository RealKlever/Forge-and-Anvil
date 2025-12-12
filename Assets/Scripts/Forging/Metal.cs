using System.Collections;
using UnityEngine;

public class Metal : MonoBehaviour
{
    public float heatingTemp;
    public float temperature;
    public int hitsToMend;
    public bool isMended;
    public bool isHeated;

    public AudioSource hammerHit;

    // Coroutine to heat the metal over time
    public IEnumerator HeatMetal(float heatingRate)
    {
        while(!isHeated)
        {
            yield return new WaitForSeconds(1f);

            temperature += heatingRate;
            Debug.Log("Metal temperature: " + temperature);

            if (temperature >= heatingTemp)
            {
                isHeated = true;
                Debug.Log("Metal is heated to mending temperature!");
            }
        }
    }

    public void HitMetal()
    {
        if (!isMended && isHeated)
        {
            hitsToMend--;
            hammerHit.Play();

            Debug.Log("Metal hit! Hits remaining to mend: " + hitsToMend);
            if (hitsToMend <= 0)
            {
                isMended = true;
            }
        }
    }
}
