using UnityEngine;
using UnityEngine.InputSystem;

public class OpenSettingsUI : MonoBehaviour
{
    [Header("Input")]
    // Reference to the InputAction you created in your Input Actions asset
    public InputActionReference openSettingsAction;

    [Header("UI")]
    // Assign the Canvas or Panel that is your settings page
    public GameObject settingsPage;

    private void OnEnable()
    {
        if (openSettingsAction != null)
        {
            openSettingsAction.action.performed += OnOpenSettings;
            openSettingsAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (openSettingsAction != null)
        {
            openSettingsAction.action.performed -= OnOpenSettings;
            openSettingsAction.action.Disable();
        }
    }

    private bool canToggle = true;
    private float toggleCooldown = 0.1f;

    private void OnOpenSettings(InputAction.CallbackContext context)
    {
        if (context.performed && canToggle)
        {
            canToggle = false;
            settingsPage.SetActive(!settingsPage.activeSelf);
            Invoke(nameof(ResetToggle), toggleCooldown);
        }
    }

    private void ResetToggle()
    {
        canToggle = true;
    }


}

