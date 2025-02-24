using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    public Text finalTimeText;

    private void Start()
    {
        // DataManager'dan zamaný al ve ekrana yazdýr
        finalTimeText.text = "Final Time: " + DataManager.finalTime;
    }
}