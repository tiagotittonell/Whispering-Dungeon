using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo2D : MonoBehaviour
{




    public int Daño;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PJ"))
        {
            // Obtener el componente de VidaPersonaje del jugador
            VidaPersonaje vidaPersonaje = other.GetComponent<VidaPersonaje>();
            if (vidaPersonaje != null)
            {
                // Aplicar daño al jugador
                //vidaPersonaje.TomarDañoBarra(Daño, transform.forward);
                vidaPersonaje.TomarDañoBarra(Daño);
            }
        }
    }
    //public int Daño;

    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("PJ"))
    //    {
    //        other.gameObject.GetComponent<VidaPersonaje>().TomarDaño(Daño, other.GetContact(0).normal);
    //        other.gameObject.GetComponent<VidaPersonaje>().TomarDañoBarra(Daño);
    //    }
    //}




    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //Brillo();
    //    /*
    //    coll.GetComponentInParent<Megaman_X>().audio_S.clip = coll.GetComponentInParent<Megaman_X>().sonido[3];
    //    coll.GetComponentInParent<Megaman_X>().audio_S.Play();
    //    */
    //}
}
