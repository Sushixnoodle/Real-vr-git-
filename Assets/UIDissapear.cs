using UnityEngine;

public class UIDisappear : MonoBehaviour
{
    public GameObject uiElement; 
    public float disappearTime = 5f; 

    void Start()
    {
        if (uiElement != null)
        {
            Invoke("HideUI", disappearTime);
        }
    }

    void HideUI()
    {
        uiElement.SetActive(false);
    }
}