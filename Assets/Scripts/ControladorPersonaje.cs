using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPersonaje : MonoBehaviour
{
  
    public Animator anim;
    public Animation aanim;
    
    //private bool idleCombate = false;

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;

  
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       

    }

   
    private void Update()
    {
       
        if(tiempoEntreAtaques > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0) && tiempoSiguienteAtaque <= 0)
        {

            Golpe();
            
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
        //if(Input.GetMouseButtonDown(0) && tiempoSiguienteAtaque <= 0)
        //{
        //    Golpe2();
        //    tiempoSiguienteAtaque = tiempoEntreAtaques;

        //}
    
    }

    private void Golpe()
    {
        anim.SetTrigger("Golpe");
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {//{
                
                colisionador.transform.GetComponent<COPIAENEMIGOSCRIPT>().TomarDaño(dañoGolpe);
            }
            else if (colisionador.CompareTag("Boss"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDañoEnemigo(dañoGolpe);
            }
        }
    }
    //private void Golpe2()
    //{
    //    anim.SetTrigger("Golpe");
    //    Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
    //    foreach (Collider2D colisionador in objetos)
    //    {
    //        if (colisionador.CompareTag("Enemigo"))
    //        {//{
    //            colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
    //            colisionador.transform.GetComponent<COPIAENEMIGOSCRIPT>().TomarDaño(dañoGolpe);
    //        }
    //    }
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
  

}
