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
    private Light currentLight = null;
    private bool isInInteractionZone = false;
    public float maxLightIntensity = 50f;

    // GunController'a referans
    public GunController gunController;

    // Ammo alma kontrol�
    private bool hasCollectedAmmo = false;

    void Start()
    {
        if (intensitySlider != null)
        {
            intensitySlider.interactable = false;
            intensitySlider.maxValue = maxLightIntensity;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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
                    hasCollectedAmmo = false; // Etkile�ime girildi�inde s�f�rla

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
            currentLight.intensity -= intensityDecreaseRate * Time.deltaTime;

            if (currentLight.intensity <= 0)
            {
                currentLight.enabled = false;
                sliderCanvas.enabled = false;
                lightCollider.enabled = false;

                // Ammo art�rma i�lemi (sadece bir kez)
                if (!hasCollectedAmmo && gunController != null && gunController.ammo < gunController.maxAmmo)
                {
                    gunController.ammo++;
                    gunController.bulletCount.text = gunController.ammo.ToString();
                    hasCollectedAmmo = true; // Ammo al�nd�, bir daha art�rmas�n
                }
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
        if (intensitySlider != null && currentLight != null)
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
