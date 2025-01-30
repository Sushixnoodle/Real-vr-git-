using UnityEngine;
using System.Collections.Generic; // Needed for tracking marbles

public class Bowl : MonoBehaviour
{
    public int bowlIndex;
    public Material requiredMaterial; // Assign Red, Blue, or Green in the Inspector

    private HashSet<GameObject> countedMarbles = new HashSet<GameObject>(); // Prevents duplicate counting

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble"))
        {
            Renderer marbleRenderer = other.GetComponent<Renderer>();

            if (marbleRenderer != null && requiredMaterial != null)
            {
                // ✅ Compare color instead of material instance
                if (marbleRenderer.material.color == requiredMaterial.color)
                {
                    // ✅ Check if marble has already been counted
                    if (!countedMarbles.Contains(other.gameObject))
                    {
                        countedMarbles.Add(other.gameObject);
                        Debug.Log($"✅ Bowl {bowlIndex}: Correct Marble Added. Current Count = {countedMarbles.Count}");

                        // ✅ Update GameManager
                        GameManager.Instance.UpdatePlayerMarbleCount(bowlIndex, countedMarbles.Count);
                    }
                }
                else
                {
                    Debug.Log($"❌ Bowl {bowlIndex}: Wrong Color! Expected {requiredMaterial.color}, but got {marbleRenderer.material.color}");
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Marble"))
        {
            if (countedMarbles.Contains(other.gameObject))
            {
                countedMarbles.Remove(other.gameObject);
                Debug.Log($"⚠️ Bowl {bowlIndex}: Marble Removed. Current Count = {countedMarbles.Count}");

                // ✅ Update GameManager to reflect marble removal
                GameManager.Instance.UpdatePlayerMarbleCount(bowlIndex, countedMarbles.Count);
            }
        }
    }
}
