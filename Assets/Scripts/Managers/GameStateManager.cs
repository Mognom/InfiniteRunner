using MognomUtils;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : PersistentSingleton<GameStateManager> {

    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string gameSceneName;

    [SerializeField] private GameState currentState;

    public int CurrentWave { get; private set; }
    private int currentScore;
    private int bestScore;

    private enum GameState {
        MAINMENU,
        GAME
    }

    protected override void Awake() {
        base.Awake();
        if (GameStateManager.I == this) {
            CurrentWave = 1;
            bestScore = 0;
            currentScore = -1;
        }
    }

    private IEnumerator SwapScene(string newScene) {
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(newScene);
    }

    public void GoToNextScene() {
        switch (currentState) {
            case GameState.MAINMENU:
                CurrentWave = 1;
                currentScore = 0;
                this.SwapScene(gameSceneName);
                currentState = GameState.GAME;
                break;
            case GameState.GAME:
                this.SwapScene(mainMenuSceneName);
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
}
