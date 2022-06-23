using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.zero;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    //public int initialSize = 0;
    public static int score;
    public static int maxScore;
    public static bool gameOver;
    public GameObject gameOverUI;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            direction = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            direction = Vector2.down;
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            direction = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            direction = Vector2.left;
    }

    private void FixedUpdate()
    {
        for (int i = segments.Count - 1; i > 0; i--)
            segments[i].position = segments[i - 1].position;

        this.transform.position = new Vector3(Mathf.Round(this.transform.position.x) + direction.x, Mathf.Round(this.transform.position.y) + direction.y, 0f);
    }

    private void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
        score ++;
        Apple.scoreResetApple++;
        if (score >= maxScore)
            maxScore = score;
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetState()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        segments.Clear();
        segments.Add(this.transform);
        //InitialSize();
        direction = Vector2.zero;
        this.transform.position = Vector3.zero;
        score = 0;
        Apple.scoreResetApple = 0;
        Apple.resetApple = 3;
    }

    //private void InitialSize()
    //{
    //    for (int i = 1; i < this.initialSize; i++) { }
    //        segments.Add(Instantiate(this.segmentPrefab));

    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple")
            Grow();
        else if (other.tag == "Wall")
            GameOver();
        else if (other.tag == "Snake Tail")
            GameOver();
    }
}