using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

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

    public Vector3 spawnLocation = new Vector3(1.1923f, 1.1690f, 2.9652f); // Set default spawn position
    public float spawnRange = 0.1f; // Small random offset so marbles don’t stack perfectly

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
            // ✅ Generate a small random offset so marbles don’t stack exactly on each other
            Vector3 randomOffset = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                Random.Range(-spawnRange / 2, spawnRange / 2), // Smaller Y offset to prevent floating
                Random.Range(-spawnRange, spawnRange)
            );

            Vector3 spawnPosition = spawnLocation + randomOffset;

            GameObject marble = Instantiate(marblePrefab, spawnPosition, Quaternion.identity);

            // ✅ Assign correct color to the marble
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
        // ✅ Prevent checking for success if the game is already over
        if (!isGameActive)
        {
            Debug.Log("🛑 Game is over. Ignoring player input.");
            return;
        }

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

        // ✅ Disable all marbles so they can't be moved
        DisableAllMarbles();

        if (didWin)
        {
            Debug.Log("🎉 SUCCESS: Player won! Showing success panel.");

            if (successPanel != null)
            {
                successPanel.SetActive(true);
                Debug.Log("✅ SUCCESS PANEL ACTIVATED!");
            }
        }
        else
        {
            Debug.Log("❌ GAME OVER: Timer ran out. Showing game over panel...");

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true);
                Debug.Log("✅ GAME OVER PANEL ACTIVATED!");
            }
        }
    }

    // ✅ Disable all marbles in the scene so they can't be moved after game ends
    void DisableAllMarbles()
    {
        GameObject[] marbles = GameObject.FindGameObjectsWithTag("Marble");

        foreach (GameObject marble in marbles)
        {
            Rigidbody rb = marble.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Stop movement
            }

            var grabInteractable = marble.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false; // Disable grabbing
            }
        }

        Debug.Log("🛑 All marbles are now disabled!");
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
