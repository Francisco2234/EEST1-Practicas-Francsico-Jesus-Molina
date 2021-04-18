using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Indica que requiere un componente del tipo 'Camera'.

[RequireComponent(typeof(Camera))]

public class CamaraGame : MonoBehaviour
{
    //Inicializando una lista publica del tipo 'Transform' la cual se encarga de la posición, la rotación y  la escala.

    public List<Transform> players;

    //Variables Publicas.

    public Vector3 sobrante;
    public float tiempoRetraso;
    public float zMinimo = 0;
    public float zMaximo = 0;
    public float zLimite = 0;


    //Variables Privadas.

    private Vector3 velocidad;
    private Camera camara;

    void Start()
    {
        /*
            -------------------------------------------------------------------------
           |ASIGNANDO A LAS VARIABLES UN COMPONENTE OBTENIDO DE SUS RESPECTIVOS TIPOS|
            -------------------------------------------------------------------------
        */

        camara = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        //Si la cantidad de jugadores es igual a cero lo omite.

        if(players.Count == 0)
        {
            return;
        }

        Movimiento();

        Zoom();

    }

    /*
       -----------------
      |DISTANCIA FUNCIÓN|
       -----------------
    */
    float Distancia()
    {
        //Declarando una variable del tipo 'Bounds' (tipo que forma un cuadro delimitador).
        //Creo un nuevo 'Bounds' y pongo el centro de la lista 'players' (con 0 y la posición), y como tamaño un 'Vector3.zero'

        var distancia = new Bounds(players[0].position, Vector3.zero);

        //Creo variable 'cantJ' como entero para guardar la cantidad de jugadores que tiene la lista.

        for (int cantJ = 0; cantJ < players.Count; cantJ++)
        {
            //En la variable distancia creo una capsula la cual me permite expandir los limites del delimitador
            //Lo anterior es para incluir a los 'x' jugadores (en este caso dos).

            distancia.Encapsulate(players[cantJ].position);
        }

        //Devuelve el tamaño del recuadro delimitador en el eje x.

        return distancia.size.x;
    }

    /*
       -----------
      |ZOOM MÉTODO|
       -----------
    */
    void Zoom()
    {
        //Declarando una variable de tipo float 'nZoom'.
        //Calculando una interpolación entre 'zMaximo'(a) y 'Minimo'(b) con respecto a 'Distancia'(t).
        //Por último divido la distancia por el limite establecido en 'zLimite'.

        float nZoom = Mathf.Lerp(zMaximo, zMinimo, Distancia() / zLimite);

        //Guardo en el campo de visión de la cámara otra interpolación entre el campo de visión(a) y 'nZoom'(b)
        //con respecto a 'Time.deltaTime'(t).
        //Este último hace referencia al tiempo de finalización en cada fotograma.

        camara.fieldOfView = Mathf.Lerp(camara.fieldOfView, nZoom, Time.deltaTime);
    }

    /*
       -----------------
      |MOVIMIENTO MÉTODO|
       -----------------
    */
    void Movimiento()
    {
        //Declarando una variable Vector3 'centro' y le asigno la función 'Centro'.

        Vector3 centro = Centro();

        //Declarando una variable Vector3 nPosicion y asignandole la suma entre 'centro' y 'sobrante'.
        //Este último es un sobrante añadido para que haya espacio entre los limites y los jugadores.

        Vector3 nPosicion = centro + sobrante;

     //Transformando la posición de la camara asignandole un 'Vector3' con 'SmoothDamp'(Funciona como suavizador de movimiento) el cual le doy:
     //Ubicación actual, 'nPosicion'(objetivo a alcanzar), 'velocidad'(referencia de la velocidad actual) y el 'tiempoRetraso'(tiempo suavizado).

        transform.position = Vector3.SmoothDamp(transform.position, nPosicion, ref velocidad, tiempoRetraso);
    }

    /*
       --------------
      |CENTRO FUNCIÓN|
       --------------
    */
    Vector3 Centro()
    {
        //Evaluando si la cantidad de jugadores es igual a '1'.

        if(players.Count == 1)
        {
            //Devolviendo como posición cetral '0' en dicho caso.

            return players[0].position;
        }

        //Declarando una variable del tipo Bounds y asignandole una posición central de cero en la lista de 'players'
        //y un 'Vector3.zero'.

        var capsula = new Bounds(players[0].position, Vector3.zero);

        //Declarando la variale entera 'cantJ' para guardar la cantidad de jugadores que hay en la lista.

        for (int cantJ = 0; cantJ < players.Count; cantJ++)
        {
            //En la variable 'capsula' creo una capsula la cual me permite expandir los limites del delimitador.
            //Lo anterior es para incluir a los 'x' jugadores (en este caso dos).

            capsula.Encapsulate(players[cantJ].position);
        }

        //Devuelve el centro del recuadro delimitador.

        return capsula.center;
    }
}
