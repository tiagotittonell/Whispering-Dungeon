using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvaPersonajeMuerto : MonoBehaviour
{
    public TextMeshProUGUI[] interactTexts; // Asigna tus objetos TextMeshPro al inspector
    public KeyCode interactKey = KeyCode.E;

    private bool isInRange = false;
    private int currentTextIndex = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PJ"))
        {
            isInRange = true;
            UpdateTextVisibility();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PJ"))
        {
            isInRange = false;
            interactTexts[currentTextIndex].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(interactKey))
        {
            ToggleTextMeshVisibility();
        }
    }

    void ToggleTextMeshVisibility()
    {
        interactTexts[currentTextIndex].gameObject.SetActive(false);

        // Cambia al siguiente TextMeshPro
        currentTextIndex = (currentTextIndex + 1) % interactTexts.Length;

        UpdateTextVisibility();
    }

    void UpdateTextVisibility()
    {
        for (int i = 0; i < interactTexts.Length; i++)
        {
            interactTexts[i].gameObject.SetActive(i == currentTextIndex);
        }
       
    }
    //public List<string> historias;
    //public List<TextMeshProUGUI> textMeshes;

    //public TextMeshProUGUI textoInteraccion;
    //private bool mostrarTexto = false;
    //private int indiceActual = 0;

    //void Update()
    //{
    //    if (mostrarTexto && Input.GetKeyDown(KeyCode.E))
    //    {
    //        MostrarSiguienteTexto();
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("PJ"))
    //    {
    //        Debug.Log("Player cerca de la palanca");
    //        mostrarTexto = true;
    //        textoInteraccion.gameObject.SetActive(true);
    //        // Mostrar el primer texto al entrar en la zona
    //        MostrarSiguienteTexto();
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

    //void MostrarSiguienteTexto()
    //{
    //    if (indiceActual < historias.Count)
    //    {
    //        // Desactivar el texto actual si existe y está dentro del rango
    //        if (indiceActual >= 0 && indiceActual < textMeshes.Count)
    //        {
    //            textMeshes[indiceActual].gameObject.SetActive(false);
    //        }

    //        // Incrementar el índice
    //        indiceActual++;

    //        // Verificar si hay más textos para mostrar
    //        if (indiceActual < historias.Count)
    //        {
    //            // Mostrar el siguiente texto si existe y está dentro del rango
    //            if (indiceActual >= 0 && indiceActual < textMeshes.Count)
    //            {
    //                textMeshes[indiceActual].gameObject.SetActive(true);
    //            }
    //        }
    //    }
    //}

    //public TextMeshProUGUI textoInteraccion;
    //private bool mostrarTexto = false;
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (mostrarTexto && Input.GetKeyDown(KeyCode.E))
    //    {


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
}
