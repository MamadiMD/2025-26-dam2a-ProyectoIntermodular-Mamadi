using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int puntosJugador = 0;
    public int puntosCPU = 0;
    public int puntosParaGanar = 3;

    [Header("UI Final")]
    public bool mostrarFinal = false;
    public string mensajeFinal = "";
    public bool Victoria = false;

    void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void GanaJugador()
    {
        puntosJugador++;
        ComprobarGanador();
    }

    public void GanaCPU()
    {
        puntosCPU++;
        ComprobarGanador();
    }

    // cosas que tengo que hacer : crear las escenas de derrota y vitoria para finalizar el juego
    void ComprobarGanador()
    {
        if (puntosJugador >= puntosParaGanar)
        {
            mensajeFinal = "¡¡VICTORIA!!\nHA GANADO EL JUGADOR LA PARTIDA\n" 
                         + puntosJugador + " - " + puntosCPU;

            mostrarFinal = true;

            SceneManager.LoadScene(1); // tu escena principal
        }
        else if (puntosCPU >= puntosParaGanar)
        {
            mensajeFinal = "DERROTA\nHA GANADO LA CPU LA PARTIDA\n" 
                         + puntosJugador + " - " + puntosCPU;

            mostrarFinal = true;

            SceneManager.LoadScene(1); // tu escena principal
        }
    }

    public void ResetearPuntuacion()
    {
        puntosJugador = 0;
        puntosCPU = 0;
    }

}
