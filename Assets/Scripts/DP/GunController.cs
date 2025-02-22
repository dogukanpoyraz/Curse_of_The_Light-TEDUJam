using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'�
    public Transform firePoint; // Merminin ��kaca�� nokta
    public float bulletSpeed = 20f; // Mermi h�z�
    public float fireRate = 0.2f; // Ate� etme h�z� (saniyede ka� mermi)

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Sol mouse tu�una bas�nca ate� et
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Ate� etme s�resini ayarla
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 3f); // 3 saniye sonra mermiyi yok et
    }
}
