using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HitMechanics : MonoBehaviour
{
    public GameObject hammer;

    private float startPosY;
    private bool triggerPressed;

    public InputActionReference triggerActionReference;

    // Register when trigger is pressed
    private void Start()
    {
        triggerPressed = false;
        triggerActionReference.action.performed += TriggerPressed;
    }

    private void OnDestroy()
    {
        triggerActionReference.action.performed -= TriggerPressed;
    }


    // If trigger is pressed, record the starting Y position of the hammer
    void Update()
    {
        if (!triggerPressed)
            startPosY = hammer.transform.position.y;
    }

    // Set triggerPressed to true when trigger is pressed
    void TriggerPressed(InputAction.CallbackContext context)
    {
        triggerPressed = true;
    }

    // When hammer collides with metal, check if trigger was pressed and if hammer was swung down sufficiently
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Metal") && triggerPressed)
        {
            float finalPosY = hammer.transform.position.y;
            if (startPosY - finalPosY > 1.5f)
            {
                Debug.Log("Hit detected on metal with sufficient force!");
                triggerPressed = false;
            }
        }
    }
}
