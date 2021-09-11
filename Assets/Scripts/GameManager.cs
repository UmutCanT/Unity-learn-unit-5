using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> targets;

    [SerializeField]
    TextMeshProUGUI scoreText;
    [SerializeField]
    TextMeshProUGUI livesText;
    [SerializeField]
    TextMeshProUGUI gameOverText;
    [SerializeField]
    Button restartButton;
    [SerializeField]
    GameObject titleScreen;
    [SerializeField]
    GameObject pausePanel;
    [SerializeField]
    GameObject swiper;

    int score;
    int playerLives;
    float spawnRate = 2f;
    bool isGameOver;
    bool isGamePaused;

    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }
    }

    public bool IsGamePaused
    {
        get
        {
            return isGamePaused;
        }
    }

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        swiper.gameObject.SetActive(true);
        playerLives = 3;        
        isGameOver = false;
        isGamePaused = false;
        score = 0;
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateLives(0);
        UpdateScore(0);
    }

    void Update()
    {
        PauseController();
    }

    void PauseController()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isGamePaused)
            {
                Time.timeScale = 0f;
                pausePanel.SetActive(true);
                isGamePaused = true;
            }
            else
            {
                Time.timeScale = 1f;
                pausePanel.SetActive(false);
                isGamePaused = false;
            }
           
        }
    }

    IEnumerator SpawnTarget()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate);

            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateLives(int damage)
    {
        if (!isGameOver)
        {
            playerLives -= damage;
            livesText.text = "Lives: " + playerLives;
        }      
        if(playerLives <= 0)
        {
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        swiper.gameObject.SetActive(false);
        isGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
