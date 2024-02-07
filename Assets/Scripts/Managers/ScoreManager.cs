using System;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

    [SerializeField] private VoidEventChannel playerDeadChannel;
    [SerializeField] private IntStringEventChannel setScoreChannel;
    [SerializeField] private IntEventChannel scoreCollectedChannel;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    private int currentScore;
    private float currentTime;


    private bool gameOver;
    private void Awake() {
        playerDeadChannel.Channel += OnPlayerDead;
        scoreCollectedChannel.Channel += OnScoreIncrease;
        gameOver = false;
        UpdateTimeText();
        scoreText.text = "0";
    }

    private void Update() {
        if (!gameOver) {
            currentTime += Time.deltaTime;
            UpdateTimeText();
        }
    }

    private String FormatTime(float seconds) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        string formatString = (timeSpan.TotalHours >= 1 ? @"hh\:" : "")
                      + (timeSpan.TotalMinutes >= 1 ? @"mm\:" : "")
                      + "ss";
        return timeSpan.ToString(formatString);
    }
    private void UpdateTimeText() {
        String formattedTime = FormatTime(currentTime);
        timeText.text = formattedTime;
    }

    private void OnScoreIncrease(int score) {
        if (!gameOver) {
            currentScore += score;
            scoreText.text = currentScore.ToString();
        }
    }

    private void OnPlayerDead() {
        gameOver = true;
        setScoreChannel.PostEvent(currentScore, FormatTime(currentTime));
    }
}
