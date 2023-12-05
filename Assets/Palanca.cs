using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Palanca : MonoBehaviour
{
    public Animator palancaAnimator;
    private bool palancaActivada = false;

    public GameObject puertaDesactivar;
    public GameObject puertaActivar;

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
            }
            else
            {
                ActivarPuerta(puertaDesactivar);
                DesactivarPuerta(puertaActivar);
            }
        }
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
}
