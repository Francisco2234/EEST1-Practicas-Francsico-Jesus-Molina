using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn2 : MonoBehaviour
{
    //Lista:

    public GameObject[] Circulos2;

    void Start()
    {
        GameObject Hijo = Instantiate(Puente2.prefapsJugadores, new Vector3(this.transform.position.x, this.transform.position.y, Puente2.prefapsJugadores.transform.position.z), Puente2.prefapsJugadores.transform.rotation) as GameObject;

        gameObject.transform.SetParent(Hijo.transform);

        ActivarCirculo2();
    }

    public void ActivarCirculo2()
    {
        foreach(GameObject circulo in Circulos2)
        {
            if(Puente2.prefapsJugadores.gameObject.tag == "Megaman")
            {
                if (circulo.gameObject.tag == "Megaman2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if(Puente2.prefapsJugadores.gameObject.tag == "Minato")
            {
                if(circulo.gameObject.tag  == "Minato2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if (Puente2.prefapsJugadores.gameObject.tag == "Sonic")
            {
                if (circulo.gameObject.tag == "Sonic2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if (Puente2.prefapsJugadores.gameObject.tag == "Zabuza")
            {
                if (circulo.gameObject.tag == "Zabuza2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if (Puente2.prefapsJugadores.gameObject.tag == "Ninja")
            {
                if (circulo.gameObject.tag == "Ninja2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if (Puente2.prefapsJugadores.gameObject.tag == "Ryu")
            {
                if (circulo.gameObject.tag == "Ryu2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if (Puente2.prefapsJugadores.gameObject.tag == "Sub Zero")
            {
                if (circulo.gameObject.tag == "SubZero2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if (Puente2.prefapsJugadores.gameObject.tag == "Zero")
            {
                if (circulo.gameObject.tag == "Zero2")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
        }
    }
}