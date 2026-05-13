using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject canvasMenu;
    public GameObject canvasOpciones;
    public AudioSource audioClick;


    // Botón opciones desde el menu inicial
    public void IrAOpcionesMenu()
    {
        audioClick.Play();
        canvasMenu.SetActive(false);
        canvasOpciones.SetActive(true);
    }

    public void CerrarOpciones()
    {
        audioClick.Play();
        canvasOpciones.SetActive(false);
        canvasMenu.SetActive(true);
        
    }

    // Botón Salir
    public void Salir()
    {
        audioClick.Play();
        Application.Quit();
        Debug.Log("Saliendo del juego"); // útil en el editor
    }

    public void Jugar()
    {
        audioClick.Play();
        SceneManager.LoadScene(1);
    }
}
