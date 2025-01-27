using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class VRButtonSceneLoader : MonoBehaviour
{
    [Tooltip("The name of the scene to load when the button is pressed.")]
    public string sceneToLoad;

    // Called when the button is selected (via controller or interaction)
    public void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad); // Load the specified scene
        }
        else
        {
            Debug.LogError("Scene name not specified in the VRButtonSceneLoader script.");
        }
    }
}
