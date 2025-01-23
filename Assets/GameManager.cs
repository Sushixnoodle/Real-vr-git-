using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject[] bowls;
    public GameObject marblePrefab;
    public Transform marbleSpawnArea;
    public int[] correctMarbleCounts; // Number of marbles for each bowl
    private int[] playerMarbleCounts; // Player's input
    private float timer = 90f;
    private bool isGameActive = false;

    void Start()
    {
        playerMarbleCounts = new int[bowls.Length];
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
                EndGame(false); // Time's up
            }
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = $"Time Left: {Mathf.Ceil(timer)}";
    }

    public void StartGame()
    {
        isGameActive = true;
        SpawnMarbles();
    }

    public void SpawnMarbles()
    {
        // Spawn marbles randomly in the marbleSpawnArea
        for (int i = 0; i < 20; i++) // Adjust number as needed
        {
            Vector3 randomPosition = marbleSpawnArea.position + new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
            Instantiate(marblePrefab, randomPosition, Quaternion.identity);
        }
    }

    public void EndGame(bool didWin)
    {
        isGameActive = false;

        if (didWin)
        {
            Debug.Log("You Win!");
        }
        else
        {
            Debug.Log("Time's Up! You Lose!");
        }

        // Show UI for restart/menu
    }

    public void CheckPlayerInput()
    {
        bool isCorrect = true;
        for (int i = 0; i < bowls.Length; i++)
        {
            if (playerMarbleCounts[i] != correctMarbleCounts[i])
            {
                isCorrect = false;
                break;
            }
        }

        EndGame(isCorrect);
    }
}
