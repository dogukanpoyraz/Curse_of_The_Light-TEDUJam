using UnityEngine;

public class InfoToggle : MonoBehaviour
{
    public GameObject infoPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }
}
