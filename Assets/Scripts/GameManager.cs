using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scoreToWin;
    public int currentScore;

    public bool gamePaused;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        gamePaused = !gamePaused;
        Time.timeScale = gamePaused ? 0f : 1f;

        Cursor.lockState = gamePaused ? CursorLockMode.None : CursorLockMode.Locked;

        GameUI.instance.TogglePauseMenu(gamePaused);
    }

    public void AddScore(int score)
    {
        currentScore += score;

        GameUI.instance.UpdateScoreText(currentScore);

        if (currentScore >= scoreToWin)
            WinGame();
    }

    private void WinGame()
    {
        GameUI.instance.SetEndgameScreen(true, currentScore);

        Time.timeScale = 0;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoseGame()
    {
        GameUI.instance.SetEndgameScreen(false, currentScore);

        Time.timeScale = 0;
        gamePaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
