using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boton2 : MonoBehaviour
{
    //Listas:

    public GameObject[] jugadores;
    public GameObject[] prefaps2;

    //Variables Públicas:

    public GameObject PersonajeASeleccionar2;
    public GameObject PersonajeSeleccionado2;
    public Transform Seleccionador2;
    public bool poderAgarrar2 = true;

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

    public void Seleccionar()
    {
        if (PersonajeASeleccionar2 != null && PersonajeASeleccionar2.GetComponent<Seleccionar>().poderSeleccionar == true && PersonajeSeleccionado2 == null)
        {
            //Evaluando si se presiono 'J'.

            if (poderAgarrar2 == true)
            {
                //Diciendo que el objeto que tenemos es el objeto que estaba para agarrar.

                PersonajeSeleccionado2 = PersonajeASeleccionar2;

                //Diciendo que no se puede agarrar porque ya lo tenemos.

                PersonajeSeleccionado2.GetComponent<Seleccionar>().poderSeleccionar = true;

                if (PersonajeSeleccionado2 != null)
                {
                    foreach (var jugador in this.jugadores)
                    {
                        if (jugador.gameObject.CompareTag("Megaman") && PersonajeSeleccionado2.gameObject.CompareTag("Megaman"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[0];
                        }
                        else if (jugador.gameObject.CompareTag("Minato") && PersonajeSeleccionado2.gameObject.CompareTag("Minato"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[1];
                        }
                        else if (jugador.gameObject.CompareTag("Ninja") && PersonajeSeleccionado2.gameObject.CompareTag("Ninja"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[2];
                        }
                        else if (jugador.gameObject.CompareTag("Sonic") && PersonajeSeleccionado2.gameObject.CompareTag("Sonic"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[3];
                        }
                        else if (jugador.gameObject.CompareTag("Sub Zero") && PersonajeSeleccionado2.gameObject.CompareTag("Sub Zero"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[4];
                        }
                        else if (jugador.gameObject.CompareTag("Zabuza") && PersonajeSeleccionado2.gameObject.CompareTag("Zabuza"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[5];
                        }
                        else if (jugador.gameObject.CompareTag("Zero") && PersonajeSeleccionado2.gameObject.CompareTag("Zero"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[6];
                        }
                        else if (jugador.gameObject.CompareTag("Ryu") && PersonajeSeleccionado2.gameObject.CompareTag("Ryu"))
                        {
                            Puente2.prefapsJugadores = this.prefaps2[7];
                        }

                    }
                }
            }
        }

        else if (PersonajeSeleccionado2 != null)
        {
            if (poderAgarrar2 == false)
            {
                PersonajeSeleccionado2 = PersonajeASeleccionar2;

                PersonajeSeleccionado2.GetComponent<Seleccionar>().poderSeleccionar = false;
            }
        }
    }

}
