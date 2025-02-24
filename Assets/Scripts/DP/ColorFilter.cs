using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorFilter : MonoBehaviour
{
    public Image colorFilterPanel; // Canvas içindeki panel
    public float transitionSpeed = 1f; // Renk geçiþ hýzý
    public float filterDuration = 5f; // Filtrenin otomatik kapanma süresi

    public AudioSource visionStartAudio; // Vision Start sesi
    public AudioSource visionEndAudio;   // Vision End sesi

    private Color targetColor;
    private Color normalColor = new Color(0, 0, 0, 0); // Þeffaf
    private Color redColor = new Color(0.5f, 0, 0, 0.3f); // Kýrmýzý
    private Color greenColor = new Color(0, 0.5f, 0, 0.3f); // Yeþil
    private Color blueColor = new Color(0, 0, 0.5f, 0.3f); // Mavi

    private Coroutine resetCoroutine; // Eski haline dönme süreci
    private bool isEndSoundPlayed = false; // End sesinin çalýp çalmadýðýný takip eder

    void Start()
    {
        targetColor = normalColor;
        colorFilterPanel.color = normalColor;
    }

    void Update()
    {
        // Renk geçiþini yumuþat
        colorFilterPanel.color = Color.Lerp(colorFilterPanel.color, targetColor, Time.deltaTime * transitionSpeed);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopResetCoroutine(); // Otomatik resetlemeyi iptal et
            SetColor(normalColor, false); // Normal görüþ
            if (!isEndSoundPlayed) // Eðer end sesi daha önce çalmadýysa çal
            {
                PlayVisionEndSound();
                isEndSoundPlayed = true; // End sesini oynattýðýmýzý iþaretle
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartVisionMode(redColor);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartVisionMode(greenColor);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartVisionMode(blueColor);
        }
    }

    void StartVisionMode(Color visionColor)
    {
        SetColor(visionColor, true);
        PlayVisionStartSound();
        isEndSoundPlayed = false; // Yeni görüþ açýldýðý için end sesi tekrar çalýnabilir
    }

    void SetColor(Color newColor, bool autoReset)
    {
        targetColor = newColor;

        // Eðer renk eski haline otomatik dönecekse önceki süreci iptal edip yenisini baþlat
        if (autoReset)
        {
            StopResetCoroutine();
            resetCoroutine = StartCoroutine(ResetFilterAfterTime(filterDuration));
        }
    }

    IEnumerator ResetFilterAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        targetColor = normalColor; // Renk geçiþini tekrar þeffaf yap

        if (!isEndSoundPlayed) // Eðer end sesi zaten çalýnmadýysa çal
        {
            PlayVisionEndSound();
            isEndSoundPlayed = true;
        }
    }

    void StopResetCoroutine()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }
    }

    void PlayVisionStartSound()
    {
        if (visionStartAudio != null)
        {
            visionStartAudio.Play();
        }
    }

    void PlayVisionEndSound()
    {
        if (visionEndAudio != null)
        {
            visionEndAudio.Play();
        }
    }
}
