using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Bowl : MonoBehaviour
{
    public int bowlIndex; // Assigned in the Inspector
    public Material requiredMaterial; // Assign the correct material (Red, Blue, Green) in Inspector

    private int marbleCount = 0; // Tracks the number of marbles in this bowl

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble")) // Ensure the object is a marble
        {
            Renderer marbleRenderer = other.GetComponent<Renderer>();

            // Ensure the marble has a renderer and material
            if (marbleRenderer != null && requiredMaterial != null)
            {
                // ✅ Compare the material's color instead of the material itself
                if (marbleRenderer.material.color == requiredMaterial.color)
                {
                    marbleCount++;
                    Debug.Log($"✅ Bowl {bowlIndex}: Correct Marble Added. Total = {marbleCount}");

                    // Notify GameManager that the correct marble was added
                    GameManager.Instance.UpdatePlayerMarbleCount(bowlIndex, marbleCount);

                    // Prevent the marble from moving after being placed
                    Rigidbody marbleRigidbody = other.GetComponent<Rigidbody>();
                    if (marbleRigidbody != null)
                    {
                        marbleRigidbody.isKinematic = true; // Stops movement
                    }

                    // Prevent grabbing again (if using XR)
                    var grabInteractable = other.GetComponent<XRGrabInteractable>();
                    if (grabInteractable != null)
                    {
                        grabInteractable.enabled = false; // Prevents picking it up again
                    }
                }
                else
                {
                    Debug.Log($"❌ Incorrect Marble in Bowl {bowlIndex}! Expected {requiredMaterial.name}, but got {marbleRenderer.material.name}");
                }
            }
        }
    }
}
