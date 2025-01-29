using UnityEngine;
using UnityEngine.UI; // For Button and UI interactions
using TMPro;          // For TextMeshPro
using UnityEngine.SceneManagement; // For scene loading

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance { get; private set; }

    
    public TextMeshProUGUI timerText; 
    public float levelTime = 30f;     
    private float timer;
    private bool isGameActive = false;

    // Bowl and marble variables
    public GameObject[] bowls;        // Array of bowl objects
    public GameObject marblePrefab;   
    public Transform marbleSpawnArea; 
    public int[] correctMarbleCounts; // Target marble counts for each bowl
    private int[] playerMarbleCounts; // Player's marble counts for each bowl

    // Success UI variables
    public GameObject successPanel;      
    public TextMeshProUGUI successText;  
    public Button nextLevelButton;       

    // Game Over UI variables
    public GameObject gameOverPanel;     
    public Button retryButton;           
    public Button menuButton;            

    void Awake()
    {
        // Singleton implementation
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate GameManager
            return;
        }
        Instance = this;
    }

    void Start()
    {
        // Initialize variables
        playerMarbleCounts = new int[bowls.Length];
        timer = levelTime;

        // Ensure success and game over panels are hidden initially
        if (successPanel != null)
            successPanel.SetActive(false);
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        // Attach button functionality
        if (nextLevelButton != null)
            nextLevelButton.onClick.AddListener(GoToNextLevel);

        if (retryButton != null)
            retryButton.onClick.AddListener(RetryLevel);

        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMainMenu);

        
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
        // Spawn marbles randomly in the spawn area
        for (int i = 0; i < 6; i++) // Adjust the number of marbles as needed
        {
            Vector3 randomPosition = marbleSpawnArea.position + new Vector3(
                Random.Range(0, 0), 0, Random.Range(0, 0));
            Instantiate(marblePrefab, randomPosition, Quaternion.identity);
        }
    }

    public void UpdatePlayerMarbleCount(int bowlIndex, int count)
    {
        // Update player's marble count for a specific bowl
        playerMarbleCounts[bowlIndex] = count;

        // Check if the player's inputs are correct
        CheckPlayerInput();
    }

    public void CheckPlayerInput()
    {
        // Check if all bowls have the correct number of marbles
        for (int i = 0; i < bowls.Length; i++)
        {
            if (playerMarbleCounts[i] != correctMarbleCounts[i])
            {
                return; // If any bowl is incorrect, don't end the game
            }
        }

        // If all bowls are correct, the player wins
        EndGame(true);
    }

    public void EndGame(bool didWin)
    {
        isGameActive = false;

        if (didWin)
        {
            
            if (successPanel != null)
            {
                successPanel.SetActive(true); // Enable the success UI
            }

            // Set the success message
            if (successText != null)
            {
                successText.text = "Nice memorization skills!";
            }
        }
        else
        {
            // Show the game over panel
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true); // Enable the game over UI
            }
        }
    }

    public void RetryLevel()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        //(adjust the scene name as needed)
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToNextLevel()
    {
        // Load the next scene (adjust the build index or scene name as needed)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
