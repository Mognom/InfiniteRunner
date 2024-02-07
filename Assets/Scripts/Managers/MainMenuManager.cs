using System;
using TMPro;
using UnityEngine;

// This class handles the score display on the MainMenu
public class MainMenuManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    private void Start() {
        int newScore = GameStateManager.I.GetCurrentScore();
        string newTime = GameStateManager.I.GetCurrentTime();
        int bestScore = GameStateManager.I.GetBestScore();
        string bestTime = GameStateManager.I.GetBestTime();

        if (newScore == -1) {
            currentScoreText.text = "";
        } else {
            currentScoreText.text = String.Format(currentScoreText.text, newScore, newTime);
        }
        if (bestScore <= 0) {
            bestScoreText.text = "";
        } else {
            bestScoreText.text = String.Format(bestScoreText.text, bestScore, bestTime);
        }
    }
}
