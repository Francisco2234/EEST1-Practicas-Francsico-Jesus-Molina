using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonAtras : MonoBehaviour
{
    public void Mapa1()
    {
        //Carga una escena nueva basado en el index que tiene la actual y la siguiente.
        //Le suma uno al index de la escena actual y el resultado es el index de la escena a cargar.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
    }

    public void Mapa2()
    {
        //Carga una escena nueva basado en el index que tiene la actual y la siguiente.
        //Le suma uno al index de la escena actual y el resultado es el index de la escena a cargar.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 4);
    }

    public void Mapa3()
    {
        //Carga una escena nueva basado en el index que tiene la actual y la siguiente.
        //Le suma uno al index de la escena actual y el resultado es el index de la escena a cargar.

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 5);
    }
}
