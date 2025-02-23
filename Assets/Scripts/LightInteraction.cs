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
    public float maxLightIntensity = 50f; // Iþýðýn en yüksek intensity deðeri

    void Start()
    {
        if (intensitySlider != null)
        {
            // Slider'ý etkileþime kapalý yap
            intensitySlider.interactable = false;

            // Slider'ýn maximum deðerini ýþýðýn maksimum intensity'sine ayarlýyoruz
            intensitySlider.maxValue = maxLightIntensity;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Sað týklama baþlatýldýðýnda
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
            // Iþýðýn intensity'sini azalt
            currentLight.intensity -= intensityDecreaseRate * Time.deltaTime;

            // Intensity'nin negatif olmamasý için sýnýr koy
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
