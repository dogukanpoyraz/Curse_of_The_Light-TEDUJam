using UnityEngine;
using UnityEngine.UI;

public class LightInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionRange = 2f;
    public LayerMask lightLayer;
    public float intensityDecreaseRate = 20f;

    private Canvas sliderCanvas;
    private Slider intensitySlider;
    private Collider lightCollider;
    public GunController gunController;
    private Light currentLight = null;
    private bool isInInteractionZone = false;
    public float maxLightIntensity = 50f; // I����n en y�ksek intensity de�eri

    void Start()
    {
        if (intensitySlider != null)
        {
            // Slider'� etkile�ime kapal� yap
            intensitySlider.interactable = false;

            // Slider'�n maximum de�erini �����n maksimum intensity'sine ayarl�yoruz
            intensitySlider.maxValue = maxLightIntensity;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Sa� t�klama ba�lat�ld���nda
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange, lightLayer))
            {
                Light lightComponent = hit.transform.GetComponentInChildren<Light>();
                sliderCanvas = lightComponent.GetComponentInChildren<Canvas>();
                intensitySlider = sliderCanvas.GetComponentInChildren<Slider>();
                lightCollider = lightComponent.GetComponent<Collider>();

                if (lightComponent != null && sliderCanvas != null && intensitySlider != null)
                {
                    currentLight = lightComponent;
                    isInInteractionZone = true;
                    if (currentLight.intensity <= 0)
                    {
                        sliderCanvas.enabled = false;
                        lightCollider.enabled = false;
                    }
                    else
                    {
                        lightCollider.enabled = true;
                        sliderCanvas.enabled = true;
                    }
                    UpdateHealthBar();
                }
            }
        }

        if (Input.GetMouseButton(1) && currentLight != null && isInInteractionZone && sliderCanvas != null && intensitySlider != null)
        {
            // I����n intensity'sini azalt
            currentLight.intensity -= intensityDecreaseRate * Time.deltaTime;

            // Intensity'nin negatif olmamas� i�in s�n�r koy
            if (currentLight.intensity <= 0)
            {
                currentLight.enabled = false;
                sliderCanvas.enabled = false;
                lightCollider.enabled = false;
            }
            UpdateHealthBar();
        }

        if (Input.GetMouseButtonUp(1))
        {
            isInInteractionZone = false;
            currentLight = null;
            if (sliderCanvas != null && intensitySlider != null)
            {
                sliderCanvas.enabled = false;
            }
        }
    }

    void UpdateHealthBar()
    {
        if (intensitySlider != null && currentLight != null )
        {
            if (currentLight.intensity <= 0)
            {
                currentLight.enabled = false;
                sliderCanvas.enabled = false;
                lightCollider.enabled = false;
            }
            else
            {
                lightCollider.enabled = true;
                sliderCanvas.enabled = true;
                intensitySlider.value = currentLight.intensity;
            }
        }
    }
}
