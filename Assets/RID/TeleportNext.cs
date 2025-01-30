using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.XR.CoreUtils; // Required for XR Origin

public class TeleportToNextScene : MonoBehaviour
{
    [SerializeField] private XROrigin playerXR; // Assign XR Rig in Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerXR.gameObject) // Check if the colliding object is the player
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("No next scene in the build settings.");
            }
        }
    }
}
