using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("UI del Juego")]
    public Button[] casillas;
    public TextMeshPro textoTurno;
    public Button botonReiniciar;
    public AudioSource audioClick;

    private string turnoActual = "X"; 
    private bool juegoTerminado = false;
    private int totalJugadas = 0;

    void Start()
    {
        
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
