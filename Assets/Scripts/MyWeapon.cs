using UnityEngine;
using TMPro; 

public class MyWeapon : MonoBehaviour
{
    [Header("Disparo")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed = 10000f;
    [SerializeField] private float fireRate = 0.1f;

    [Header("Munición")]
    [SerializeField] private int maxAmmo = 9;       
    [SerializeField] private int currentAmmo = 9;   
    [SerializeField] private float reloadTime = 2f;  
    [SerializeField] private bool isReloading = false;

    [Header("UI (opcional)")]
    [SerializeField] private TextMeshProUGUI Municion; 

    private float nextFireTime = 0f;

    void Start()
    {
        UpdateAmmoUI();
    }

    void Update()
    {
        if (isReloading) return;

       
        if (Input.GetButton("Fire1"))
        {
            Fire();
        }

        
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentAmmo < maxAmmo)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private void Fire()
    {
        if (Time.time >= nextFireTime)
        {
            if (currentAmmo > 0)
            {
               
                GameObject newBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
                Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>();
                bulletRigidbody.AddForce(spawnPoint.forward * bulletSpeed);

               
                Destroy(newBullet, 2f);

                
                currentAmmo--;
                nextFireTime = Time.time + 1f / fireRate;
                UpdateAmmoUI();
            }
            else
            {
                Debug.Log("Sin munición. Presiona R para recargar.");
            }
        }
    }

    private System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recargando...");

        
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
        Debug.Log("Recarga completa.");
    }

    private void UpdateAmmoUI()
    {
        if (Municion != null)
        {
            Municion.text = currentAmmo + " / " + maxAmmo;
        }
    }
}
