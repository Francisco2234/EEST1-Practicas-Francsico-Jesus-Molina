using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectiles2 : MonoBehaviour
{
    public LayerMask enemigoP1;

    public float rangoDisparo2;

    public GameObject Jugador2;

    public int dañoBala2 = 3;

    public int fuerzaBala2 = 150;

    public Transform JugadorTransform2;

    private Rigidbody2D fuerzas2;

    public float proyectilVelocidad2;

    public float proyectilVida2;


    void Awake()
    {
        fuerzas2 = GetComponent<Rigidbody2D>();

        Jugador2 = GameObject.Find("Player2");

        JugadorTransform2 = Jugador2.transform;
    }
    void Start()
    {
        if (gameObject.tag == "PoderSonic")
        {
            fuerzas2.velocity = new Vector2(0, -proyectilVelocidad2);
        }
        else
        {
            if (Jugador2.transform.localScale.x < 0)
            {
                fuerzas2.velocity = new Vector2(proyectilVelocidad2, fuerzas2.velocity.y);
                transform.localScale = new Vector3(3.377424f, 4.5f, 1);
                Disparar2();

            }
            else
            {
                fuerzas2.velocity = new Vector2(-proyectilVelocidad2, fuerzas2.velocity.y);
                transform.localScale = new Vector3(-3.377424f, -4.5f, 1);
                Disparar2();
            }
        }

    }
    void Update()
    {
        Destroy(gameObject, proyectilVida2);

        Disparar2();
    }

    public void Disparar2()
    {
        Collider2D[] colisionEnemigo = Physics2D.OverlapCircleAll(transform.position, rangoDisparo2, enemigoP1);

        //Iterando en 'colisionEnemigo' y guardandolo en 'enemigo'.

        foreach (Collider2D enemigo in colisionEnemigo)
        {
            //Obteniendo un componente 'Player2' de 'enemigo'
            //llamando a el método 'DañoP2' y agregandole como parametro daño el 'dañoAtaque'.

            enemigo.GetComponent<Player1>().DañoP1(dañoBala2);

            //Declarando una variable Vector3 'dirección' y asignandole la distancia que tenemos con respecto al enemigo.
            //Lo anterior mencionado lo hacemos restando la posición actual del enemigo con la nuestra y lo almacenamos.

            float direccion = enemigo.transform.position.x - transform.position.x;

            //Obteniendo el componente 'Rigidbody2D' del enemigo y añadiendole una fuerza de ataque.
            //Y, sumando a lo anterior, multiplicandolo por la dirección. 

            enemigo.GetComponent<Rigidbody2D>().AddForce(new Vector3(direccion * fuerzaBala2, enemigo.transform.position.y, enemigo.transform.position.z));

            if (gameObject.tag == "SubZero")
            {
                enemigo.GetComponent<Rigidbody2D>().gravityScale = 0;

                enemigo.GetComponent<Rigidbody2D>().mass = 60;

                enemigo.GetComponent<Player1>().Desactivar();

                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                StartCoroutine(EsperaActivar2());

                IEnumerator EsperaActivar2()
                {
                    yield return new WaitForSeconds(5f);

                    enemigo.GetComponent<Player1>().Activar();

                    enemigo.GetComponent<Rigidbody2D>().gravityScale = 1;

                    enemigo.GetComponent<Rigidbody2D>().mass = 1;

                    Destroy(gameObject);
                }
            }
            else if(gameObject.tag != "PoderSonic" && gameObject.tag != "Meg" && gameObject.tag != "Meg2")
            {
                Destroy(gameObject);
            }

        }
    }


    private void OnDrawGizmosSelected()
    {
        //Dibujando un circulo gráficamente en el editor para mayor comodidad a la hora de manipular el rango de ataque.

        Gizmos.DrawWireSphere(transform.position, rangoDisparo2);
    }
}
