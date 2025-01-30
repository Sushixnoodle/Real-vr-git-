using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class VRButtonSceneLoader : MonoBehaviour
{
    [Tooltip("The name of the scene to load when the button is pressed.")]
    public string sceneToLoad;

    private InputDevice targetDevice;

    void Start()
    {
        // Try to get the right-hand controller (Oculus, OpenXR, etc.)
        InitializeXRDevice();
    }

    void Update()
    {
        if (targetDevice.isValid)
        {
            bool buttonPressed = false;

            // Check if the "A" button is pressed (CommonUsages.primaryButton usually corresponds to the "A" button)
            if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed) && buttonPressed)
            {
                LoadScene();
            }
        }
        else
        {
            // If device is not valid, try to reinitialize it (useful if a controller gets reconnected)
            InitializeXRDevice();
        }
    }

    void InitializeXRDevice()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name not specified in the VRButtonSceneLoader script.");
        }
    }
}
