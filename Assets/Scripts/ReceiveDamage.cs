using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReceiveDamage : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;

    [SerializeField] private GameObject sangrePrefab;
    [SerializeField] private Transform puntoImpacto;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            ApplyDamage(collision);
        }
    }

    private void ApplyDamage(Collision collision)
    {
        Debug.Log("He recibido un disparo!");
        int damageReceived = collision.gameObject.GetComponent<Damage>().damageAmount;
        health -= damageReceived;

        SpawnSangre(collision);

        UpdateUI();

        if (health <= 0)
        {
            Debug.Log("He muerto!");

            
            Mision.instancia?.EnemigoEliminado();

            Destroy(gameObject);
        }

        else
        {
            Debug.Log("Salud restante: " + health);
        }
    }

    private void SpawnSangre(Collision collision)
    {
        if (sangrePrefab != null)
        {

            Vector3 posicion = puntoImpacto != null
                ? puntoImpacto.position
                : collision.contacts[0].point;

            Quaternion rotacion = Quaternion.LookRotation(collision.contacts[0].normal);

            GameObject sangre = Instantiate(sangrePrefab, posicion, rotacion);


            Destroy(sangre, 2f);
        }
        else
        {
            Debug.LogWarning("No se asignó un prefab de sangre en el inspector.");
        }
    }

    private void UpdateUI()
    {
        healthText.text = "Health: " + health;
        healthBar.fillAmount = health / 100f;
    }
}
