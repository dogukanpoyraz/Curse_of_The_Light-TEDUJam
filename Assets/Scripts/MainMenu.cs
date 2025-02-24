using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject creditsMenuPanel;
    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Slide");
        Debug.Log("play");
        Cursor.visible = false;
    }

	public void SettingsGame()
    {
        mainMenuPanel.SetActive(false);
        settingsMenuPanel.SetActive(true);
    }

    public void CreditsGame()
    {
        mainMenuPanel.SetActive(false);
        creditsMenuPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitted Game");
    }
}
