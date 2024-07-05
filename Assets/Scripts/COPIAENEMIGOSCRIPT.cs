using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COPIAENEMIGOSCRIPT : MonoBehaviour
{


    public int rutina;
    public float cronometro;
    public Animator ani;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool atacando;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango;
    public GameObject Hit;

    public Rigidbody2D rb;

    [SerializeField] private float vida;
    private bool estaMuerto = false; // Nueva variable para controlar el estado de muerte

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        target = GameObject.Find("PJ");
    }

    public void Comportamientos()
    {
        if (estaMuerto) return; // Si está muerto, no hacer nada más

        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)
        {
            ani.SetBool("Correr", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("Caminar", false);
                    break;

                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;

                case 2:

                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;

                        case 1:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                    }
                    ani.SetBool("Caminar", true);
                    break;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    ani.SetBool("Caminar", false);
                    ani.SetBool("Correr", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    ani.SetBool("Ataque", false);
                }
                else
                {
                    ani.SetBool("Caminar", false);
                    ani.SetBool("Correr", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    ani.SetBool("Ataque", false);
                }
            }
            else
            {
                if (!atacando)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    ani.SetBool("Caminar", false);
                    ani.SetBool("Correr", false);
                }
            }
        }
    }

    public void Final_Ani()
    {
        if (estaMuerto) return; // Si está muerto, no hacer nada más
        ani.SetBool("Ataque", false);
        atacando = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        if (estaMuerto) return; // Si está muerto, no hacer nada más
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponFalse()
    {
        if (estaMuerto) return; // Si está muerto, no hacer nada más
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Comportamientos();
    }

    public void TomarDaño(float daño)
    {
        if (estaMuerto) return; // Si está muerto, no puede recibir más daño

        vida -= daño;

        if (vida > 0)
        {
            ani.SetTrigger("Daño");
        }
        else
        {
            estaMuerto = true; // Marcar al enemigo como muerto
            ani.SetTrigger("Muerte");

            // Desactivar todos los comportamientos y colisionadores
            rb.velocity = Vector2.zero;
            rb.isKinematic = true; // Opcional: desactivar la física
            GetComponent<Collider2D>().enabled = false;
            rango.GetComponent<Collider2D>().enabled = false;
            Hit.GetComponent<Collider2D>().enabled = false;

            StartCoroutine(DestruirEnemigo());
        }
    }

    private IEnumerator DestruirEnemigo()
    {
        // Esperar el tiempo de la animación de muerte antes de destruir el objeto
        yield return new WaitForSeconds(ani.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }

    public void Muerte()
    {
        Destroy(gameObject);
    }

    //    public int rutina;
    //    public float cronometro;
    //    public Animator ani;
    //    public int direccion;
    //    public float speed_walk;
    //    public float speed_run;
    //    public GameObject target;
    //    public bool atacando;

    //    public float rango_vision;
    //    public float rango_ataque;
    //    public GameObject rango;
    //    public GameObject Hit;

    //    public Rigidbody2D rb;


    //    [SerializeField] private float vida;

    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        rb = GetComponent<Rigidbody2D>();
    //        ani = GetComponent<Animator>();
    //        target = GameObject.Find("PJ");
    //    }

    //    public void Comportamientos()
    //    {
    //        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)
    //        {
    //            ani.SetBool("Correr", false);
    //            cronometro += 1 * Time.deltaTime;
    //            if (cronometro >= 4)
    //            {
    //                rutina = Random.Range(0, 2);
    //                cronometro = 0;
    //            }
    //            switch (rutina)
    //            {
    //                case 0:
    //                    ani.SetBool("Caminar", false);
    //                    break;

    //                case 1:
    //                    direccion = Random.Range(0, 2);
    //                    rutina++;
    //                    break;

    //                case 2:

    //                    switch (direccion)
    //                    {
    //                        case 0:
    //                            transform.rotation = Quaternion.Euler(0, 0, 0);
    //                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
    //                            break;

    //                        case 1:
    //                            transform.rotation = Quaternion.Euler(0, 180, 0);
    //                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
    //                            break;
    //                    }
    //                    ani.SetBool("Caminar", true);
    //                    break;
    //            }
    //        }
    //        else
    //        {
    //            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)
    //            {
    //                if (transform.position.x < target.transform.position.x)
    //                {
    //                    ani.SetBool("Caminar", false);
    //                    ani.SetBool("Correr", true);
    //                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
    //                    transform.rotation = Quaternion.Euler(0, 0, 0);
    //                    ani.SetBool("Ataque", false);
    //                }
    //                else
    //                {
    //                    ani.SetBool("Caminar", false);
    //                    ani.SetBool("Correr", true);
    //                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
    //                    transform.rotation = Quaternion.Euler(0, 180, 0);
    //                    ani.SetBool("Ataque", false);
    //                }
    //            }
    //            else
    //            {
    //                if (!atacando)
    //                {
    //                    if (transform.position.x < target.transform.position.x)
    //                    {
    //                        transform.rotation = Quaternion.Euler(0, 0, 0);
    //                    }
    //                    else
    //                    {
    //                        transform.rotation = Quaternion.Euler(0, 180, 0);
    //                    }
    //                    ani.SetBool("Caminar", false);
    //                    ani.SetBool("Correr", false);
    //                }
    //            }
    //        }
    //    }

    //    public void Final_Ani()
    //    {
    //        ani.SetBool("Ataque", false);
    //        atacando = false;
    //        rango.GetComponent<BoxCollider2D>().enabled = true;
    //    }
    //    public void ColliderWeaponTrue()
    //    {
    //        Hit.GetComponent<BoxCollider2D>().enabled = true;
    //    }
    //    public void ColliderWeaponFalse()
    //    {
    //        Hit.GetComponent<BoxCollider2D>().enabled = false;
    //    }

    //    // Update is called once per frame
    //    void Update()
    //    {
    //        Comportamientos();


    //    }
    //    public void TomarDaño(float daño)
    //    {
    //        vida -= daño;

    //        //barraVida.CambiarVidaActual(vida);

    //        ani.SetTrigger("Daño");
    //        if (vida <= 0)

    //        {
    //            ani.SetTrigger("Muerte");
    //            //StartCoroutine(TiempoMuerte());
    //            //Destroy(gameObject);
    //            //Muerte();

    //        }

    //    }
    //    public void Muerte()
    //    {
    //        Destroy(gameObject);
    //    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //IEnumerator TiempoMuerte()
    //{
    //    yield return new WaitForSeconds(1);
    //}
    //public int rutina;
    //public float cronometro;
    //public Animator ani;
    //public int direccion;
    //public float speed_walk;
    //public float speed_run;
    //public GameObject target;
    //public bool atacando;

    //public float rango_vision;
    //public float rango_ataque;
    //public GameObject rango;
    //public GameObject Hit;


    //// Start is called before the first frame update
    //void Start()
    //{
    //    ani = GetComponent<Animator>();
    //    target = GameObject.Find("PJ");
    //}

    //public void Comportamientos()
    //{
    //    if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_vision && !atacando)
    //    {
    //        ani.SetBool("Correr", false);
    //        cronometro += 1 * Time.deltaTime;
    //        if (cronometro >= 4)
    //        {
    //            rutina = Random.Range(0, 2);
    //            cronometro = 0;
    //        }
    //        switch (rutina)
    //        {
    //            case 0:
    //                ani.SetBool("Caminar", false);
    //                break;

    //            case 1:
    //                direccion = Random.Range(0, 2);
    //                rutina++;
    //                break;

    //            case 2:

    //                switch (direccion)
    //                {
    //                    case 0:
    //                        transform.rotation = Quaternion.Euler(0, 0, 0);
    //                        transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
    //                        break;

    //                    case 1:
    //                        transform.rotation = Quaternion.Euler(0, 180, 0);
    //                        transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
    //                        break;
    //                }
    //                ani.SetBool("Caminar", true);
    //                break;
    //        }
    //    }
    //    else
    //    {
    //        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacando)
    //        {
    //            if (transform.position.x < target.transform.position.x)
    //            {
    //                ani.SetBool("Caminar", false);
    //                ani.SetBool("Correr", true);
    //                transform.Translate(Vector3.right * speed_run * Time.deltaTime);
    //                transform.rotation = Quaternion.Euler(0, 0, 0);
    //                ani.SetBool("Ataque", false);
    //            }
    //            else
    //            {
    //                ani.SetBool("Caminar", false);
    //                ani.SetBool("Correr", true);
    //                transform.Translate(Vector3.right * speed_run * Time.deltaTime);
    //                transform.rotation = Quaternion.Euler(0, 180, 0);
    //                ani.SetBool("Ataque", false);
    //            }
    //        }
    //        else
    //        {
    //            if (!atacando)
    //            {
    //                if (transform.position.x < target.transform.position.x)
    //                {
    //                    transform.rotation = Quaternion.Euler(0, 0, 0);
    //                }
    //                else
    //                {
    //                    transform.rotation = Quaternion.Euler(0, 180, 0);
    //                }
    //                ani.SetBool("Caminar", false);
    //                ani.SetBool("Correr", false);
    //            }
    //        }
    //    }
    //}

    //public void Final_Ani()
    //{
    //    ani.SetBool("Ataque", false);
    //    atacando = false;
    //    rango.GetComponent<BoxCollider2D>().enabled = true;
    //}
    //public void ColliderWeaponTrue()
    //{
    //    Hit.GetComponent<BoxCollider2D>().enabled = true;
    //}
    //public void ColliderWeaponFalse()
    //{
    //    Hit.GetComponent<BoxCollider2D>().enabled = false;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Comportamientos();
    //}
}
