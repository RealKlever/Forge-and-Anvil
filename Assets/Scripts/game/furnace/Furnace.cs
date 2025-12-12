using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace game.furnace
{
    public class Furnace : MonoBehaviour
    {

        public GameObject furnaceLight;
        private int coalAmount = 0;

        public void addCoal(int secondsBurnTime, GameObject coalObject, XRSocketInteractor socket)
        {
            coalAmount++;
            StartCoroutine(removeCoalAfterDelay(secondsBurnTime, coalObject, socket));
        }
        
        IEnumerator removeCoalAfterDelay(float delayTime, GameObject coalObject, XRSocketInteractor socket)
        {
            // Wait for the specified delay time
            yield return new WaitForSeconds(delayTime);

            Debug.Log("Remove 1 Coal");
            coalAmount--;
            Destroy(coalObject);
            socket.socketActive = true;  // Enable socket again
            furnaceLight.SetActive(false);
        }
        
    }
}