using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class BackToMenu : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject settingsMenuPanel;
    public GameObject creditsMenuPanel;

    public void SettingsBackToTheMenu()
    {
        settingsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void CreditsBackToTheMenu()
    {
        creditsMenuPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }


}
