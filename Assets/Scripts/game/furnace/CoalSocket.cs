using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

namespace game.furnace
{
    public class CoalSocket : MonoBehaviour
    {
        public XRSocketInteractor socket;

        public Furnace furnace;
        
        
        private void OnEnable()
        {
            socket.selectEntered.AddListener(OnObjectPlaced);
        }

        private void OnDisable()
        {
            socket.selectEntered.RemoveListener(OnObjectPlaced);
        }

        private void OnObjectPlaced(SelectEnterEventArgs args)
        {
            // Check if the placed object is the correct one
            // if (args.interactableObject.transform == correctObject.transform)
            if (args.interactableObject.transform.CompareTag("coal"))
            {
                // Lock the object so it cannot be removed
                args.interactableObject.transform.GetComponent<XRGrabInteractable>().enabled = false;
                args.interactableObject.transform.GetComponent<Rigidbody>().useGravity = false;
                args.interactableObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                // socket.allowSelect = false;
                socket.socketActive = false;
                furnace.addCoal(30, args.interactableObject.transform.gameObject, socket);  //TODO: use 300 secs
                furnace.furnaceLight.SetActive(true);
                furnace.furnaceHeat.GetComponent<BoxCollider>().enabled = true;
                furnace.furnaceHeat.GetComponent<FurnaceHeat>().ActivateFurnace();

                Debug.Log("Coal placed. It is now locked.");
            }
            else
            {
                Debug.Log("Wrong object inserted.");
            }
        }
    }
}