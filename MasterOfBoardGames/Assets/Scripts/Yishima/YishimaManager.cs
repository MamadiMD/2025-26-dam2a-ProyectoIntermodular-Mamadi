using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YishimaManager : MonoBehaviour
{
    public GameObject prefabFicha;
    public Sprite spriteJ1; 
    public Sprite spriteJ2; 
    public List<Nodo> todosLosNodos;

    [Header("Estado del Juego")]
    public PiezaYishima piezaSeleccionada;
    public bool turnoJugador1 = true;
    private bool juegoTerminado = false;

    [Header("Configuración IA")]
    public bool contraCPU = true; // Activa/Desactiva la IA
    public float tiempoEsperaIA = 1.0f;

    [Header("UI Elementos")]
    public GameObject panelVictoria;
    public TMPro.TextMeshProUGUI textoResultado;

    void Start()
    {
        ColocarFichasIniciales();
    }

    void ColocarFichasIniciales()
    {
        // Jugador 1 (Blancas) en los primeros nodos
        CrearPieza(todosLosNodos[0], true);
        CrearPieza(todosLosNodos[1], true);
        CrearPieza(todosLosNodos[2], true);

        // Jugador 2 (Negras) en los nodos opuestos
        CrearPieza(todosLosNodos[5], false);
        CrearPieza(todosLosNodos[6], false);
        CrearPieza(todosLosNodos[7], false);
    }

    void CrearPieza(Nodo nodo, bool esJ1)
    {
        GameObject nuevaFicha = Instantiate(prefabFicha, nodo.transform.position, Quaternion.identity);
        PiezaYishima pieza = nuevaFicha.GetComponent<PiezaYishima>();
        
        pieza.esJugador1 = esJ1;
        SpriteRenderer sr = nuevaFicha.GetComponent<SpriteRenderer>();
        sr.sprite = esJ1 ? spriteJ1 : spriteJ2;

        pieza.MoverA(nodo);
    }

    void Update()
    {
        if (juegoTerminado) return;

        // Bloqueamos clics si es turno de la IA
        if (contraCPU && !turnoJugador1) return;

        if (Input.GetMouseButtonDown(0))
        {
            DetectarClic();
        }
    }

    void DetectarClic()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null)
        {
            // 1. ¿Tocamos una pieza?
            PiezaYishima pieza = hit.collider.GetComponent<PiezaYishima>();
            if (pieza != null)
            {
                if (pieza.esJugador1 == turnoJugador1)
                {
                    piezaSeleccionada = pieza;
                    Debug.Log("Pieza seleccionada: " + (turnoJugador1 ? "Blanca" : "Negra"));
                }
                return;
            }

            // 2. ¿Tocamos un nodo?
            Nodo nodo = hit.collider.GetComponent<Nodo>();
            if (nodo != null && piezaSeleccionada != null)
            {
                IntentarMovimiento(nodo);
            }
        }
    }

    void IntentarMovimiento(Nodo destino)
    {
        if (!destino.ocupado && piezaSeleccionada.nodoActual.vecinos.Contains(destino))
        {
            piezaSeleccionada.MoverA(destino);
            ComprobarVictoria();

            if (!juegoTerminado)
            {
                piezaSeleccionada = null;
                turnoJugador1 = !turnoJugador1;

                // Si ahora es el turno de la IA (Jugador 2)
                if (contraCPU && !turnoJugador1)
                {
                    StartCoroutine(TurnoIA());
                }
            }
        }
    }

    void ComprobarVictoria()
    {
        Nodo centro = todosLosNodos[8];
        if (!centro.ocupado || centro.piezaActual == null) return;

        bool esJ1EnCentro = centro.piezaActual.GetComponent<PiezaYishima>().esJugador1;

        // Revisamos las 4 líneas que pasan por el centro (según tu Inspector)
        if (VerificarLinea(0, 5, esJ1EnCentro)) return; 
        if (VerificarLinea(1, 6, esJ1EnCentro)) return; 
        if (VerificarLinea(2, 7, esJ1EnCentro)) return; 
        if (VerificarLinea(4, 3, esJ1EnCentro)) return; 
    }
    
    bool VerificarLinea(int indexA, int indexB, bool esJ1EnCentro)
    {
        Nodo nodoA = todosLosNodos[indexA];
        Nodo nodoB = todosLosNodos[indexB];

        if (nodoA.ocupado && nodoB.ocupado)
        {
            bool dueñoA = nodoA.piezaActual.GetComponent<PiezaYishima>().esJugador1;
            bool dueñoB = nodoB.piezaActual.GetComponent<PiezaYishima>().esJugador1;
    
            if (dueñoA == esJ1EnCentro && dueñoB == esJ1EnCentro)
            {
                FinalizarJuego(esJ1EnCentro);
                return true;
            }
        }
        return false;
    }

    IEnumerator TurnoIA()
    {
        yield return new WaitForSeconds(tiempoEsperaIA);

        List<PiezaYishima> piezasIA = obtenerPiezas(false);
        Nodo nodoCentro = todosLosNodos[8]; // El centro suele ser el último en la lista
        
        PiezaYishima piezaAEjecutar = null;
        Nodo nodoDestinoAEjecutar = null;

        // PASO 1: ¿PUEDO GANAR? 
        foreach (PiezaYishima p in piezasIA)
        {
            foreach (Nodo vecino in p.nodoActual.vecinos)
            {
                if (!vecino.ocupado)
                {
                    // Simulamos el movimiento temporalmente para ver si ganaría
                    if (SimularVictoriaIA(vecino))
                    {
                        piezaAEjecutar = p;
                        nodoDestinoAEjecutar = vecino;
                        break;
                    }
                }
            }
            if (piezaAEjecutar != null) break;
        }

        // PASO 2: SI NO PUEDO GANAR, ¿PUEDO IR AL CENTRO? 
        if (piezaAEjecutar == null && !nodoCentro.ocupado)
        {
            foreach (PiezaYishima p in piezasIA)
            {
                if (p.nodoActual.vecinos.Contains(nodoCentro))
                {
                    piezaAEjecutar = p;
                    nodoDestinoAEjecutar = nodoCentro;
                    break;
                }
            }
        }

        // PASO 3: MOVIMIENTO POR DEFECTO (EL PRIMERO QUE VEA) 
        if (piezaAEjecutar == null)
        {
            foreach (PiezaYishima p in piezasIA)
            {
                foreach (Nodo vecino in p.nodoActual.vecinos)
                {
                    if (!vecino.ocupado)
                    {
                        piezaAEjecutar = p;
                        nodoDestinoAEjecutar = vecino;
                        break;
                    }
                }
                if (piezaAEjecutar != null) break;
            }
        }

        // EJECUTAR EL MOVIMIENTO ELEGIDO
        if (piezaAEjecutar != null && nodoDestinoAEjecutar != null)
        {
            piezaSeleccionada = piezaAEjecutar;
            IntentarMovimiento(nodoDestinoAEjecutar);
        }
    }

    bool SimularVictoriaIA(Nodo nodoDestino)
    {
        Nodo centro = todosLosNodos[8];
        
        // Si el movimiento no es al centro, y el centro no lo tenemos nosotros, es imposible ganar en este turno
        if (nodoDestino != centro)
        {
            if (!centro.ocupado || centro.piezaActual.GetComponent<PiezaYishima>().esJugador1) 
                return false;
        }

        // Comprobamos si el movimiento completaría una de las líneas (0-5, 1-6, 2-7, 4-3)
        int[,] parejas = { {0, 5}, {1, 6}, {2, 7}, {4, 3} };
        for (int i = 0; i < 4; i++)
        {
            Nodo nA = todosLosNodos[parejas[i, 0]];
            Nodo nB = todosLosNodos[parejas[i, 1]];

            if (nodoDestino == nA && nB.ocupado && !nB.piezaActual.GetComponent<PiezaYishima>().esJugador1) return true;
            if (nodoDestino == nB && nA.ocupado && !nA.piezaActual.GetComponent<PiezaYishima>().esJugador1) return true;
            
            // Si el destino es el centro, ganamos si el extremo A y B son nuestros
            if (nodoDestino == centro)
            {
                if (nA.ocupado && !nA.piezaActual.GetComponent<PiezaYishima>().esJugador1 &&
                    nB.ocupado && !nB.piezaActual.GetComponent<PiezaYishima>().esJugador1) return true;
            }
        }
        return false;
    }

    List<PiezaYishima> obtenerPiezas(bool esJ1)
    {
        List<PiezaYishima> lista = new List<PiezaYishima>();
        PiezaYishima[] todas = FindObjectsOfType<PiezaYishima>();
        foreach (PiezaYishima p in todas)
        {
            if (p.esJugador1 == esJ1) lista.Add(p);
        }
        return lista;
    }

    void FinalizarJuego(bool ganoJ1)
    {
        juegoTerminado = true;
        
        // Activamos el panel que estaba oculto
        panelVictoria.SetActive(true);

        if (ganoJ1)
        {
            textoResultado.text = "¡VICTORIA!\n" + "Ha ganado el Jugador";
            textoResultado.color = Color.green;
            StartCoroutine(GanaJugadorPartida());
            
        }
        else
        {
            textoResultado.text = "DERROTA...\n"+"Ha ganado la CPU";
            textoResultado.color = Color.red;
            StartCoroutine(GanaCpuPartida());
        }
    }

    IEnumerator GanaJugadorPartida()
    {
        GameManager.instance.GanaJugador();
        yield return new WaitForSecondsRealtime(6);
        SceneManager.LoadScene(1);
        
    }

    IEnumerator GanaCpuPartida()
    {
        GameManager.instance.GanaCPU();
        yield return new WaitForSecondsRealtime(6);
        SceneManager.LoadScene(1);
    }

}
