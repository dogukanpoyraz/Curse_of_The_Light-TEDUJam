using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public bool gamePaused = false;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GunController gunController;
    PlayerMovement player = null;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    void Update () {
        if (Input.GetButtonDown("Cancel"))
        {
            if (gamePaused == false)
            {
                player.GamePaused();
                Time.timeScale = 0f;
                gamePaused = true;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                player.GameResumed();
                pauseMenu.SetActive(false);
                Cursor.visible = false;
                gamePaused = false;
                Time.timeScale = 1f;
            }
        }
        	
	}

    public void PauseGame()
    {
        player.GamePaused();
        Time.timeScale = 0f;
        gunController.gamePaused = true;
        gamePaused = true;
        Cursor.visible = true;
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        player.GameResumed();
        pauseMenu.SetActive(false);
        gunController.gamePaused = false;
        Cursor.visible = false;
        gamePaused = false;
        Time.timeScale = 1f;
    }

    public void ReturnMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Cursor.visible = true;
    }

    public void PauseMenuSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void SettingsBackToPauseMenu()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
}
