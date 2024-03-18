using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControladorLVL3 : MonoBehaviour
{
    private void Start()
    {
        // Suscribirse al evento de muerte del enemigo
        Enemigo enemigo = FindObjectOfType<Enemigo>(); // Asegúrate de que solo haya un enemigo en tu escena o encuentra una forma adecuada de manejar múltiples enemigos
        if (enemigo != null)
        {
            enemigo.OnMuerte += CargarPantallaVictoria;
        }
    }

    private void OnDestroy()
    {
        // Asegúrate de desuscribirte del evento cuando el objeto se destruye para evitar fugas de memoria
        Enemigo enemigo = FindObjectOfType<Enemigo>();
        if (enemigo != null)
        {
            enemigo.OnMuerte -= CargarPantallaVictoria;
        }
    }

    private void CargarPantallaVictoria()
    {
        // Cargar la escena "PantallaVictoria" después de 2 segundos
        Invoke("CargarEscenaVictoria", 2f);
    }

    private void CargarEscenaVictoria()
    {
        SceneManager.LoadScene("PantallaWin");
    }
}
