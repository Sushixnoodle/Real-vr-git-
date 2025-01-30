using UnityEngine;
using System.Collections;

public class TeleporterActivator : MonoBehaviour
{
    [SerializeField] private GameObject teleporter;

    private void Start()
    {
        if (teleporter) teleporter.SetActive(false);
    }

    public void ActivateTeleporter()
    {
        StartCoroutine(EnableTeleporterWithDelay());
    }

    IEnumerator EnableTeleporterWithDelay()
    {
        Debug.Log("[TeleporterActivator] Enabling teleporter in 0.5 seconds...");
        yield return new WaitForSeconds(0.5f);
        if (teleporter)
        {
            teleporter.SetActive(true);
            Debug.Log("[TeleporterActivator] Teleporter is now ACTIVE!");
        }
    }
}
