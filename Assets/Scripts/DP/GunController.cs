using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'ı
    public Transform firePoint; // Merminin çıkacağı nokta
    public Camera playerCamera; // Oyuncunun kamerası
    public float bulletSpeed = 60f; // Mermi hızı
    public float fireRate = 3.5f; // Ateş etme süresi (3.5 saniyede bir)
    public float maxShootingDistance = 100f; // Maksimum mermi menzili

    [Header("Ses Efektleri")]
    public AudioSource gunAudio; // Silah sesi kaynağı
    public AudioClip shootSound; // Silah ateş sesi
    public float gunVolume = 0.3f; // Silah sesi seviyesi (0-1 arası)

    [Header("Geri Tepme (Recoil)")]
    public Transform gunTransform; // Silahın kendisi
    public float recoilAmount = 0.1f; // Geri tepme mesafesi
    public float recoilSpeed = 5f; // Geri tepme dönüş hızı
    private Vector3 originalGunPosition; // Silahın başlangıç pozisyonu

    [Header("Mermi İzi (Tracer)")]
    public GameObject tracerPrefab; // Mermi izi (Tracer)
    public float tracerLifetime = 0.2f; // İz ne kadar sürecek

    private float nextFireTime = 0f;

    void Start()
    {
        // Silahın başlangıç pozisyonunu kaydet
        originalGunPosition = gunTransform.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Sol mouse tuşuna basınca ateş et
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // 3.5 saniyede bir ateş etme süresi
        }

        // Silahın geri dönüşünü yumuşat
        gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, originalGunPosition, Time.deltaTime * recoilSpeed);
    }

    void Shoot()
    {
        // Silah sesi çal (ses seviyesi azaltıldı)
        if (gunAudio != null && shootSound != null)
        {
            gunAudio.PlayOneShot(shootSound, gunVolume);
        }

        // Kamera'nın baktığı noktaya doğru mermi yönünü belirle
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, maxShootingDistance))
        {
            targetPoint = hit.point; // Mermi çarpacağı noktayı belirler
        }
        else
        {
            targetPoint = ray.GetPoint(maxShootingDistance); // Eğer bir nesneye çarpmazsa düz ileri gider
        }

        // Mermi spawn noktası
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Mermiyi hedefe yönlendir
        Vector3 direction = (targetPoint - firePoint.position).normalized;
        rb.linearVelocity = direction * bulletSpeed;

        Destroy(bullet, 3f); // 3 saniye sonra mermiyi yok et

        // Geri tepme efekti
        gunTransform.localPosition -= new Vector3(0, 0, recoilAmount);

        // Mermi izi (Tracer) oluştur
        if (tracerPrefab != null)
        {
            GameObject tracer = Instantiate(tracerPrefab, firePoint.position, Quaternion.LookRotation(direction));
            Destroy(tracer, tracerLifetime);
        }
    }
}
