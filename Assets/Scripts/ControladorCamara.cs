using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ControladorCamara : MonoBehaviour
{
    public Camera camaraPrincipal;
    public Camera camaraSecundaria;

    void Start()
    {
        
        camaraSecundaria.depth = camaraPrincipal.depth + 1;
    }
}
