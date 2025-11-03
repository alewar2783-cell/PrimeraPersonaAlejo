using UnityEngine;
using TMPro;
using System.Collections;

public class Mision : MonoBehaviour
{
   
    public static Mision instancia;

    [Header("Configuración de enemigos")]
    [SerializeField] private int totalEnemigos = 25;
    private int enemigosRestantes;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI mensajeTMP;
    [SerializeField] private TextMeshProUGUI contadorTMP;

    [Header("Configuración del mensaje inicial")]
    [SerializeField] private float duracionMensajeInicial = 5f;

    [Header("Configuración del mensaje final")]
    [SerializeField] private float duracionMensajeFinal = 5f;

    private bool juegoIniciado = false;

   
    private void Awake()
    {
       
        instancia = this;
    }

    private void Start()
    {
        enemigosRestantes = totalEnemigos;

        
        contadorTMP.gameObject.SetActive(false);

      
        mensajeTMP.text = $"Elimina a los {totalEnemigos} enemigos!";
        StartCoroutine(MostrarMensajeInicial());
    }

    
    private IEnumerator MostrarMensajeInicial()
    {
        
        Time.timeScale = 0f;
        mensajeTMP.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(duracionMensajeInicial);

        
        Time.timeScale = 1f;
        mensajeTMP.gameObject.SetActive(false);
        contadorTMP.gameObject.SetActive(true);
        juegoIniciado = true;

        ActualizarContador();
    }

    
    public void EnemigoEliminado()
    {
        if (!juegoIniciado) return;

        enemigosRestantes--;
        ActualizarContador();

        if (enemigosRestantes <= 0)
        {
            StartCoroutine(MostrarMensajeFinal());
        }
    }

    
    private void ActualizarContador()
    {
        contadorTMP.text = $"Enemigos restantes: {enemigosRestantes}";
    }

    
    private IEnumerator MostrarMensajeFinal()
    {
        juegoIniciado = false;

        
        mensajeTMP.text = "¡Misión completada!";
        mensajeTMP.gameObject.SetActive(true);

        contadorTMP.gameObject.SetActive(false);
        Time.timeScale = 0f;

        yield return new WaitForSecondsRealtime(duracionMensajeFinal);

        
        mensajeTMP.gameObject.SetActive(false);
        Time.timeScale = 1f;

        Debug.Log("Misión completada correctamente.");
    }
}
