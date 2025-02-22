using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Mermi prefab'ý
    public Transform firePoint; // Merminin çýkacaðý nokta
    public float bulletSpeed = 20f; // Mermi hýzý
    public float fireRate = 0.2f; // Ateþ etme hýzý (saniyede kaç mermi)

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Sol mouse tuþuna basýnca ateþ et
        {
            Shoot();
            nextFireTime = Time.time + fireRate; // Ateþ etme süresini ayarla
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
