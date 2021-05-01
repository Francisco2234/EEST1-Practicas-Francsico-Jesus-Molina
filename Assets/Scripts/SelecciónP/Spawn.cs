using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    //Lista:

    public GameObject[] Circulos;

    void Start()
    {
        GameObject Hijo = Instantiate(Puente.prefapsJugadores, new Vector3(this.transform.position.x, this.transform.position.y, Puente.prefapsJugadores.transform.position.z), Puente.prefapsJugadores.transform.rotation) as GameObject;

        gameObject.transform.parent = Hijo.transform;

        ActivarCirculo();
    }


    void Update()
    {
    }
    public void ActivarCirculo()
    {
        foreach(GameObject circulo in Circulos)
        {
            if(Puente.prefapsJugadores.gameObject.tag == "Megaman")
            {
                if(circulo.gameObject.tag == "Megaman")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            else if (Puente.prefapsJugadores.gameObject.tag == "Minato")
            {
                if (circulo.gameObject.tag == "Minato")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            if (Puente.prefapsJugadores.gameObject.tag == "Sonic")
            {
                if (circulo.gameObject.tag == "Sonic")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            if (Puente.prefapsJugadores.gameObject.tag == "Zabuza")
            {
                if (circulo.gameObject.tag == "Zabuza")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            if (Puente.prefapsJugadores.gameObject.tag == "Ninja")
            {
                if (circulo.gameObject.tag == "Ninja")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            if (Puente.prefapsJugadores.gameObject.tag == "Ryu")
            {
                if (circulo.gameObject.tag == "Ryu")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            if (Puente.prefapsJugadores.gameObject.tag == "Zero")
            {
                if (circulo.gameObject.tag == "Zero")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
            if (Puente.prefapsJugadores.gameObject.tag == "Sub Zero")
            {
                if (circulo.gameObject.tag == "SubZero")
                {
                    circulo.gameObject.SetActive(true);
                }
            }
        }
    }
}