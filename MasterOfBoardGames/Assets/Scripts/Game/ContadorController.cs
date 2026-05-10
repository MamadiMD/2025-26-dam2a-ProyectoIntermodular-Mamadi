using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContadorController : MonoBehaviour
{
    public TMP_Text contadorJugador;
    public TMP_Text contadorCPU;

    [Header("Panel Final")]
    public GameObject panelFinal;
    public TMP_Text textoResultado;

    [Header("Musica")]
    public AudioSource audioGeneral; 


    void Start()
    {
        ActualizarContador();
        ComprobarFinal();
    }

    void OnEnable()
    {
        ActualizarContador();
    }

    void ActualizarContador()
    {
        contadorJugador.text = $"Jugador: {GameManager.instance.puntosJugador}";
        contadorCPU.text = $"CPU: {GameManager.instance.puntosCPU}";
    }

    void ComprobarFinal()
    {
        if (GameManager.instance.mostrarFinal)
        {
            audioGeneral.Pause();

            panelFinal.SetActive(true);
            textoResultado.text = GameManager.instance.mensajeFinal;

            // para que no vuelva a aparecer otra vez
            GameManager.instance.mostrarFinal = false;
        }
        else
        {
            panelFinal.SetActive(false);
        }
    }
}
