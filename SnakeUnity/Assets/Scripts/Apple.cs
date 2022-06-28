using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public static int resetApple;
    public static int scoreResetApple;
    private float randomX;
    private float randomY;
    [SerializeField] private TextMeshProUGUI resetAppleText;

    private void Start()
    {
        RandomizePosition();
        resetApple = 3;
        scoreResetApple = 0;
    }

    private void Update()
    {
        if (scoreResetApple >= 10)
        {
            scoreResetApple -= 10;
            resetApple++;
        }
        if (resetApple >= 1)
        {
            if (Snake.gameOver == false)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RandomizePosition();
                    resetApple--;
                }
            }
        }
        resetAppleText.text = $"{resetApple}";
    }

    private void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        randomX = Random.Range(bounds.min.x, bounds.max.x);
        randomY = Random.Range(bounds.min.y, bounds.max.y);
        while (randomX == 0 && randomY == 0)
        {
            randomX = Random.Range(bounds.min.x, bounds.max.x);
            randomY = Random.Range(bounds.min.y, bounds.max.y);
        }
        this.transform.position = new Vector3(Mathf.Round(randomX), Mathf.Round(randomY), 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Snake Head")
            RandomizePosition();
    }
}