using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoP2 : MonoBehaviour
{
    //Variables Publicas:

    public float Velocidad = 250;
    public float vMax = 0;
    public GameObject ObjetoAAgarrar2;
    public GameObject ObjetoObtenido2;
    public Transform Agarre2;

    //Variables Privadas:

    private Rigidbody2D fuerzas;

    void Start()
    {
        fuerzas = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Agarrar2();
    }
    void FixedUpdate()
    {
        MovHorizontal();

        MovVertical();
    }

    public void MovHorizontal()
    {
        float ejeX = Input.GetAxis("MovHorizontal");

        fuerzas.AddForce(Vector2.right * Velocidad * ejeX);

        float limite = Mathf.Clamp(fuerzas.velocity.x, -vMax, vMax);

        fuerzas.velocity = new Vector2(limite, fuerzas.velocity.y);

        Debug.Log(fuerzas.velocity.x);
    }

    public void MovVertical()
    {
        float ejeY = Input.GetAxis("MovVertical");

        fuerzas.AddForce(Vector2.down * Velocidad * ejeY);

        float limite = Mathf.Clamp(fuerzas.velocity.y, -vMax, vMax);

        fuerzas.velocity = new Vector2(fuerzas.velocity.x, limite);

        Debug.Log(fuerzas.velocity.y);
    }

    public void Agarrar2()
    {
        //Evaluando si hay un objeto para agarrar, si se puede agarrar y si no tenemos ningún objeto actualmente.

        if (ObjetoAAgarrar2 != null && ObjetoAAgarrar2.GetComponent<Boton2>().poderAgarrar2 == true && ObjetoObtenido2 == null)
        {
            //Evaluando si se presiono 'P'.

            if (Input.GetKeyDown(KeyCode.P))
            {
                //Diciendo que el objeto que tenemos es el objeto que estaba para agarrar.

                ObjetoObtenido2 = ObjetoAAgarrar2;

                //Diciendo que no se puede agarrar porque ya lo tenemos.

                ObjetoObtenido2.GetComponent<Boton2>().poderAgarrar2 = false;

                //Estableciendo al objeto obtenido como hijo del objeto 'Agarre2'.

                ObjetoObtenido2.transform.SetParent(Agarre2);

                //Igualando la posición del objeto obtenido con la posición de 'Agarre2'.
                //A lo anterior le resto un 'Vector3' para corregir la posición.

                ObjetoObtenido2.transform.position = Agarre2.position - new Vector3(Agarre2.position.x - Agarre2.position.x - -73.2f, 36, Agarre2.position.z);
            }
        }

        //Evaluando si el objeto obtenido no esta vacío.

        else if (ObjetoObtenido2 != null)
        {
            //Evaluando si se presiono 'P' de nuevo.

            if (Input.GetKeyDown(KeyCode.P))
            {
                //Diciendo que se puede agarrar porque no lo tenemos.

                ObjetoObtenido2.GetComponent<Boton2>().poderAgarrar2 = true;

                //Sacando como hijo al objeto obtenido de 'Agarre2'.

                ObjetoObtenido2.transform.SetParent(null);

                //Diciendo que no hay ningún objeto obtenido.

                ObjetoObtenido2 = null;
            }
        }
    }
}
