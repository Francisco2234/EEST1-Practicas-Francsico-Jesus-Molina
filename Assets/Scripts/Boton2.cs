using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton2 : MonoBehaviour
{
    //Variables Públicas:

    public bool poderAgarrar2 = true;

    //Variables Privadas:


    //Método del tipo 'Trigger' que indica cuando entramos en colisión con algún objeto.
    private void OnTriggerEnter2D(Collider2D enterColision)
    {
        //Evaluando si estoy entrando en la colisión del objeto que tiene el tag 'Agarre2'.

        if (enterColision.tag == "Agarre2")
        {
            //Obteniendo un componente de colisión de 'ManoP2'.
            //Llamando a la variable 'ObjetoAAgarrar2' del tipo 'GameObject'.
            //Igualandola a 'this.gameObject' que quiere decir que hay un objeto.

            enterColision.GetComponentInParent<ManoP2>().ObjetoAAgarrar2 = this.gameObject;

        }
    }

    //Método del tipo 'Trigger' que indica cuando no entramos en colisión con algún objeto.
    private void OnTriggerExit2D(Collider2D exitColision)
    {
        //Evaluando si estoy saliendo de la colisión del objeto que tiene el tag 'Agarre2'.

        if (exitColision.tag == "Agarre2")
        {
            //Obteniendo un componente de colisión de 'ManoP2'.
            //Llamando a la variable 'ObjetoAAgarrar2' del tipo 'GameObject' e igualandola a 'null' que quiere decir que esta vacío. 

            exitColision.GetComponentInParent<ManoP2>().ObjetoAAgarrar2 = null;
        }
    }


}
