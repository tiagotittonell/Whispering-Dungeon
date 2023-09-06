using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo2D : MonoBehaviour
{
    public Animator ani;
    public COPIAENEMIGOSCRIPT enemigo;
    public float rnd = 0;
    void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.CompareTag("PJ"))
        {

            ani.SetBool("Caminar", false);
            ani.SetBool("Correr", false);
            rnd = Random.Range(0.5f, 0.75f);
            StartCoroutine("Cooldown");

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(rnd);
        ani.SetBool("Ataque", true);
        enemigo.atacando = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }





































    //public Animator ani;
    //public Enemigo3 enemigo; // Asigna el componente del script del enemigo aquí

    //void Start()
    //{
    //    ani.SetBool("Ataque", true); // Activar la animación de ataque al inicio
    //}

    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.CompareTag("PJ"))
    //    {
    //        ani.SetBool("Caminar", false);
    //        ani.SetBool("Correr", false);
    //        enemigo.atacando = true;
    //        enemigo.RealizarAtaque(); // Llamamos al método de RealizarAtaque() en el enemigo
    //        GetComponent<BoxCollider2D>().enabled = false;
    //    }
    //}

    //void OnTriggerExit2D(Collider2D coll)
    //{
    //    if (coll.CompareTag("PJ"))
    //    {
    //        enemigo.DetenerAtaque(); // Llamamos al método para detener el ataque del enemigo
    //        GetComponent<BoxCollider2D>().enabled = true;
    //    }
    //}
    //public Animator ani;
    //public Enemigo3 enemigo;

    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.CompareTag("PJ"))
    //    {
    //        ani.SetBool("Caminar", false);
    //        ani.SetBool("Correr", false);
    //        ani.SetBool("Ataque", true);
    //        enemigo.atacando = true;
    //        GetComponent<BoxCollider2D>().enabled = false;
    //    }
    //}
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
