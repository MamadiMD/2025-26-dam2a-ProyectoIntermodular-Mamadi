using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Header("UI del Juego")]
    public Button[] casillas;
    public TextMeshProUGUI textoTurno;


    private string turnoActual = "X"; 
    private bool juegoTerminado = false;
    private int totalJugadas = 0;

    void Start()
    {
        ConfigurarBotonesDelTablero();

        ActualizarTextoPantalla("Turno de: " + turnoActual);
    }
    void ConfigurarBotonesDelTablero()
    {
        for (int i = 0; i < casillas.Length; i++)
        {
            int indiceCasilla = i; // Guardamos la posición fija de este botón (0 al 8)
            casillas[indiceCasilla].GetComponentInChildren<TextMeshProUGUI>().text = "";

            // Cuando pinchen en esta casilla, llamamos al método encargado de procesar la jugada
            casillas[indiceCasilla].onClick.AddListener(() => EnCasillaClick(indiceCasilla));
        }
    }

    void EnCasillaClick(int indice)
    {
        // 1. Validar si se puede jugar en esa casilla
        string textoEnCasilla = casillas[indice].GetComponentInChildren<TextMeshProUGUI>().text;
        if (juegoTerminado || textoEnCasilla != "") return; 

        // 2. Marcar la jugada de forma visual
        casillas[indice].GetComponentInChildren<TextMeshProUGUI>().text = turnoActual;
        totalJugadas++;

        // 3. Comprobar si esta jugada ha hecho ganar a alguien
        if (ComprobarSiHayGanador())
        {
            ActualizarTextoPantalla("¡Ganador: " + turnoActual + "!");
            juegoTerminado = true;
            SceneManager.LoadScene(1);
            return;
        }

        // 4. Comprobar si nos hemos quedado sin casillas (Empate)
        if (totalJugadas == 9)
        {
            ActualizarTextoPantalla("¡Empate!");
            juegoTerminado = true;
            SceneManager.LoadScene(1);
            return;
        }

        // 5. Si nadie ganó ni empató, pasamos el turno al siguiente
        CambiarTurno();
    }

    bool ComprobarSiHayGanador()
    {
        if (
            // Horizontales
            (ObtenerTexto(0) == turnoActual && ObtenerTexto(1) == turnoActual && ObtenerTexto(2) == turnoActual) ||
            (ObtenerTexto(3) == turnoActual && ObtenerTexto(4) == turnoActual && ObtenerTexto(5) == turnoActual) ||
            (ObtenerTexto(6) == turnoActual && ObtenerTexto(7) == turnoActual && ObtenerTexto(8) == turnoActual) ||
            // Verticales
            (ObtenerTexto(0) == turnoActual && ObtenerTexto(3) == turnoActual && ObtenerTexto(6) == turnoActual) ||
            (ObtenerTexto(1) == turnoActual && ObtenerTexto(4) == turnoActual && ObtenerTexto(7) == turnoActual) ||
            (ObtenerTexto(2) == turnoActual && ObtenerTexto(5) == turnoActual && ObtenerTexto(8) == turnoActual) ||
            // Diagonales
            (ObtenerTexto(0) == turnoActual && ObtenerTexto(4) == turnoActual && ObtenerTexto(8) == turnoActual) ||
            (ObtenerTexto(2) == turnoActual && ObtenerTexto(4) == turnoActual && ObtenerTexto(6) == turnoActual)
           )
        {
            return true; // Alguien ganó
        }

        return false; // Nadie ha ganado todavía
    }

    void CambiarTurno()
    {
        turnoActual = (turnoActual == "X") ? "O" : "X";
        ActualizarTextoPantalla("Turno de: " + turnoActual);
    }

    string ObtenerTexto(int indice)
    {
        return casillas[indice].GetComponentInChildren<TextMeshProUGUI>().text;
    }

    // Cambia el texto del letrero principal de la pantalla
    void ActualizarTextoPantalla(string mensaje)
    {
        textoTurno.text = mensaje;
    }
}
