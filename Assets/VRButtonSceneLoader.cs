using UnityEngine;
using UnityEngine.SceneManagement;

public class VRButtonSceneLoader : MonoBehaviour
{
    [Tooltip("The input action name for the VR button press (e.g., 'XRI RightHand PrimaryButton')")]
    public string vrButtonInput = "XRI RightHand PrimaryButton"; // Default input for the right-hand button "A"

    [Tooltip("The name of the scene to load when the button is pressed.")]
    public string sceneToLoad;

    void Update()
    {
        // Check if the specified button is pressed
        if (Input.GetButtonDown(vrButtonInput))
        {
            LoadScene();
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
            Debug.LogError("Scene name is not specified in the script.");
        }
    }
}
