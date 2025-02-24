using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelOnCollision : MonoBehaviour
{
    [Header("Scene Settings")]
    public string sceneNameToLoad; // Yüklenecek sahnenin adı

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " " + other.tag,other.transform);
        // Eğer trigger'a giren obje "Player" tag'ine sahipse sahne değiştir
        if (other.CompareTag("TriggerBoxEnding"))
        {
            TimerController.instance.EndTimer();
            // Sahne değiştir
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
