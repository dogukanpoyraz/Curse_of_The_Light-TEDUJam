using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColorFilter : MonoBehaviour
{
    public Image colorFilterPanel; // Canvas i�indeki panel
    public float transitionSpeed = 1f; // Renk ge�i� h�z�
    public float filterDuration = 5f; // Filtrenin otomatik kapanma s�resi

    public AudioSource visionStartAudio; // Vision Start sesi
    public AudioSource visionEndAudio;   // Vision End sesi

    private Color targetColor;
    private Color normalColor = new Color(0, 0, 0, 0); // �effaf
    private Color redColor = new Color(0.5f, 0, 0, 0.3f); // K�rm�z�
    private Color greenColor = new Color(0, 0.5f, 0, 0.3f); // Ye�il
    private Color blueColor = new Color(0, 0, 0.5f, 0.3f); // Mavi

    private Coroutine resetCoroutine; // Eski haline d�nme s�reci
    private bool isEndSoundPlayed = false; // End sesinin �al�p �almad���n� takip eder

    void Start()
    {
        targetColor = normalColor;
        colorFilterPanel.color = normalColor;
    }

    void Update()
    {
        // Renk ge�i�ini yumu�at
        colorFilterPanel.color = Color.Lerp(colorFilterPanel.color, targetColor, Time.deltaTime * transitionSpeed);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StopResetCoroutine(); // Otomatik resetlemeyi iptal et
            SetColor(normalColor, false); // Normal g�r��
            if (!isEndSoundPlayed) // E�er end sesi daha �nce �almad�ysa �al
            {
                PlayVisionEndSound();
                isEndSoundPlayed = true; // End sesini oynatt���m�z� i�aretle
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
        isEndSoundPlayed = false; // Yeni g�r�� a��ld��� i�in end sesi tekrar �al�nabilir
    }

    void SetColor(Color newColor, bool autoReset)
    {
        targetColor = newColor;

        // E�er renk eski haline otomatik d�necekse �nceki s�reci iptal edip yenisini ba�lat
        if (autoReset)
        {
            StopResetCoroutine();
            resetCoroutine = StartCoroutine(ResetFilterAfterTime(filterDuration));
        }
    }

    IEnumerator ResetFilterAfterTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        targetColor = normalColor; // Renk ge�i�ini tekrar �effaf yap

        if (!isEndSoundPlayed) // E�er end sesi zaten �al�nmad�ysa �al
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
