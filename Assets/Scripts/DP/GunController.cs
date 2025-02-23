using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'ı
    public Transform firePoint; // Merminin çıkacağı nokta
    public Camera playerCamera; // Oyuncunun kamerası
    public float bulletSpeed = 60f; // Mermi hızı
    public float fireRate = 3.5f; // Ateş etme süresi (3.5 saniyede bir)
    public float maxShootingDistance = 100f; // Maksimum mermi menzili
    public Text bulletCount;

    [Header("Cephane Ayarları")]
    public int ammo = 12; // Başlangıç cephane miktarı
    public int maxAmmo = 20; // Maksimum cephane miktarı
    public bool canReload = true; // Yeniden doldurma açık mı?

    [Header("Ses Efektleri")]
    public AudioSource gunAudio; // Silah sesi kaynağı
    public AudioClip shootSound; // Silah ateş sesi
    public AudioClip emptyGunSound; // Cephane bittiğinde çıkacak ses
    public float gunVolume = 0.3f; // Silah sesi seviyesi (0-1 arası)

    [Header("Geri Tepme (Recoil)")]
    public Transform gunTransform; // Silahın kendisi
    public float recoilAmount = 0.1f; // Geri tepme mesafesi
    public float recoilSpeed = 5f; // Geri tepme dönüş hızı
    private Vector3 originalGunPosition; // Silahın başlangıç pozisyonu

    [Header("Mermi İzi (Tracer)")]
    public GameObject tracerPrefab; // Mermi izi (Tracer)
    public float tracerLifetime = 0.2f; // İz ne kadar sürecek
    public bool gamePaused = false;

    private float nextFireTime = 0f;

    void Start()
    {
        // Silahın başlangıç pozisyonunu kaydet
        originalGunPosition = gunTransform.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            if (ammo > 0)
            {
                Shoot(gamePaused);
                ammo--; // Her ateş ettiğinde cephaneyi azalt
                bulletCount.text = ammo.ToString();
                Debug.Log(ammo.ToString());
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                // Cephane bittiyse ses efekti çal
                if (gunAudio != null && emptyGunSound != null)
                {
                    gunAudio.PlayOneShot(emptyGunSound, gunVolume);
                }
            }
        }

        // Silahın geri dönüşünü yumuşat
        gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, originalGunPosition, Time.deltaTime * recoilSpeed);

        // R tuşuna basınca cephane doldurma
        if (canReload && Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot(bool gamePaused)
    {
        if (!gamePaused)
        {

            // Silah sesi çal (varsa)
            if (gunAudio != null && shootSound != null)
            {
                gunAudio.PlayOneShot(shootSound, gunVolume);
            }

            // Kamera'nın baktığı noktaya doğru bir Ray gönder
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit, maxShootingDistance))
            {
                targetPoint = hit.point; // Eğer bir nesneye çarptıysa, orası hedef
            }
            else
            {
                targetPoint = ray.GetPoint(maxShootingDistance); // Eğer çarpmadıysa ileriye devam et
            }

            // Mermiyi oluştur ve yönlendir
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            // Mermiyi hedefe yönlendir
            Vector3 direction = (targetPoint - firePoint.position).normalized;
            bullet.transform.rotation = Quaternion.LookRotation(direction); // Mermiyi hedefe yönlendir
            rb.velocity = direction * bulletSpeed;

            Destroy(bullet, 3f); // 3 saniye sonra mermiyi yok et

            // Geri tepme efekti
            gunTransform.localPosition -= new Vector3(0, 0, recoilAmount);

            // Mermi izi (Tracer) oluştur
            if (tracerPrefab != null)
            {
                GameObject tracer = Instantiate(tracerPrefab, firePoint.position, Quaternion.LookRotation(direction));
                Destroy(tracer, tracerLifetime);
            }

        }

    }

    void Reload()
    {
        if (ammo < maxAmmo)
        {
            ammo = maxAmmo;
            Debug.Log("Cephane dolduruldu!");
        }
    }
}