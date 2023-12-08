using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class Palanca : MonoBehaviour {

    //    public Animator palancaAnimator;
    //private bool palancaActivada = false;

    //public GameObject puertaDesactivar;
    //public GameObject puertaActivar;

    //public TextMeshProUGUI textoInteraccion;
    //private bool mostrarTexto = false;

    //void Update()
    //{
    //    if (mostrarTexto && Input.GetKeyDown(KeyCode.E))
    //    {
    //        // Invertir el estado de la palanca al presionar la tecla "E"
    //        palancaActivada = !palancaActivada;

    //        // Actualizar el parámetro del Animator según el estado de la palanca
    //        palancaAnimator.SetBool("PalancaActivada", palancaActivada);

    //        // Activar o desactivar las puertas según el estado de la palanca
    //        if (palancaActivada)
    //        {
    //            ActivarPuerta(puertaActivar);
    //            DesactivarPuerta(puertaDesactivar);
    //        }
    //        else
    //        {
    //            ActivarPuerta(puertaDesactivar);
    //            DesactivarPuerta(puertaActivar);
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("PJ"))
    //    {
    //        Debug.Log("Player cerca de la palanca");
    //        mostrarTexto = true;
    //        textoInteraccion.gameObject.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("PJ"))
    //    {
    //        mostrarTexto = false;
    //        textoInteraccion.gameObject.SetActive(false);
    //    }
    //}

    //// Método para activar una puerta
    //private void ActivarPuerta(GameObject puerta)
    //{
    //    if (puerta != null)
    //    {
    //        puerta.SetActive(true);
    //    }
    //}

    //// Método para desactivar una puerta
    //private void DesactivarPuerta(GameObject puerta)
    //{
    //    if (puerta != null)
    //    {
    //        puerta.SetActive(false);
    //    }
    //}
    public Animator palancaAnimator;
    private bool palancaActivada = false;

    public GameObject puertaDesactivar;
    public GameObject puertaActivar;

    public Camera camaraPrincipal;
    public Camera camaraSecundaria;

    public float tiempoEsperaCamaraSecundaria = 1.0f;
    public float tiempoEsperaPuertas = 1.5f;
    public float tiempoEsperaRestaurarCamaraPrincipal = 4.0f;

    public TextMeshProUGUI textoInteraccion;
    private bool mostrarTexto = false;

    void Update()
    {
        if (mostrarTexto && Input.GetKeyDown(KeyCode.E))
        {
            // Invertir el estado de la palanca al presionar la tecla "E"
            palancaActivada = !palancaActivada;

            // Actualizar el parámetro del Animator según el estado de la palanca
            palancaAnimator.SetBool("PalancaActivada", palancaActivada);

            // Activar o desactivar las puertas según el estado de la palanca
            if (palancaActivada)
            {
                ActivarPuerta(puertaActivar);
                DesactivarPuerta(puertaDesactivar);

                // Iniciar la secuencia de cámaras y puertas
                Invoke("ActivarCamaraSecundaria", tiempoEsperaCamaraSecundaria);
            }
            else
            {
                ActivarPuerta(puertaDesactivar);
                DesactivarPuerta(puertaActivar);
            }
        }
    }

    private void ActivarCamaraSecundaria()
    {
        camaraPrincipal.enabled = false;
        camaraSecundaria.enabled = true;

        // Iniciar la secuencia de puertas después de cierto tiempo
        Invoke("ControlarPuertas", tiempoEsperaPuertas);
    }

    private void ControlarPuertas()
    { // Desactivar la puerta anterior después de cierto tiempo
        Invoke("DesactivarPuertaAnterior", tiempoEsperaPuertas + 1.5f);

        // Activar la nueva puerta después de cierto tiempo
        Invoke("ActivarNuevaPuerta", tiempoEsperaPuertas + 2.5f);

        // Restaurar la cámara principal después de cierto tiempo
        Invoke("RestaurarCamaraPrincipal", tiempoEsperaRestaurarCamaraPrincipal);
    }

    private void DesactivarPuertaAnterior()
    {
        DesactivarPuerta(puertaDesactivar);
    }

    private void ActivarNuevaPuerta()
    {
        ActivarPuerta(puertaActivar);
    }

    private void RestaurarCamaraPrincipal()
    {
        camaraSecundaria.enabled = false;
        camaraPrincipal.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PJ"))
        {
            Debug.Log("Player cerca de la palanca");
            mostrarTexto = true;
            textoInteraccion.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PJ"))
        {
            mostrarTexto = false;
            textoInteraccion.gameObject.SetActive(false);
        }
    }

    // Método para activar una puerta
    private void ActivarPuerta(GameObject puerta)
    {
        if (puerta != null)
        {
            puerta.SetActive(true);
        }
    }

    // Método para desactivar una puerta
    private void DesactivarPuerta(GameObject puerta)
    {
        if (puerta != null)
        {
            puerta.SetActive(false);
        }
    }


    //public Animator palancaAnimator;
    //private bool palancaActivada = false;

    //public GameObject puertaDesactivar;
    //public GameObject puertaActivar;

    //public Camera camaraPrincipal;
    //public Camera camaraSecundaria;

    //public float tiempoEsperaCamaraSecundaria = 1.5f;
    //public float tiempoEsperaPuertas = 2f;
    //public float tiempoEsperaRestaurarCamaraPrincipal = 4.0f;

    //public TextMeshProUGUI textoInteraccion;
    //private bool mostrarTexto = false;

    //void Update()
    //{
    //    if (mostrarTexto && Input.GetKeyDown(KeyCode.E))
    //    {
    //        palancaActivada = !palancaActivada;
    //        palancaAnimator.SetBool("PalancaActivada", palancaActivada);

    //        if (palancaActivada)
    //        {
    //            ActivarPuerta(puertaActivar);
    //            DesactivarPuerta(puertaDesactivar);

    //            StartCoroutine(ActivarCamaraSecundaria());
    //        }
    //        else
    //        {
    //            ActivarPuerta(puertaDesactivar);
    //            DesactivarPuerta(puertaActivar);
    //        }
    //    }
    //}

    //private IEnumerator ActivarCamaraSecundaria()
    //{
    //    camaraPrincipal.enabled = false;
    //    camaraSecundaria.enabled = true;

    //    yield return new WaitForSeconds(tiempoEsperaCamaraSecundaria);

    //    DesactivarPuertaAnterior();
    //    yield return new WaitForSeconds(tiempoEsperaPuertas);
    //    ActivarNuevaPuerta();
    //    yield return new WaitForSeconds(tiempoEsperaRestaurarCamaraPrincipal);

    //    camaraSecundaria.enabled = false;
    //    camaraPrincipal.enabled = true;
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("PJ"))
    //    {
    //        Debug.Log("Player cerca de la palanca");
    //        mostrarTexto = true;
    //        textoInteraccion.gameObject.SetActive(true);
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("PJ"))
    //    {
    //        mostrarTexto = false;
    //        textoInteraccion.gameObject.SetActive(false);
    //    }
    //}

    //private void ActivarPuerta(GameObject puerta)
    //{
    //    if (puerta != null)
    //    {
    //        puerta.SetActive(true);
    //    }
    //}

    //private void DesactivarPuerta(GameObject puerta)
    //{
    //    if (puerta != null)
    //    {
    //        puerta.SetActive(false);
    //    }
    //}

    //private void DesactivarPuertaAnterior()
    //{
    //    DesactivarPuerta(puertaDesactivar);
    //}

    //private void ActivarNuevaPuerta()
    //{
    //    ActivarPuerta(puertaActivar);
    //}
}