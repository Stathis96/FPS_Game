using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
	public static GameOverMenu instance ;
    public GameObject gameOverMenuUI;
	public GameObject crosshair;

	public Text scoreText2;
	int score = 0;

	void Awake()
	{
		instance = this ;
	}

    public void Restart()
    {
        gameOverMenuUI.SetActive(false);
		crosshair.SetActive(true);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false ;
        Time.timeScale = 1f;
		SceneManager.LoadScene("GameScene");
    }

    public void LoadMenu()
    {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true ;    
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    } 

	public void AddPoint2()
	{
		score += 1;
		scoreText2.text = "YOU SCORED:"+ score.ToString() ;
	}
}
