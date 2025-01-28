using UnityEngine;

public class Bowl : MonoBehaviour
{
    public int bowlIndex; // Assigned in the Inspector
    private int marbleCount = 0; // Tracks the number of marbles in this bowl

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble"))
        {
            marbleCount++;
            GameManager.Instance.UpdatePlayerMarbleCount(bowlIndex, marbleCount);
            Destroy(other.gameObject); // Optionally destroy the marble
        }
    }
}
