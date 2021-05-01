using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectiles : MonoBehaviour
{
    public LayerMask enemigoP2;

    public float rangoDisparo;

    public GameObject Jugador;

    public int dañoBala = 3;

    public int fuerzaBala = 150;

    public Transform JugadorTransform;

    private Rigidbody2D fuerzas;

    public float proyectilVelocidad;

    public float proyectilVida;


    void Awake()
    {
        //Obteniendo los componentes necesarios antes de que inicie la escena.

        fuerzas = GetComponent<Rigidbody2D>();

        Jugador = GameObject.Find("Player1");

        JugadorTransform = Jugador.transform;
    }
    void Start()
    {
        //Evaluando si el objeto tiene el tag de Sonic.

        if (gameObject.tag == "PoderSonic")
        {
            //Agregandole fuerza negativa en 'Y' al proyectil.

            fuerzas.velocity = new Vector2(0, -proyectilVelocidad);
        }
        else
        {
            //Evaluar si la escala del jugador es negativa o positiva para la dirección de la bala.

            if (Jugador.transform.localScale.x < 0)
            {
                //Asignandole a 'fuerzas.velocity' un vector con  'proyectilVelocidad' en 'X'.

                fuerzas.velocity = new Vector2(proyectilVelocidad, fuerzas.velocity.y);

                //Asignandole a 'transform.localScale' un vector3.

                transform.localScale = new Vector3(3.377424f, 4.5f, 1);

                //Método disparar.

                Disparar();

            }
            else
            {
                fuerzas.velocity = new Vector2(-proyectilVelocidad, fuerzas.velocity.y);
                transform.localScale = new Vector3(-3.377424f, -4.5f, 1);
                Disparar();
            }
        }
        
    }
    void Update()
    {
        //Destruyendo el objeto con el tiempo que le puse.

        Destroy(gameObject, proyectilVida);

        //Método disparar.

        Disparar();
    }

    public void Disparar()
    {
        Collider2D[] colisionEnemigo = Physics2D.OverlapCircleAll(transform.position, rangoDisparo, enemigoP2);

        //Iterando en 'colisionEnemigo' y guardandolo en 'enemigo'.

        foreach (Collider2D enemigo in colisionEnemigo)
        {
            //Obteniendo un componente 'Player2' de 'enemigo'
            //llamando a el método 'DañoP2' y agregandole como parametro daño el 'dañoAtaque'.

            enemigo.GetComponent<Player2>().DañoP2(dañoBala);


            //Declarando una variable Vector3 'dirección' y asignandole la distancia que tenemos con respecto al enemigo.
            //Lo anterior mencionado lo hacemos restando la posición actual del enemigo con la nuestra y lo almacenamos.

            float direccion = enemigo.transform.position.x - transform.position.x;

            //Obteniendo el componente 'Rigidbody2D' del enemigo y añadiendole una fuerza de ataque.
            //Y, sumando a lo anterior, multiplicandolo por la dirección. 

            enemigo.GetComponent<Rigidbody2D>().AddForce(new Vector3(direccion * fuerzaBala, enemigo.transform.position.y, enemigo.transform.position.z));

            if (gameObject.tag == "SubZero")
            {
                enemigo.GetComponent<Rigidbody2D>().gravityScale = 0;

                enemigo.GetComponent<Rigidbody2D>().mass = 60;

                enemigo.GetComponent<Player2>().Desactivar();

                gameObject.GetComponent<SpriteRenderer>().enabled = false;

                StartCoroutine(EsperaActivar());

                IEnumerator EsperaActivar()
                {
                    yield return new WaitForSeconds(5f);

                    enemigo.GetComponent<Player2>().Activar();

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

        Gizmos.DrawWireSphere(transform.position, rangoDisparo);
    }
}
