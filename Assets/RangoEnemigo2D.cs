using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo2D : MonoBehaviour
{
    public Animator ani;
    public Enemigo3 enemigo; // Asigna el componente del script del enemigo aquí

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("PJ"))
        {
            ani.SetBool("Caminar", false);
            ani.SetBool("Correr", false);
            ani.SetBool("Ataque", true);
            enemigo.atacando = true;
            enemigo.RealizarAtaque(); // Llamamos al método de RealizarAtaque() en el enemigo
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("PJ"))
        {
            ani.SetBool("Ataque", false);
            enemigo.DetenerAtaque(); // Llamamos al método para detener el ataque del enemigo
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    //public Animator ani;
    //public Enemigo3 enemigo;

    //void OnTriggerEnter2D(Collider2D coll)
    //{
    //    if (coll.CompareTag("PJ"))
    //    {
    //       ani.SetBool("Caminar", false);
    //       ani.SetBool("Correr", false);
    //       ani.SetBool("Ataque", true);
    //       enemigo.atacando = true;
    //       GetComponent<BoxCollider2D>().enabled = false;            
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
