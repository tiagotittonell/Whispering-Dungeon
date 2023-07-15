using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo2D : MonoBehaviour
{

    public int Daño;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {

            //if (coll.GetComponent<Megaman_X>().HP_Min > 0 && coll.GetComponent<Megaman_X>().damage_ == false)
            //{
            //    coll.GetComponent<Megaman_X>().ani.SetTrigger("damage");
            //    coll.GetComponent<Megaman_X>().damage_ = true;

            //    coll.GetComponent<Megaman_X>().audio_S.clip = coll.GetComponent<Megaman_X>().sonido[3];
            //    coll.GetComponent<Megaman_X>().audio_S.Play();

            //    if (transform.position.x > coll.transform.position.x)
            //    {
            //        coll.GetComponent<Megaman_X>().empuje = -3;
            //        coll.transform.rotation = Quaternion.Euler(0, 0, 0);
            //    }
            //    else
            //    {
            //        coll.GetComponent<Megaman_X>().empuje = 3;
            //        coll.transform.rotation = Quaternion.Euler(0, 180, 0);
            //    }

            //    coll.GetComponent<Megaman_X>().HP_Min -= Daño;
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
