using UnityEngine;

public class Metal : MonoBehaviour
{
    public float heatingTemp;
    public float temperature;
    public int hitsToMend;
    public bool isMended;
    public bool isHeated;


    public void HeatMetal(float heatingRate)
    {
        if(!isHeated)
        {
            temperature += heatingRate * Time.deltaTime;
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
        if(!isMended && isHeated)
        {
            hitsToMend--;
            Debug.Log("Metal hit! Hits remaining to mend: " + hitsToMend);
            if (hitsToMend <= 0)
            {
                MendMetal();
            }
        }
    }

    public void MendMetal()
    {
        isMended = true;
        Debug.Log("Metal has been mended!");
    }
}
