using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class WLCondition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Jugador jugador;        
    [SerializeField] private TextMeshProUGUI mensajeUI;

    private bool juegoTerminado = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        if (juegoTerminado) return;

       
        if (jugador != null && jugador.SaludActual() <= 0)
        {
            Derrota();
            return;
        }

        
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemigo");
        if (enemigos.Length == 0)
        {
            Victoria();
        }
    }
    private void Victoria()
    {
        juegoTerminado = true;
        Debug.Log("¡Victoria! Has derrotado a todos los enemigos.");

        if (mensajeUI != null)
            mensajeUI.text = "¡Victoria!";

        
        Time.timeScale = 0f;
       
    }
    private void Derrota()
    {
        juegoTerminado = true;
        Debug.Log("Has sido derrotado.");

        if (mensajeUI != null)
            mensajeUI.text = "Derrota";

        Time.timeScale = 0f;
        
    }
    
}


