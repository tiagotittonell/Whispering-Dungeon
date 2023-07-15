using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoEnemigo2D : MonoBehaviour
{
    public Animator ani;
    public Enemigo3 enemigo;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
           ani.SetBool("Caminar", false);
           ani.SetBool("Correr", false);
           ani.SetBool("Atacar", true);
           enemigo.atacando = true;
           GetComponent<BoxCollider2D>().enabled = false;            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
