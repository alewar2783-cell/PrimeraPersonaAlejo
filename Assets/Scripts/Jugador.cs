using TMPro;
using UnityEngine;
using System.Collections;

public class Jugador : MonoBehaviour
{
    [SerializeField] private int salud = 100;
    [SerializeField] private TextMeshProUGUI SaludT;
    [SerializeField] private float duracionInvencibilidad = 2f;
    [SerializeField] private bool Invencible = false;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogWarning("No se encontró un CharacterController en el jugador. Agrega uno desde el Inspector.");
        }

        UpdateUI();
    }

    private void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemigo"))
        {
            if (!Invencible) 
            {
                Debug.Log("Me tocó el enemigo!");
                ApplyDamage(20);
            }
            else
            {
                Debug.Log("Golpe bloqueado — el jugador es invencible temporalmente.");
            }
        }
    }

    private void ApplyDamage(int danioRecibido)
    {
        salud -= danioRecibido;
        Debug.Log($"He recibido {danioRecibido} de daño. Salud restante: {salud}");
        UpdateUI();

        if (salud <= 0)
        {
            Debug.Log("He muerto!");
            
        }
        else
        {
            StartCoroutine(ActivarInvencibilidad());
        }
    }

    private IEnumerator ActivarInvencibilidad()
    {
        Invencible = true;
        Debug.Log("Invencibilidad activada");

        
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            for (float i = 0; i < duracionInvencibilidad; i += 0.2f)
            {
                renderer.enabled = !renderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }
            renderer.enabled = true;
        }

        yield return new WaitForSeconds(duracionInvencibilidad);

        Invencible = false;
        Debug.Log("Invencibilidad terminada.");
    }

    private void UpdateUI()
    {
        if (SaludT != null)
            SaludT.text = salud.ToString();
    }

    public int SaludActual()
    {
        return salud;
    }
}
