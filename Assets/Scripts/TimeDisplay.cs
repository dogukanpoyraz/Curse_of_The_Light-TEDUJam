using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public Text finalTimeText;

    private void Start()
    {
        // DataManager'dan zaman� al ve ekrana yazd�r
        finalTimeText.text = "Final Time: " + DataManager.finalTime;
    }
}