using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Boton1 : MonoBehaviour
{
    Dictionary<GameObject, GameObject> Jugadores = new Dictionary<GameObject, GameObject>();

    public GameObject[] jugadores;
    public GameObject[] prefaps;

    //Variables Públicas:

    public GameObject PersonajeASeleccionar;
    public GameObject PersonajeSeleccionado;
    public Transform Seleccionador;
    public bool poderAgarrar = true;

    //Variables Privadas:
    void Start()
    {
        Seleccionar();
    }

    void Update()
    {
        Seleccionar();
    }

    //Método del tipo 'Trigger' que indica cuando entramos en colisión con algún objeto.
    private void OnTriggerEnter2D(Collider2D enterColision)
    {
        //Evaluando si estoy entrando en la colisión del objeto que tiene el tag "Agarre".

        if (enterColision.tag == "Agarre")
        {
            //Obteniendo un componente de colisión de 'ManoP1'.
            //Llamando a la variable 'ObjetoAAgarrar' del tipo 'GameObject'.
            //Igualandola a 'this.gameObject' que quiere decir que hay un objeto.

            enterColision.GetComponentInParent<ManoP1>().ObjetoAAgarrar = this.gameObject;

        }
    }

    //Método del tipo 'Trigger' que indica cuando no entramos en colisión con algún objeto.
    private void OnTriggerExit2D(Collider2D exitColision)
    {
        //Evaluando si estoy saliendo de la colisión del objeto que tiene el tag "Agarre".

        if (exitColision.tag == "Agarre")
        {
            //Obteniendo un componente de colisión de 'ManoP1'.
            //Llamando a la variable 'ObjetoAAgarrar' del tipo 'GameObject' e igualandola a 'null' que quiere decir que esta vacío. 

            exitColision.GetComponentInParent<ManoP1>().ObjetoAAgarrar = null;
        }
    }

    public void Seleccionar()
    {
        if (PersonajeASeleccionar != null && PersonajeASeleccionar.GetComponent<Seleccionar>().poderSeleccionar == true && PersonajeSeleccionado == null)
        {
            //Evaluando si se presiono 'J'.

            if (poderAgarrar == true)
            {
                //Diciendo que el objeto que tenemos es el objeto que estaba para agarrar.

                PersonajeSeleccionado = PersonajeASeleccionar;

                //Diciendo que no se puede agarrar porque ya lo tenemos.

                PersonajeSeleccionado.GetComponent<Seleccionar>().poderSeleccionar = true;

                if (PersonajeSeleccionado != null)
                {
                    foreach (var jugador in this.jugadores)
                    {
                        if (jugador.gameObject.CompareTag("Megaman") && PersonajeSeleccionado.gameObject.CompareTag("Megaman"))
                        {
                            Puente.prefapsJugadores = this.prefaps[0];
                        }
                        else if (jugador.gameObject.CompareTag("Minato") && PersonajeSeleccionado.gameObject.CompareTag("Minato"))
                        {
                            Puente.prefapsJugadores = this.prefaps[1];
                        }
                        else if (jugador.gameObject.CompareTag("Zabuza") && PersonajeSeleccionado.gameObject.CompareTag("Zabuza"))
                        {
                            Puente.prefapsJugadores = this.prefaps[2];
                        }
                        else if (jugador.gameObject.CompareTag("Ninja") && PersonajeSeleccionado.gameObject.CompareTag("Ninja"))
                        {
                            Puente.prefapsJugadores = this.prefaps[3];
                        }
                        else if (jugador.gameObject.CompareTag("Ryu") && PersonajeSeleccionado.gameObject.CompareTag("Ryu"))
                        {
                            Puente.prefapsJugadores = this.prefaps[4];
                        }
                        else if (jugador.gameObject.CompareTag("Sonic") && PersonajeSeleccionado.gameObject.CompareTag("Sonic"))
                        {
                            Puente.prefapsJugadores = this.prefaps[5];
                        }
                        else if (jugador.gameObject.CompareTag("Sub Zero") && PersonajeSeleccionado.gameObject.CompareTag("Sub Zero"))
                        {
                            Puente.prefapsJugadores = this.prefaps[6];
                        }
                        else if (jugador.gameObject.CompareTag("Zero") && PersonajeSeleccionado.gameObject.CompareTag("Zero"))
                        {
                            Puente.prefapsJugadores = this.prefaps[7];
                        }

                    }
                }
            }
        }

        else if (PersonajeSeleccionado != null)
        {
            if (poderAgarrar == false)
            {
                PersonajeSeleccionado = PersonajeASeleccionar;

                PersonajeSeleccionado.GetComponent<Seleccionar>().poderSeleccionar = false;
            }
        }
    }
}
