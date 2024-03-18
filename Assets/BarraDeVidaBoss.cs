using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaBoss : MonoBehaviour
{

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();  
    }

    public void CambiarDeVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima;
    }

    public void CambiarVidaActual(float cantidadVida)
    {
        slider.value = cantidadVida;
        
    }

    public void InicializarBarraDeVida(float cantidadVida)
    {
        CambiarDeVidaMaxima(cantidadVida);
        CambiarVidaActual(cantidadVida);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
