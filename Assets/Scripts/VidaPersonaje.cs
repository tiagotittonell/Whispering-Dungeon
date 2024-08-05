using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPersonaje : MonoBehaviour
{
    [SerializeField] private float vida;
    [SerializeField] private BarraDeVida barraVida;
    private MovimientoPersonaje movimientoJugador;
    [SerializeField] private float tiempoPerdidaControl = 2f; 
    private Animator animator;
    [SerializeField] private float vidaMaxima;
    [SerializeField] private float fuerzaEmpuje = 10f; 
    private Rigidbody2D rb;
    private bool estaMuerto = false;

    private void Start()
    {
        vida = vidaMaxima;
        barraVida.InicializarBarraDeVida(vida);
        movimientoJugador = GetComponent<MovimientoPersonaje>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

   

    private IEnumerator PerderControl()
    {
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }

    private IEnumerator Muerte()
    {
        
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("PantallaDerrota");
    }

    public void TomarDañoBarra(float Daño)
    {
        //if (estaMuerto) return; 

        vida -= Daño;
        //animator.SetTrigger("Hurt");
        barraVida.CambiarVidaActual(vida);
        if (vida <= 0)
        {
            //estaMuerto = true;
            movimientoJugador.sePuedeMover = false;
            movimientoJugador.enabled = false;
            animator.SetTrigger("Muerte");
            StartCoroutine(Muerte());
        }
        else
        {
            StartCoroutine(PerderControl());
        }
    }
    //    [SerializeField] private float vida;
    //    [SerializeField] private BarraDeVida barraVida;
    //    private MovimientoPersonaje movimientoJugador;
    //    [SerializeField] private float tiempoPerdidaControl = 2f; // Tiempo en segundos
    //    private Animator animator;
    //    [SerializeField] private float vidaMaxima;
    //    [SerializeField] private float fuerzaEmpuje = 10f; // Fuerza del empuje hacia atrás
    //    private Rigidbody2D rb;

    //    private void Start()
    //    {
    //        vida = vidaMaxima;
    //        barraVida.InicializarBarraDeVida(vida);
    //        movimientoJugador = GetComponent<MovimientoPersonaje>();
    //        animator = GetComponent<Animator>();
    //        rb = GetComponent<Rigidbody2D>();
    //    }

    //    private IEnumerator PerderControl()
    //    {
    //        movimientoJugador.sePuedeMover = false;
    //        yield return new WaitForSeconds(tiempoPerdidaControl);
    //        movimientoJugador.sePuedeMover = true;
    //    }

    //    private IEnumerator Muerte()
    //    {
    //        // Esperar dos segundos para permitir que se reproduzca la animación de muerte
    //        yield return new WaitForSeconds(2f);
    //        SceneManager.LoadScene("PantallaDerrota");
    //    }

    //    public void TomarDañoBarra(float Daño)
    //    {
    //        vida -= Daño;
    //        animator.SetTrigger("Hurt");
    //        barraVida.CambiarVidaActual(vida);
    //        if (vida <= 0)
    //        {
    //            movimientoJugador.sePuedeMover = false;
    //            movimientoJugador.enabled = false;
    //            animator.SetTrigger("Muerte");
    //            StartCoroutine(Muerte());
    //        }
    //        else
    //        {
    //            StartCoroutine(PerderControl());
    //        }
    //    }

    //[SerializeField] private float vida;
    //[SerializeField] private BarraDeVida barraVida;
    //private MovimientoPersonaje movimientoJugador;
    //[SerializeField] private float tiempoPerdidaControl = 2f; // Tiempo en segundos
    //private Animator animator;
    //[SerializeField] private float vidaMaxima;
    //[SerializeField] private float fuerzaEmpuje = 10f; // Fuerza del empuje hacia atrás
    //private Rigidbody2D rb;

    //private void Start()
    //{
    //    vida = vidaMaxima;
    //    barraVida.InicializarBarraDeVida(vida);
    //    movimientoJugador = GetComponent<MovimientoPersonaje>();
    //    animator = GetComponent<Animator>();
    //    rb = GetComponent<Rigidbody2D>();
    //}

    //public void TomarDañoBarra(float Daño, Vector2 direccionEmpuje)
    //{
    //    vida -= Daño;
    //    animator.SetTrigger("Hurt");
    //    barraVida.CambiarVidaActual(vida);
    //    if (vida <= 0)
    //    {
    //        movimientoJugador.sePuedeMover = false;
    //        movimientoJugador.enabled = false;
    //        animator.SetTrigger("Muerte");
    //        StartCoroutine(Muerte());
    //    }
    //    else
    //    {
    //        // Aplicar empuje
    //        rb.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
    //        StartCoroutine(PerderControl());
    //    }
    //}

    //private IEnumerator PerderControl()
    //{
    //    movimientoJugador.sePuedeMover = false;
    //    yield return new WaitForSeconds(tiempoPerdidaControl);
    //    movimientoJugador.sePuedeMover = true;
    //}

    //private IEnumerator Muerte()
    //{
    //    // Esperar dos segundos para permitir que se reproduzca la animación de muerte
    //    yield return new WaitForSeconds(5f);
    //    SceneManager.LoadScene("PantallaDerrota");
    //}

    //public void TomarDañoBarra(float Daño)
    //{
    //    vida -= Daño;
    //    animator.SetTrigger("Hurt");
    //    barraVida.CambiarVidaActual(vida);
    //    if (vida <= 0)
    //    {
    //        animator.SetTrigger("Muerte");
    //        movimientoJugador.sePuedeMover = false;
    //        movimientoJugador.enabled = false;
    //        StartCoroutine(Muerte());
    //    }
    //    else
    //    {
    //        StartCoroutine(PerderControl());
    //    }
    //}

    //[SerializeField] private float vida;
    //[SerializeField] private BarraDeVida barraVida;
    //private MovimientoPersonaje movimientoJugador;
    //[SerializeField] private float tiempoPerdidaControl = 2f; // Tiempo en segundos
    //private Animator animator;
    //[SerializeField] private float vidaMaxima;
    //[SerializeField] private float fuerzaEmpuje = 10f; // Fuerza del empuje hacia atrás

    //private Rigidbody2D rb;

    //private void Start()
    //{
    //    vida = vidaMaxima;
    //    barraVida.InicializarBarraDeVida(vida);
    //    movimientoJugador = GetComponent<MovimientoPersonaje>();
    //    animator = GetComponent<Animator>();
    //    rb = GetComponent<Rigidbody2D>();
    //}

    //public void TomarDañoBarra(float Daño, Vector2 direccionEmpuje)
    //{
    //    vida -= Daño;
    //    animator.SetTrigger("Hurt");
    //    barraVida.CambiarVidaActual(vida);
    //    if (vida <= 0)
    //    {
    //        movimientoJugador.sePuedeMover = false;
    //        movimientoJugador.enabled = false;
    //        animator.SetTrigger("Muerte");
    //        //Muerte();
    //    }
    //    else
    //    {
    //        // Aplicar empuje
    //        rb.AddForce(direccionEmpuje * fuerzaEmpuje, ForceMode2D.Impulse);
    //        StartCoroutine(PerderControl());
    //    }
    //}

    //private IEnumerator PerderControl()
    //{
    //    movimientoJugador.sePuedeMover = false;
    //    yield return new WaitForSeconds(tiempoPerdidaControl);
    //    movimientoJugador.sePuedeMover = true;
    //}

    //private void Muerte()
    //{
    //    Destroy(gameObject);
    //}



    //public void TomarDañoBarra(float Daño)
    //{
    //    vida -= Daño;
    //    animator.SetTrigger("Hurt");
    //    barraVida.CambiarVidaActual(vida);
    //    if (vida <= 0)
    //    {
    //        movimientoJugador.sePuedeMover = false;
    //        movimientoJugador.enabled = false;
    //        animator.SetTrigger("Muerte");
    //        Muerte();
    //    }
    //    else
    //    {
    //        StartCoroutine(PerderControl());
    //    }
    //}

    //public void TomarDaños(float daño, Vector2 posicion)
    //{
    //    vida -= daño;
    //    StartCoroutine(PerderControl());
    //}

    //private IEnumerator PerderControl()
    //{
    //    Debug.Log("Perdiendo control del personaje");
    //    movimientoJugador.sePuedeMover = false;
    //    yield return new WaitForSeconds(tiempoPerdidaControl);
    //    movimientoJugador.sePuedeMover = true;
    //    Debug.Log("Recuperando control del personaje");
    //}

    //private void Muerte()
    //{
    //    Destroy(gameObject);
    //}

    //[SerializeField] private float vida;
    //[SerializeField] private BarraDeVida barraVida;
    //private MovimientoPersonaje movimientoJugador;
    //[SerializeField] private float tiempoPerdidaControl;
    //private Animator animator;
    //[SerializeField] private float vidaMaxima;


    //private void Start()
    //{
    //    vida = vidaMaxima;
    //    barraVida.InicializarBarraDeVida(vida);
    //    movimientoJugador = GetComponent<MovimientoPersonaje>();
    //    animator = GetComponent<Animator>();
    //}
    ////public void TomarDaño(float daño)
    ////{
    ////    vida -= daño;





    ////}
    //public void TomarDañoBarra(float Daño)
    //{
    //    vida -= Daño;
    //    animator.SetTrigger("Hurt");
    //    barraVida.CambiarVidaActual(vida);
    //    if (vida <= 0)
    //    {
    //        movimientoJugador.sePuedeMover = false;
    //        movimientoJugador.enabled = false;
    //        animator.SetTrigger("Muerte");
    //        //Muerte();
    //    }

    //}
    //public void TomarDaños(float daño, Vector2 posicion)
    //{
    //    vida -= daño;
    //    //animator.SetTrigger("Golpe");
    //    //animator.SetTrigger("Hurt");
    //    //Jugador pierde el Control del personaje
    //    StartCoroutine(PerderControl());
    //    //movimientoJugador.Rebote(posicion);
    //}

    //private IEnumerator PerderControl()
    //{
    //    movimientoJugador.sePuedeMover = false;
    //    yield return new WaitForSeconds(tiempoPerdidaControl);
    //    movimientoJugador.sePuedeMover = true;
    //}
    //private void Muerte()
    //{


    //    Destroy(gameObject);
    //}
}
