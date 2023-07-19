using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class VidaPersonaje : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraVida;
    private MovimientoPersonaje movimientoJugador;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;


    private void Start()
    {
        barraVida.InicializarBarraDeVida(vida);
        movimientoJugador = GetComponent<MovimientoPersonaje>();
        animator = GetComponent<Animator>();
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;

        barraVida.CambiarVidaActual(vida);

        if (vida <= 0)

        {
           Muerte();

        }

    }
    public void TomarDaño(float daño, Vector2 posicion)
    {
        vida -= daño;
        animator.SetTrigger("Golpe");
        //Jugador pierde el Control del personaje
        StartCoroutine(PerderControl());
        movimientoJugador.Rebote(posicion);
    }

    private IEnumerator PerderControl()
    {
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }
    private void Muerte()
    {


        Destroy(gameObject);
    }
}
