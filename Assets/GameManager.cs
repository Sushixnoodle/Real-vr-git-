using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Timer variables
    public TextMeshProUGUI timerText;
    public float levelTime = 30f;
    private float timer;
    private bool isGameActive = false;

    // Bowl and marble tracking
    public GameObject[] bowls;
    public GameObject marblePrefab;
    public Transform marbleSpawnArea;
    public int[] correctMarbleCounts;
    private int[] playerMarbleCounts;

    // Marble spawning controls
    public int redMarbleCount = 5;
    public int blueMarbleCount = 5;
    public int greenMarbleCount = 5;
    public Material redMaterial;
    public Material blueMaterial;
    public Material greenMaterial;

    // Success UI
    public GameObject successPanel;
    public TextMeshProUGUI successText;
    public Button nextLevelButton;

    // Game Over UI
    public GameObject gameOverPanel;
    public Button retryButton;
    public Button menuButton;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        playerMarbleCounts = new int[bowls.Length];
        timer = levelTime;

        if (successPanel != null) successPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (nextLevelButton != null) nextLevelButton.onClick.AddListener(GoToNextLevel);
        if (retryButton != null) retryButton.onClick.AddListener(RetryLevel);
        if (menuButton != null) menuButton.onClick.AddListener(GoToMainMenu);

        StartGame();
    }

    void Update()
    {
        if (isGameActive)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();

            if (timer <= 0)
            {
                EndGame(false);
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Time Left: {Mathf.Ceil(timer)}";
    }

    public void StartGame()
    {
        isGameActive = true;
        SpawnMarbles();
    }

    public void SpawnMarbles()
    {
        SpawnMarbleGroup(redMarbleCount, redMaterial);
        SpawnMarbleGroup(blueMarbleCount, blueMaterial);
        SpawnMarbleGroup(greenMarbleCount, greenMaterial);
    }

    private void SpawnMarbleGroup(int count, Material marbleMaterial)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = marbleSpawnArea.position + new Vector3(
                Random.Range(-1, 1), 0, Random.Range(-1, 1));

            GameObject marble = Instantiate(marblePrefab, spawnPosition, Quaternion.identity);

            Renderer marbleRenderer = marble.GetComponent<Renderer>();
            if (marbleRenderer != null)
            {
                marbleRenderer.material = marbleMaterial;
            }
        }
    }

    public void UpdatePlayerMarbleCount(int bowlIndex, int count)
    {
        Debug.Log($"⚠️ Updating Bowl {bowlIndex}: New Count = {count}");

        playerMarbleCounts[bowlIndex] = count;
        CheckPlayerInput();
    }


    public void CheckPlayerInput()
    {
        Debug.Log("🔍 Checking Player Input...");

        for (int i = 0; i < bowls.Length; i++)
        {
            Debug.Log($"⚠️ Bowl {i}: Player Count = {playerMarbleCounts[i]}, Required Count = {correctMarbleCounts[i]}");

            if (playerMarbleCounts[i] != correctMarbleCounts[i])
            {
                Debug.Log($"❌ Bowl {i} is incorrect. Game continues.");
                return;
            }
        }

        Debug.Log("🎉 ALL BOWLS CORRECT! TRIGGERING SUCCESS!");
        EndGame(true);
    }

    public void EndGame(bool didWin)
    {
        isGameActive = false;

        if (didWin)
        {
            if (successPanel != null) successPanel.SetActive(true);
            if (successText != null) successText.text = "Nice memorization skills!";
        }
        else
        {
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
        }
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
