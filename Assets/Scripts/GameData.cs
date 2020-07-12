using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public float playerAlcoholLevel = 0.0f;
    public bool gameHasStarted = false;
    public bool gameOver = false;

    private GameObject gameOverUI;
    private GameObject camera;

    private void Start() {
        gameOverUI = GameObject.Find("GameOverUI");
        camera = GameObject.Find("Main Camera");
        gameOverUI.active = false;
    }

    private void Update() {
        if (gameOver) {
            gameOverUI.active = true;
            //camera.transform.position = Vector3.zero;
        }
    }

    public void Restart() {
        Debug.Log("Restarted");
        playerAlcoholLevel = 0.0f;
        gameHasStarted = false;
        gameOver = false;
        gameOverUI.active = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
