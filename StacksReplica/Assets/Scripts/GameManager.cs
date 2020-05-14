using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Spawner spawner;
    
    public Text playText;
    public Text scoreText;
    public Text LoseText;
    public GameObject panel;
    public GameObject restartButton;

    private int score = 0;

    [SerializeField]
    Direction direction;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        panel.SetActive(true);
        restartButton.SetActive(false);
        playText.enabled = true;
        scoreText.enabled = false;
        LoseText.enabled = false;
        UpdateScore();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && spawner != null)
        {
            if (score == 0)
            {
                panel.SetActive(false);
                playText.enabled = false;
                scoreText.enabled = true;
            }

            if (MoveCube.CurrentCube != null && MoveCube.CurrentCube != MoveCube.PreviosCube)
            {
                if ((Mathf.Abs(MoveCube.CurrentCube.transform.position.x - MoveCube.PreviosCube.transform.position.x) > MoveCube.CurrentCube.transform.localScale.x && direction == Direction.Xdirection)
                    || (Mathf.Abs(MoveCube.CurrentCube.transform.position.z - MoveCube.PreviosCube.transform.position.z) > MoveCube.CurrentCube.transform.localScale.z && direction == Direction.Zdirection))
                {
                    Lose();
                }
                else
                {
                    MoveCube.CurrentCube.StopCube();
                    Camera.main.transform.position += transform.up * Time.deltaTime * 5f;
                    UpdateScore();
                }
            }

            direction = direction == Direction.Zdirection ? Direction.Xdirection : Direction.Zdirection;
            if (spawner != null)
                spawner.SpawnCube(direction);
        }
    }

    private void UpdateScore()
    {
        score = MoveCube.Stacks;
        scoreText.text = "SCORE: " + score;
    }

    private void Lose()
    {
        MoveCube.CurrentCube.MSpeed = 0f;
        MoveCube.CurrentCube.gameObject.AddComponent<Rigidbody>();
        spawner = null;

        panel.SetActive(true);
        restartButton.SetActive(true);
        scoreText.enabled = false;
        LoseText.enabled = true;
        LoseText.text = "YOU LOST .. YOUR SCORE IS " + score;
    }

    public void RestartGame()
    {
        MoveCube.ResetStaticVariables();
        SceneManager.LoadScene(0);
    }
}
