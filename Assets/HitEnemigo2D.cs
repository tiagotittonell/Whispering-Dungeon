using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo2D : MonoBehaviour
{

    public int Daño;

  

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("PJ"))
        {
            other.gameObject.GetComponent<VidaPersonaje>().TomarDaño(10, other.GetContact(0).normal);
            other.gameObject.GetComponent<VidaPersonaje>().TomarDañoBarra(10);
            //if (coll.GetComponent<MovimientoPersonaje>().HP_Min > 0 && coll.GetComponent<MovimientoPersonaje>().damage_ == false)
            //{
            //    coll.GetComponent<MovimientoPersonaje>().ani.SetTrigger("damage");
            //    coll.GetComponent<MovimientoPersonaje>().damage_ = true;



            //    if (transform.position.x > coll.transform.position.x)
            //    {
            //        coll.GetComponent<MovimientoPersonaje>().empuje = -3;
            //        coll.transform.rotation = Quaternion.Euler(0, 0, 0);
            //    }
            //    else
            //    {
            //        coll.GetComponent<MovimientoPersonaje>().empuje = 3;
            //        coll.transform.rotation = Quaternion.Euler(0, 180, 0);
            //    }

            //    coll.GetComponent<MovimientoPersonaje>().HP_Min -= Daño;
            //}
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Brillo();
        /*
        coll.GetComponentInParent<Megaman_X>().audio_S.clip = coll.GetComponentInParent<Megaman_X>().sonido[3];
        coll.GetComponentInParent<Megaman_X>().audio_S.Play();
        */
    }
}
