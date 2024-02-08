using MognomUtils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : PersistentSingleton<GameStateManager> {

    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string gameSceneName;

    [SerializeField] private GameState currentState;

    [SerializeField] private IntStringEventChannel setScoreChannel;

    private int currentScore;
    private String currentTime;
    private int bestScore;
    private String bestTime;

    private enum GameState {
        MAINMENU,
        GAME
    }

    protected override void Awake() {
        base.Awake();
        if (GameStateManager.I == this) {
            bestScore = 0;
            bestTime = "0";
            currentScore = -1;
            if (currentState == GameState.GAME) {
                setScoreChannel.Channel += OnSetScore;
            }
        }
    }

    private void OnSetScore(int score, string formattedTime) {
        currentScore = score;
        currentTime = formattedTime;

        if (currentScore > bestScore) {
            bestScore = currentScore;
            bestTime = currentTime;
        }

        currentState = GameState.MAINMENU;
        setScoreChannel.Channel -= OnSetScore;
        StartCoroutine(SwapScene(mainMenuSceneName));

    }

    private IEnumerator SwapScene(string newScene) {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(newScene);
    }

    public void GoToNextScene() {
        switch (currentState) {
            case GameState.MAINMENU:
                setScoreChannel.Channel += OnSetScore;
                SceneManager.LoadScene(gameSceneName);
                currentState = GameState.GAME;
                break;
            case GameState.GAME:
                SceneManager.LoadScene(mainMenuSceneName);
                setScoreChannel.Channel -= OnSetScore;
                currentState = GameState.MAINMENU;
                break;
        }
    }

    public void GoToMainMenu() {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public int GetBestScore() {
        return bestScore;
    }

    public int GetCurrentScore() {
        return currentScore;
    }

    public String GetBestTime() {
        return bestTime;
    }

    public String GetCurrentTime() {
        return currentTime;
    }
}
