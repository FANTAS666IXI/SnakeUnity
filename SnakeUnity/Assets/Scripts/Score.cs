using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI maxScoreText;
    private float dificulty;

    private void Update()
    {
        scoreText.text = $"{Snake.score}";
        maxScoreText.text = $"{Snake.maxScore}";
        dificulty = Mathf.Round(Snake.score / 10);
        dificulty /= 100;
        Time.fixedDeltaTime = 0.1f - dificulty;
    }
}