using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorPantalla : MonoBehaviour
{
   
    public void VolverAJugar()

    { 
        SceneManager.LoadScene("Nivel 1");

    }
    public void Menu()

    {
        SceneManager.LoadScene("Menu");

    }
    public void SalirDelJuego()
    {
        // Salir de la aplicación
        Application.Quit();
    }

}
