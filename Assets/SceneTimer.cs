using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class SceneTimer : MonoBehaviour
{
    public float timer = 10f; // Timer duration
    public TextMeshProUGUI timerText; // UI text to display countdown

    void Start()
    {
        UpdateTimerUI(); // Initialize UI text
    }

    void Update()
    {
        timer -= Time.deltaTime;
        UpdateTimerUI();

        if (timer <= 0)
        {
            GoToNextScene();
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = "Time Left: " + Mathf.Ceil(timer).ToString();
        }
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
