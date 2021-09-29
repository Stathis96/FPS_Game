using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
	public GameObject crosshair;

    public static bool GameIsPaused = false;

    void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {  
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
		crosshair.SetActive(true);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false ;
        Time.timeScale = 1f; //ξεπαγωμα χρονου!
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
		crosshair.SetActive(false);
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true ;
        Time.timeScale = 0f; //παγωμα χρονου!
        GameIsPaused = true;
    }

    public void LoadMenu()
    {    
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true ;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }   

    
    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
