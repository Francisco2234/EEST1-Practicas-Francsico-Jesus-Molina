using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    /*
        ------------------
       |BOTÓN START MÉTODO|
        ------------------
    */
    public void BStart()
    {
        //Carga una escena nueva basado en el index que tiene la actual y la siguiente.
        //Le suma uno al index de la escena actual y el resultado es el index de la escena a cargar.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
        --------------------
       |BOTÓN OPTIONS MÉTODO|
        --------------------
    */
    public void BOption()
    {

    }

    /*
        -----------------
       |BOTÓN EXIT MÉTODO|
        -----------------
    */
    public void BExit()
    {
        //Cierra la aplicación una vez precionado.

        Application.Quit();
    }
}
