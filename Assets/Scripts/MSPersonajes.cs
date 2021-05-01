using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MSPersonajes : MonoBehaviour
{
    /*
         ------------------
        |BOTÓN START MÉTODO|
         ------------------
     */
    public void BNext()
    {
        //Carga una escena nueva basado en el index que tiene la actual y la siguiente.
        //Le suma uno al index de la escena actual y el resultado es el index de la escena a cargar.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
        -----------------
       |BOTÓN EXIT MÉTODO|
        -----------------
    */
    public void BExit()
    {
        //Cierra la aplicación una vez precionado.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
