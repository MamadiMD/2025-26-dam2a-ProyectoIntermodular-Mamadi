using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsController : MonoBehaviour
{
    public GameObject canvasOpciones;
    public GameObject canvasAjedrez;
    public AudioSource audioClick;
    public GameObject canvasDamas;
    public GameObject canvasDomino;
    public GameObject canvasBrisca;
    public GameObject canvasShisima;
    public GameObject canvasTresRaya;

    public AudioSource audioDomino; 
    public AudioSource audioAjedrez; 
    public AudioSource audioDamas; 
    public AudioSource audioShisima; 
    public AudioSource audioBrisca; 
    public AudioSource audioTresRaya; 
    public AudioSource audioGeneral; 
    public void VolverMenu()
    {
        audioClick.Play();
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(0);
    }

    public void Reiniciar()
    {
        audioClick.Play();
        GameManager.instance.NuevaPartida();
        
    }

    public void CerrarOpciones()
    {
        audioClick.Play();
        canvasOpciones.SetActive(false);
    }

    public void AbrirOpciones()
    {
        audioClick.Play();
        canvasOpciones.SetActive(true);
    }

    // Ajedrez Options

    public void CerrarAjedrez()
    {
        audioClick.Play();
        canvasAjedrez.SetActive(false);
        audioAjedrez.Pause();
        audioGeneral.Play();
    }

    public void JugarAjedrez()
    {
        audioClick.Play();
        SceneManager.LoadScene(2);
    }

    public void AbrirAjedrez()
    {
        audioClick.Play();
        canvasAjedrez.SetActive(true);
        audioGeneral.Pause();
        audioAjedrez.Play();
    }

    // Damas Options
    public void CerrarDamas()
    {
        audioClick.Play();
        canvasDamas.SetActive(false);
        audioDamas.Pause();
        audioGeneral.Play();
    }

    public void JugarDamas()
    {
        audioClick.Play();
        SceneManager.LoadScene(3);
    }

    public void AbrirDamas()
    {
        audioClick.Play();
        canvasDamas.SetActive(true);
        audioGeneral.Pause();
        audioDamas.Play();
    }

    // Domino Options
    public void CerrarDomino()
    {
        audioClick.Play();
        canvasDomino.SetActive(false);
        audioDomino.Pause();
        audioGeneral.Play();
    }

    public void JugarDomino()
    {
        audioClick.Play();
        SceneManager.LoadScene(4);
    }

    public void AbrirDomino()
    {
        audioClick.Play();
        canvasDomino.SetActive(true);
        audioGeneral.Pause();
        audioDomino.Play();
    }

    // Brisca Options

    public void CerrarBrisca()
    {
        audioClick.Play();
        canvasBrisca.SetActive(false);
        audioBrisca.Pause();
        audioGeneral.Play();
    }

    public void JugarBrisca()
    {
        audioClick.Play();
        SceneManager.LoadScene(5);
    }

    public void AbrirBrisca()
    {
        audioClick.Play();
        canvasBrisca.SetActive(true);
        audioGeneral.Pause();
        audioBrisca.Play();
    }

    // Shisima options
    public void CerrarShisima()
    {
        audioClick.Play();
        canvasShisima.SetActive(false);
        audioShisima.Pause();
        audioGeneral.Play();
    }

    public void JugarShisima()
    {
        audioClick.Play();
        SceneManager.LoadScene(6);
    }

    public void AbrirShisima()
    {
        audioClick.Play();
        canvasShisima.SetActive(true);
        audioGeneral.Pause();
        audioShisima.Play();
    }

    //Tres en Raya

    public void CerrarTresRaya()
    {
        audioClick.Play();
        canvasTresRaya.SetActive(false);
        audioTresRaya.Pause();
        audioGeneral.Play();
    }

    public void JugarTresRaya()
    {
        audioClick.Play();
        SceneManager.LoadScene(7);
    }

    public void AbrirTresRaya()
    {
        audioClick.Play();
        canvasTresRaya.SetActive(true);
        audioGeneral.Pause();
        audioTresRaya.Play();
    }
}
