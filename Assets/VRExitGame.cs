using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Required for VR controller input

public class VRExitButton : MonoBehaviour
{
    public Button exitUIButton; // Assign the UI button in the Inspector
    public InputActionProperty vrButton; // Assign the A button in the Inspector

    private void Update()
    {
        // Check if the A button is pressed
        if (vrButton.action.WasPressedThisFrame())
        {
            Debug.Log("🎮 A Button Pressed! Triggering Exit UI Button.");

            // Simulate UI button click
            if (exitUIButton != null)
            {
                exitUIButton.onClick.Invoke();
            }
            else
            {
                Debug.LogError("❌ ERROR: No UI Button assigned to VRExitButton script!");
            }
        }
    }
}
