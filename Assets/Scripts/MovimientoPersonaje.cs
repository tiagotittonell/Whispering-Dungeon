using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows;


public class MovimientoPersonaje : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider;

    // Efecto daño
    [SerializeField] private Vector2 velocidadRebote;
    [SerializeField] private bool damage_;
    [SerializeField] private int empuje;

    // Movimiento
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    [Range(0, 0.3f)][SerializeField] private float suavizadoMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    // Salto
    public float fuerzaSalto = 10f;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimensionCaja = new Vector2(0.5f, 0.1f);
    private bool enSuelo;
    private bool salto = false;

    // Dash
    [SerializeField] private float velocidadDash;
    [SerializeField] private float tiempoDash;
    private bool puedeHacerDash = true;
    public bool sePuedeMover = true;
    [SerializeField] private TrailRenderer trailRender;

    // Deslizamiento pared
    [SerializeField] private Transform controladorPared;
    [SerializeField] private Vector2 dimensionCajaPared = new Vector2(0.5f, 1f);
    private bool enPared;
    private bool deslizando;
    [SerializeField] private float velocidadDeslizar;
    [SerializeField] private float fuerzaSaltoParedX;
    [SerializeField] private float fuerzaSaltoParedY;
    [SerializeField] private float tiempoSaltoPared;
    private bool saltandoPared;

    // Subir escaleras
    [SerializeField] private float velocidadEscalar;
    private float gravedadInicial;
    private bool escalando;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        gravedadInicial = rb.gravityScale;
    }

    private void Update()
    {
        DetectarEntrada();
        ActualizarAnimaciones();
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionCaja, 0f, queEsSuelo);
        enPared = Physics2D.OverlapBox(controladorPared.position, dimensionCajaPared, 0f, queEsSuelo);

        if (sePuedeMover)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime);
        }

        if (deslizando)
        {
            rb.velocity = new Vector2(rb.velocity.x, -velocidadDeslizar);
        }

        salto = false;

        Escalar();
    }

    private void DetectarEntrada()
    {
        movimientoHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;

        if (UnityEngine.Input.GetButtonDown("Jump") && enSuelo)
        {
            salto = true;
        }

        if (!enSuelo && enPared && UnityEngine.Input.GetAxisRaw("Horizontal") != 0)
        {
            deslizando = true;
        }
        else
        {
            deslizando = false;
        }

        if (UnityEngine.Input.GetKeyDown(KeyCode.C) && puedeHacerDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void ActualizarAnimaciones()
    {
        anim.SetFloat("VelocidadX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("VelocidadY", rb.velocity.y);
        anim.SetBool("enSuelo", enSuelo);
        anim.SetBool("Deslizando", deslizando);
        anim.SetBool("Escalar", escalando);
    }

    private void Mover(float mover)
    {
        if (!saltandoPared)
        {
            Vector3 velocidadObjetivo = new Vector2(mover, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);
        }

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (salto && enSuelo && !deslizando)
        {
            Salto();
        }
        if (salto && enPared && deslizando)
        {
            SaltoPared();
        }
    }

    private void Salto()
    {
        rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
        enSuelo = false;
    }

    private void SaltoPared()
    {
        enPared = false;
        rb.velocity = new Vector2(fuerzaSaltoParedX * -UnityEngine.Input.GetAxisRaw("Horizontal"), fuerzaSaltoParedY);
        StartCoroutine(CambioSaltoPared());
    }

    private IEnumerator CambioSaltoPared()
    {
        saltandoPared = true;
        yield return new WaitForSeconds(tiempoSaltoPared);
        saltandoPared = false;
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private IEnumerator Dash()
    {
        sePuedeMover = false;
        puedeHacerDash = false;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
        anim.SetTrigger("Dash");
        trailRender.emitting = true;

        yield return new WaitForSeconds(tiempoDash);

        sePuedeMover = true;
        puedeHacerDash = true;
        rb.gravityScale = gravedadInicial;
        trailRender.emitting = false;
    }

    private void Escalar()
    {
        if ((UnityEngine.Input.GetAxisRaw("Vertical") != 0 || escalando) && boxCollider.IsTouchingLayers(LayerMask.GetMask("ESCALERAS")))
        {
            Vector2 velocidadSubida = new Vector2(rb.velocity.x, UnityEngine.Input.GetAxisRaw("Vertical") * velocidadEscalar);
            rb.velocity = velocidadSubida;
            rb.gravityScale = 0;
            escalando = true;
        }
        else
        {
            rb.gravityScale = gravedadInicial;
            escalando = false;
        }

        if (enSuelo)
        {
            escalando = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionCaja);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorPared.position, dimensionCajaPared);
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //private Rigidbody2D rb;

    ////EFECTO DAÑO
    //[SerializeField] private Vector2 velocidadRebote;
    //[SerializeField] private bool damage_;
    //[SerializeField] private int empuje;

    ////MOVIMIENTO
    //private Vector2 input;
    //private float movimientoHorizontal = 0f;
    //[SerializeField] private float velocidadDeMovimiento;
    //[Range(0, 0.3f)]
    //[SerializeField] private float suavizadoMovimiento;
    //private Vector3 velocidad = Vector3.zero;
    //private bool mirandoDerecha = true;

    ////SALTO
    //public float fuerzaSalto;
    //[SerializeField] private LayerMask queEsSuelo;
    //[SerializeField] private Transform controladorSuelo;
    //[SerializeField] private Vector3 dimensionCaja;
    //[SerializeField] private bool enSuelo;
    //private bool salto = false;

    ////DASH
    //[SerializeField] private float velocidadDash;
    //[SerializeField] private float tiempoDash;
    //private bool puedeHacerDash = true;
    //public bool sePuedeMover = true;
    //[SerializeField] private TrailRenderer trailRender;

    ////DESLIZARSE PARED
    //[SerializeField] Transform controladorPared;
    //[SerializeField] private Vector3 dimensionCajaPared;
    //private bool enPared;
    //private bool deslizando;
    //[SerializeField] private float velocidadDeslizar;
    //[SerializeField] private float fuerzaSaltoParedX;
    //[SerializeField] private float fuerzaSaltoParedY;
    //[SerializeField] private float tiempoSaltoPared;
    //private bool saltandoPared;

    ////ANIMACION
    //private Animator anim;

    ////SUBIR ESCALERAS
    //[SerializeField] private float velocidadEscalar;
    //private BoxCollider2D boxCollider;
    //private float gravedadInicial;
    //private bool escalando;

    //// Start is called before the first frame update
    //private void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();
    //    boxCollider = GetComponent<BoxCollider2D>();
    //    gravedadInicial = rb.gravityScale;
    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //    input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
    //    input.y = UnityEngine.Input.GetAxisRaw("Vertical");
    //    movimientoHorizontal = input.x * velocidadDeMovimiento;

    //    anim.SetFloat("VelocidadY", rb.velocity.y);
    //    anim.SetFloat("VelocidadX", Mathf.Abs(rb.velocity.x));

    //    if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
    //    {
    //        anim.SetFloat("VelocidadY", Mathf.Sign(rb.velocity.y));
    //    }
    //    else
    //    {
    //        anim.SetFloat("VelocidadY", 0);
    //    }

    //    if (UnityEngine.Input.GetButtonDown("Jump"))
    //    {
    //        salto = true;
    //    }

    //    if (!enSuelo && enPared && input.x != 0)
    //    {
    //        deslizando = true;
    //    }
    //    else
    //    {
    //        deslizando = false;
    //    }

    //    if (enSuelo)
    //    {
    //        escalando = false;
    //    }

    //    anim.SetBool("Deslizando", deslizando);
    //}

    //private void FixedUpdate()
    //{
    //    enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionCaja, 0f, queEsSuelo);
    //    anim.SetBool("enSuelo", enSuelo);

    //    enPared = Physics2D.OverlapBox(controladorPared.position, dimensionCajaPared, 0f, queEsSuelo);

    //    if (sePuedeMover)
    //    {
    //        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
    //    }

    //    Escalar();
    //    salto = false;

    //    if (deslizando)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.y, Mathf.Clamp(rb.velocity.y, -velocidadDeslizar, float.MaxValue));
    //    }
    //}

    //private void Mover(float mover, bool saltar)
    //{
    //    if (!saltandoPared)
    //    {
    //        Vector3 velocidadObjetivo = new Vector2(mover, rb.velocity.y);
    //        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);
    //    }

    //    if (mover > 0 && !mirandoDerecha)
    //    {
    //        Girar();
    //    }
    //    else if (mover < 0 && mirandoDerecha)
    //    {
    //        Girar();
    //    }

    //    if (saltar && enSuelo && !deslizando)
    //    {
    //        Salto();
    //    }
    //    if (saltar && enPared && deslizando)
    //    {
    //        SaltoPared();
    //    }

    //    if (UnityEngine.Input.GetKeyDown(KeyCode.C) && puedeHacerDash)
    //    {
    //        StartCoroutine(Dash());
    //    }
    //}

    //private void Salto()
    //{
    //    enSuelo = false;
    //    rb.AddForce(new Vector2(0f, fuerzaSalto));
    //}

    //private void Escalar()
    //{
    //    if ((input.y != 0 || escalando) && (boxCollider.IsTouchingLayers(LayerMask.GetMask("ESCALERAS"))))
    //    {
    //        Vector2 velocidadSubida = new Vector2(rb.velocity.x, input.y * velocidadEscalar);
    //        rb.velocity = velocidadSubida;
    //        rb.gravityScale = 0;
    //        escalando = true;
    //    }
    //    else
    //    {
    //        rb.gravityScale = gravedadInicial;
    //        escalando = false;
    //    }
    //    if (enSuelo)
    //    {
    //        escalando = false;
    //    }
    //    anim.SetBool("Escalar", escalando);
    //}

    //private void SaltoPared()
    //{
    //    enPared = false;
    //    rb.velocity = new Vector2(fuerzaSaltoParedX * -input.x, fuerzaSaltoParedY);
    //    StartCoroutine(CambioSaltoPared());
    //}

    //IEnumerator CambioSaltoPared()
    //{
    //    saltandoPared = true;
    //    yield return new WaitForSeconds(tiempoSaltoPared);
    //    saltandoPared = false;
    //}

    //private void Girar()
    //{
    //    mirandoDerecha = !mirandoDerecha;
    //    Vector3 escala = transform.localScale;
    //    escala.x *= -1;
    //    transform.localScale = escala;
    //}

    //private IEnumerator Dash()
    //{
    //    sePuedeMover = false;
    //    puedeHacerDash = false;
    //    rb.gravityScale = 0;
    //    rb.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
    //    anim.SetTrigger("Dash");
    //    trailRender.emitting = true;

    //    yield return new WaitForSeconds(tiempoDash);

    //    sePuedeMover = true;
    //    puedeHacerDash = true;
    //    rb.gravityScale = gravedadInicial;
    //    trailRender.emitting = false;
    //}

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    // Suponiendo que los enemigos tienen la etiqueta "Enemigo"
    //    if (collision.gameObject.CompareTag("Enemigo"))
    //    {
    //        // Calcula la dirección del empuje basado en la posición del enemigo
    //        Vector2 direccionEmpuje = (transform.position - collision.transform.position).normalized;
    //        GetComponent<VidaPersonaje>().TomarDañoBarra(10f, direccionEmpuje); // Ajusta el daño según sea necesario
    //    }
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(controladorSuelo.position, dimensionCaja);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(controladorPared.position, dimensionCajaPared);
    //}
    /////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////

    // private Rigidbody2D rb;
    // private Animator anim;
    // private BoxCollider2D boxCollider;

    // //Efecto daño
    //[SerializeField] private Vector2 velocidadRebote;
    // [SerializeField] private bool damage_;
    // [SerializeField] private int empuje;

    // //Movimiento
    //  private float movimientoHorizontal = 0f;
    // [SerializeField] private float velocidadDeMovimiento;
    // [Range(0, 0.3f)][SerializeField] private float suavizadoMovimiento;
    // private Vector3 velocidad = Vector3.zero;
    // private bool mirandoDerecha = true;

    // //Salto
    //  public float fuerzaSalto = 10f;
    // [SerializeField] private LayerMask queEsSuelo;
    // [SerializeField] private Transform controladorSuelo;
    // [SerializeField] private Vector2 dimensionCaja = new Vector2(0.5f, 0.1f);
    // private bool enSuelo;
    // private bool salto = false;

    // //Dash
    //[SerializeField] private float velocidadDash;
    // [SerializeField] private float tiempoDash;
    // private bool puedeHacerDash = true;
    // public bool sePuedeMover = true;
    // [SerializeField] private TrailRenderer trailRender;

    // //Deslizamiento pared
    //[SerializeField] private Transform controladorPared;
    // [SerializeField] private Vector2 dimensionCajaPared = new Vector2(0.5f, 1f);
    // private bool enPared;
    // private bool deslizando;
    // [SerializeField] private float velocidadDeslizar;
    // [SerializeField] private float fuerzaSaltoParedX;
    // [SerializeField] private float fuerzaSaltoParedY;
    // [SerializeField] private float tiempoSaltoPared;
    // private bool saltandoPared;

    // //Subir escaleras
    //[SerializeField] private float velocidadEscalar;
    // private float gravedadInicial;
    // private bool escalando;

    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     anim = GetComponent<Animator>();
    //     boxCollider = GetComponent<BoxCollider2D>();
    //     gravedadInicial = rb.gravityScale;
    // }

    // private void Update()
    // {
    //     DetectarEntrada();
    //     ActualizarAnimaciones();
    // }

    // private void FixedUpdate()
    // {
    //     enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionCaja, 0f, queEsSuelo);
    //     enPared = Physics2D.OverlapBox(controladorPared.position, dimensionCajaPared, 0f, queEsSuelo);

    //     if (sePuedeMover)
    //     {
    //         Mover(movimientoHorizontal * Time.fixedDeltaTime);
    //     }

    //     if (deslizando)
    //     {
    //         rb.velocity = new Vector2(rb.velocity.x, -velocidadDeslizar);
    //     }

    //     salto = false;
    // }

    // private void DetectarEntrada()
    // {
    //     movimientoHorizontal = UnityEngine.Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;

    //     if (UnityEngine.Input.GetButtonDown("Jump") && enSuelo)
    //     {
    //         salto = true;
    //     }

    //     if (!enSuelo && enPared && UnityEngine.Input.GetAxisRaw("Horizontal") != 0)
    //     {
    //         deslizando = true;
    //     }
    //     else
    //     {
    //         deslizando = false;
    //     }

    //     if (UnityEngine.Input.GetKeyDown(KeyCode.C) && puedeHacerDash)
    //     {
    //         StartCoroutine(Dash());
    //     }
    // }

    // private void ActualizarAnimaciones()
    // {
    //     anim.SetFloat("VelocidadX", Mathf.Abs(rb.velocity.x));
    //     anim.SetFloat("VelocidadY", rb.velocity.y);
    //     anim.SetBool("enSuelo", enSuelo);
    //     anim.SetBool("Deslizando", deslizando);
    // }

    // private void Mover(float mover)
    // {
    //     Vector3 velocidadObjetivo = new Vector2(mover, rb.velocity.y);
    //     rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);

    //     if (mover > 0 && !mirandoDerecha)
    //     {
    //         Girar();
    //     }
    //     else if (mover < 0 && mirandoDerecha)
    //     {
    //         Girar();
    //     }

    //     if (salto && enSuelo)
    //     {
    //         Salto();
    //     }
    // }


    // private void Salto()
    // {
    //     rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);
    //     enSuelo = false;
    // }

    // private void Girar()
    // {
    //     mirandoDerecha = !mirandoDerecha;
    //     Vector3 escala = transform.localScale;
    //     escala.x *= -1;
    //     transform.localScale = escala;
    // }

    // private IEnumerator Dash()
    // {
    //     sePuedeMover = false;
    //     puedeHacerDash = false;
    //     rb.gravityScale = 0;
    //     rb.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
    //     anim.SetTrigger("Dash");
    //     trailRender.emitting = true;

    //     yield return new WaitForSeconds(tiempoDash);

    //     sePuedeMover = true;
    //     puedeHacerDash = true;
    //     rb.gravityScale = gravedadInicial;
    //     trailRender.emitting = false;
    // }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireCube(controladorSuelo.position, dimensionCaja);
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireCube(controladorPared.position, dimensionCajaPared);
}
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    //private Rigidbody2D rb;

    ////EFECTO DAÑO


    //[SerializeField] private Vector2 velocidadRebote;


    //[SerializeField] private bool damage_;
    //[SerializeField] private int empuje;

    ////MOVIMIENTO

    //private Vector2 input;

    //private float movimientoHorizontal = 0f;

    //[SerializeField] private float velocidadDeMovimiento;

    //[Range(0, 0.3f)]
    //[SerializeField] private float suavizadoMovimiento;

    //private Vector3 velocidad = Vector3.zero;

    //private bool mirandoDerecha = true;

    ////SALTO
    //public float fuerzaSalto;

    //[SerializeField] private LayerMask queEsSuelo;

    //[SerializeField] private Transform controladorSuelo;

    //[SerializeField] private Vector3 dimensionCaja;


    //[SerializeField]
    //private bool enSuelo;

    //private bool salto = false;

    ////DASH

    //[SerializeField] private float velocidadDash;

    //[SerializeField] private float tiempoDash;

    //private bool puedeHacerDash = true;

    //public bool sePuedeMover = true;
    //[SerializeField] private TrailRenderer trailRender;



    ////DESLIZARSE PARED

    //[SerializeField] Transform controladorPared;
    //[SerializeField] private Vector3 dimensionCajaPared;
    //private bool enPared;
    //private bool deslizando;
    //[SerializeField] private float velocidadDeslizar;
    //[SerializeField] private float fuerzaSaltoParedX;
    //[SerializeField] private float fuerzaSaltoParedY;
    //[SerializeField] private float tiempoSaltoPared;

    //private bool saltandoPared;

    ////ANIMACION
    //private Animator anim;


    ////SUBIR ESCALERAS
    //[SerializeField] private float velocidadEscalar;

    //private BoxCollider2D boxCollider;

    //private float gravedadInicial;

    //private bool escalando;

    //// Start is called before the first frame update

    //private void Start()
    //{
    //    //jugador = MovimientoPersonaje();
    //    rb = GetComponent<Rigidbody2D>();
    //    anim = GetComponent<Animator>();
    //    boxCollider = GetComponent<BoxCollider2D>();
    //    gravedadInicial = rb.gravityScale;

    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //    input.x = UnityEngine.Input.GetAxisRaw("Horizontal");
    //    input.y = UnityEngine.Input.GetAxisRaw("Vertical");

    //    movimientoHorizontal = input.x * velocidadDeMovimiento;
    //    //anim.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

    //    anim.SetFloat("VelocidadY", rb.velocity.y);
    //    anim.SetFloat("VelocidadX", Mathf.Abs(rb.velocity.x));

    //    if (Mathf.Abs(rb.velocity.y) > Mathf.Epsilon)
    //    {
    //        anim.SetFloat("VelocidadY", Mathf.Sign(rb.velocity.y));
    //    }
    //    else
    //    {
    //        anim.SetFloat("VelocidadY", 0);
    //    }




    //    if (UnityEngine.Input.GetButtonDown("Jump"))
    //    {
    //        salto = true;

    //    }

    //    if (!enSuelo && enPared && input.x != 0)
    //    {
    //        deslizando = true;
    //    }
    //    else
    //    {
    //        deslizando = false;
    //    }
    //    if (enSuelo)
    //    {
    //        escalando = false;
    //    }
    //    anim.SetBool("Deslizando", deslizando);


    //}

    //private void FixedUpdate()
    //{
    //    enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionCaja, 0f, queEsSuelo);
    //    anim.SetBool("enSuelo", enSuelo);


    //    enPared = Physics2D.OverlapBox(controladorPared.position, dimensionCajaPared, 0f, queEsSuelo);
    //    //Mover

    //    if (sePuedeMover)
    //    {
    //        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
    //    }

    //    Escalar();
    //    salto = false;

    //    if (deslizando)
    //    {
    //        rb.velocity = new Vector2(rb.velocity.y, Mathf.Clamp(rb.velocity.y, -velocidadDeslizar, float.MaxValue));
    //    }

    //}

    //public void Damage()
    //{
    //    if (damage_)
    //    {
    //        transform.Translate(Vector3.right * empuje * Time.deltaTime, Space.World);


    //    }
    //}


    //private void Mover(float mover, bool saltar)
    //{
    //    if (!saltandoPared)
    //    {
    //        Vector3 velocidadObjetivo = new Vector2(mover, rb.velocity.y);

    //        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo, ref velocidad, suavizadoMovimiento);


    //    }


    //    if (mover > 0 && !mirandoDerecha)
    //    {
    //        //girar

    //        Girar();
    //    }
    //    else if (mover < 0 && mirandoDerecha)
    //    {
    //        Girar();
    //    }

    //    if (saltar && enSuelo && !deslizando)
    //    {
    //        Salto();
    //    }
    //    if (saltar && enPared && deslizando)
    //    {
    //        SaltoPared();
    //    }

    //    if (UnityEngine.Input.GetKeyDown(KeyCode.C) && puedeHacerDash)
    //    {
    //        StartCoroutine(Dash());
    //    }
    //}
    //private void Salto()
    //{
    //    enSuelo = false;

    //    rb.AddForce(new Vector2(0f, fuerzaSalto));

    //}

    //private void Escalar()
    //{
    //    if ((input.y != 0 || escalando) && (boxCollider.IsTouchingLayers(LayerMask.GetMask("ESCALERAS"))))

    //    {
    //        Vector2 velocidadSubida = new Vector2(rb.velocity.x, input.y * velocidadEscalar);
    //        rb.velocity = velocidadSubida;
    //        rb.gravityScale = 0;
    //        escalando = true;
    //    }
    //    else
    //    {
    //        rb.gravityScale = gravedadInicial;
    //        escalando = false;
    //    }
    //    if (enSuelo)
    //    {
    //        escalando = false;
    //    }
    //    anim.SetBool("Escalar", escalando);

    //}
    //public void Rebote(Vector2 puntoGolpe)
    //{
    //    rb.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    //}


    //private void SaltoPared()
    //{
    //    enPared = false;

    //    rb.velocity = new Vector2(fuerzaSaltoParedX * -input.x, fuerzaSaltoParedY);
    //    StartCoroutine(CambioSaltoPared());


    //}

    //IEnumerator CambioSaltoPared()
    //{
    //    saltandoPared = true;
    //    yield return new WaitForSeconds(tiempoSaltoPared);
    //    saltandoPared = false;
    //}
    //private void Girar()
    //{
    //    mirandoDerecha = !mirandoDerecha;

    //    Vector3 escala = transform.localScale;

    //    escala.x *= -1;

    //    transform.localScale = escala;
    //}

    //private IEnumerator Dash()
    //{
    //    sePuedeMover = false;
    //    puedeHacerDash = false;
    //    rb.gravityScale = 0;
    //    rb.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
    //    anim.SetTrigger("Dash");
    //    trailRender.emitting = true;

    //    yield return new WaitForSeconds(tiempoDash);

    //    sePuedeMover = true;
    //    puedeHacerDash = true;
    //    rb.gravityScale = gravedadInicial;
    //    trailRender.emitting = false;
    //}

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(controladorSuelo.position, dimensionCaja);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(controladorPared.position, dimensionCajaPared);
    //}

