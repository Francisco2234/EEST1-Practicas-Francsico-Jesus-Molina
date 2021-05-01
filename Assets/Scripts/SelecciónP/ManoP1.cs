using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoP1 : MonoBehaviour
{
    //Variables Publicas:

    public float Velocidad = 250;
    public float vMax = 0;
    public GameObject ObjetoAAgarrar;
    public GameObject ObjetoObtenido;
    public Transform Agarre;

    //Variables Pivadas:

    private Rigidbody2D fuerzas;
    
    void Start()
    {
        fuerzas = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Agregando el método 'Agarrar'.

        Agarrar();
    }

    void FixedUpdate()
    {
        //Agregando el método 'MovHorizontal'.

        MovHorizontal();

        //Agregando el método 'MovVertical'.

        MovVertical();
    }

    /*
         ----------------------------
        |MÉTODO MOVIMIENTO HORIZONTAL|
         ----------------------------
    */
    public void MovHorizontal()
    {
        float ejeX = Input.GetAxis("Horizontal");

        fuerzas.AddForce(Vector2.right * Velocidad * ejeX);

        float limite = Mathf.Clamp(fuerzas.velocity.x, -vMax, vMax);

        fuerzas.velocity = new Vector2(limite, fuerzas.velocity.y);

        Debug.Log(fuerzas.velocity.x);
    }

    /*
         --------------------------
        |MÉTODO MOVIMIENTO VERTICAL|
         --------------------------
    */
    public void MovVertical()
    {
        float ejeY = Input.GetAxis("Vertical");

        fuerzas.AddForce(Vector2.up * Velocidad * ejeY);

        float limite = Mathf.Clamp(fuerzas.velocity.y, -vMax, vMax);

        fuerzas.velocity = new Vector2(fuerzas.velocity.x, limite);

        Debug.Log(fuerzas.velocity.y);
    }

    /*
         --------------
        |MÉTODO AGARRAR|
         --------------
    */
    public void Agarrar()
    {
        //Evaluando si hay un objeto para agarrar, si se puede agarrar y si no tenemos ningún objeto actualmente.

        if (ObjetoAAgarrar != null && ObjetoAAgarrar.GetComponent<Boton1>().poderAgarrar == true && ObjetoObtenido == null)
        {
            //Evaluando si se presiono 'J'.

            if (Input.GetKeyDown(KeyCode.J))
            {
                //Diciendo que el objeto que tenemos es el objeto que estaba para agarrar.

                ObjetoObtenido = ObjetoAAgarrar;

                //Diciendo que no se puede agarrar porque ya lo tenemos.

                ObjetoObtenido.GetComponent<Boton1>().poderAgarrar = false;

                //Estableciendo al objeto obtenido como hijo del objeto 'Agarre'.

                ObjetoObtenido.transform.SetParent(Agarre);

                //Igualando la posición del objeto obtenido con la posición de 'Agarre'.
                //A lo anterior le resto un 'Vector3' para corregir la posición.

                ObjetoObtenido.transform.position = Agarre.position - new Vector3(Agarre.position.x - Agarre.position.x - 1.5f, 57, Agarre.position.z);
            }
        }

        //Evaluando si el objeto obtenido no esta vacío.

        else if (ObjetoObtenido != null)
        {
            //Evaluando si se presiono 'J' de nuevo.

            if (Input.GetKeyDown(KeyCode.J))
            {
                //Diciendo que se puede agarrar porque no lo tenemos.

                ObjetoObtenido.GetComponent<Boton1>().poderAgarrar = true;

                //Sacando como hijo al objeto obtenido de 'Agarre'.

                ObjetoObtenido.transform.SetParent(null);

                //Diciendo que no hay ningún objeto obtenido.

                ObjetoObtenido = null;
            }
        }
    }
}
