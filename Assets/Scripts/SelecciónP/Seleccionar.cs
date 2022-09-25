using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seleccionar : MonoBehaviour
{
    //Variables Públicas:

    public bool poderSeleccionar = true;

    //Variables Privadas:

    private void Start()
    {

    }

    //Método del tipo 'Trigger' que indica cuando entramos en colisión con algún objeto.
    private void OnTriggerEnter2D(Collider2D enterColision)
    {
        //Evaluando si estoy entrando en la colisión del objeto que tiene el tag "Agarre".

        if (enterColision.tag == "Boton1")
        {
            //Obteniendo un componente de colisión de 'ManoP1'.
            //Llamando a la variable 'ObjetoAAgarrar' del tipo 'GameObject'.
            //Igualandola a 'this.gameObject' que quiere decir que hay un objeto.

            enterColision.GetComponentInParent<Boton1>().PersonajeASeleccionar = this.gameObject;

        }

        if (enterColision.tag == "Boton2")
        {
            //Obteniendo un componente de colisión de 'ManoP1'.
            //Llamando a la variable 'ObjetoAAgarrar' del tipo 'GameObject'.
            //Igualandola a 'this.gameObject' que quiere decir que hay un objeto.

            enterColision.GetComponentInParent<Boton2>().PersonajeASeleccionar2 = this.gameObject;

        }
    }

    //Método del tipo 'Trigger' que indica cuando no entramos en colisión con algún objeto.
    private void OnTriggerExit2D(Collider2D exitColision)
    {
        //Evaluando si estoy saliendo de la colisión del objeto que tiene el tag "Agarre".

        if (exitColision.tag == "Boton1")
        {
            //Obteniendo un componente de colisión de 'ManoP1'.
            //Llamando a la variable 'ObjetoAAgarrar' del tipo 'GameObject' e igualandola a 'null' que quiere decir que esta vacío. 

            exitColision.GetComponentInParent<Boton1>().PersonajeASeleccionar = null;
        }

        if (exitColision.tag == "Boton2")
        {
            //Obteniendo un componente de colisión de 'ManoP1'.
            //Llamando a la variable 'ObjetoAAgarrar' del tipo 'GameObject'.
            //Igualandola a 'this.gameObject' que quiere decir que hay un objeto.

            exitColision.GetComponentInParent<Boton2>().PersonajeASeleccionar2 = null;

        }
    }

    
}
