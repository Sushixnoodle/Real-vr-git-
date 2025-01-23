using UnityEngine;

public class Bowl : MonoBehaviour
{
    public int bowlIndex; // Index of this bowl
    private int marbleCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Marble"))
        {
            marbleCount++;
            Destroy(other.gameObject); // Remove the marble
        }
    }

    public int GetMarbleCount()
    {
        return marbleCount;
    }
}
