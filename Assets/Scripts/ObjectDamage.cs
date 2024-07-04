using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDamage : MonoBehaviour
{
    public int Daño;

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("PJ"))
    //    {
    //        Obtener el componente de VidaPersonaje del jugador
    //       VidaPersonaje vidaPersonaje = other.GetComponent<VidaPersonaje>();
    //        if (vidaPersonaje != null)
    //        {
    //            Aplicar daño al jugador
    //            vidaPersonaje.TomarDaños(Daño, transform.forward);
    //            vidaPersonaje.TomarDañoBarra(Daño);
    //        }
    //    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.transform.CompareTag("PJ"))
    //    {
    //        collision.transform.GetComponent<VidaPersonaje>().TomarDañoBarra(25);
           
    //    }
    //}
}
