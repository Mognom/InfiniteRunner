using MognomUtils;
using UnityEngine;

public class DifficultyManager : Singleton<DifficultyManager> {
    [SerializeField] private int difficultyIncreaseStep;
    [SerializeField] private float speedIncreasePerStep;
    [SerializeField] private ScoreManager scoreManager;

    private void Update() {
        int difficulty = CalculateCurrentDifficulty();
        Time.timeScale = 1 + (speedIncreasePerStep * difficulty);
    }
    private int CalculateCurrentDifficulty() {
        int currentSeconds = (int)scoreManager.GetCurrentTime();
        return currentSeconds / difficultyIncreaseStep;
    }
}
