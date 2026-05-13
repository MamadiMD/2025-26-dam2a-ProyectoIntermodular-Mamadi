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
            CargarPartida();
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void GanaJugador()
    {
        puntosJugador++;
        GuardarPartida();
        ComprobarGanador();
    }

    public void GanaCPU()
    {
        puntosCPU++;
        GuardarPartida();
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

            BorrarDatosGuardados();
            SceneManager.LoadScene(1); 
        }
        else if (puntosCPU >= puntosParaGanar)
        {
            mensajeFinal = "DERROTA\nHA GANADO LA CPU LA PARTIDA\n" 
                         + puntosJugador + " - " + puntosCPU;

            mostrarFinal = true;
            
            BorrarDatosGuardados();
            SceneManager.LoadScene(1); 
        }
    }

    public void ResetearPuntuacion()
    {
        puntosJugador = 0;
        puntosCPU = 0;
    }

    public void GuardarPartida()
    {
        PlayerPrefs.SetInt("PuntosJugador", puntosJugador);
        PlayerPrefs.SetInt("PuntosCPU", puntosCPU);

        PlayerPrefs.Save();

        Debug.Log("Partida guardada");
    }

    public void CargarPartida()
    {
        puntosJugador = PlayerPrefs.GetInt("PuntosJugador", 0);
        puntosCPU = PlayerPrefs.GetInt("PuntosCPU", 0);

        Debug.Log("Partida cargada");
    }


    public void NuevaPartida()
    {
        ResetearPuntuacion();
    
        PlayerPrefs.DeleteKey("PuntosJugador");
        PlayerPrefs.DeleteKey("PuntosCPU");
    
        SceneManager.LoadScene(1);
    }

    public void BorrarDatosGuardados()
    {
        PlayerPrefs.DeleteKey("PuntosJugador");
        PlayerPrefs.DeleteKey("PuntosCPU");
        ResetearPuntuacion();
    }

}
