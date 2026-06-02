using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("UI del Juego")]
    public Button[] casillas;
    public TextMeshPro textoTurno;
    public Button botonReiniciar;

    private string turnoActual = "X"; 
    private bool juegoTerminado = false;
    private int totalJugadas = 0;

    void Start()
    {
        
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
