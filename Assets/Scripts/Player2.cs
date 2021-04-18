using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    //Variables publicas:

    public bool piso;
    public float Velocidad = 150;
    public float vMax = 3;
    public int vida = 100;
    public int vidaActual;
    public int dañoAtaque;
    public Transform circuloAtaque;
    public float rangoAtaque;
    public LayerMask enemigoP1;

    //Variables privadas:

    private Animator animacion;
    private Rigidbody2D fuerzas;
    private bool salto2;
    private bool salto1;

    void Start()
    {
        /*
            -------------------------------------------------------------------------
           |ASIGNANDO A LAS VARIABLES UN COMPONENTE OBTENIDO DE SUS RESPECTIVOS TIPOS|
            -------------------------------------------------------------------------
        */

        animacion = GetComponent<Animator>();
        fuerzas = GetComponent<Rigidbody2D>();

        vidaActual = vida;
    }

    void AtaqueP2()
    {
        //Activando animación de ataque.

        animacion.SetTrigger("Ataque");

        //Agregando una variable del tipo 'Colider2D' y asignandolé  Un circulo apartir de la posición del objeto que elegí
        //Agregandole 'rangoAtaque' y a que tipo de mascara de objeto afecta esto.

        Collider2D[] colisionEnemigo = Physics2D.OverlapCircleAll(circuloAtaque.position, rangoAtaque, enemigoP1);

        //Iterando en 'colisionEnemigo' y guardandolo en 'enemigo'.

        foreach (Collider2D enemigo in colisionEnemigo)
        {
            //Obteniendo un componente 'Player2' de 'enemigo'
            //llamando a el método 'DañoP2' y agregandole como parametro daño el 'dañoAtaque'.

            enemigo.GetComponent<Player1>().DañoP1(dañoAtaque);
        }
    }

    public void DañoP2(int daño)
    {
        //Restandole a la vida actual el daño provocado usando el parametro entero 'daño'.

        vidaActual -= daño;

        //Activando la animación de daño.

        animacion.SetTrigger("Daño");

        //Evaluando si la vida actual es igual o menor a cero.

        if (vidaActual <= 0)
        {
            MuerteP2();
        }
    }

    public void MuerteP2()
    {
        //Activando la animación de muerte.

        animacion.SetBool("Muerte", true);

        //Desabilitando el script.

        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        //Evaluando si hay un objeto correspondiente a 'circuloAtaque'.

        if (circuloAtaque == null)
        {
            //Retornando.

            return;
        }

        //Dibujando un circulo gráficamente en el editor para mayor comodidad a la hora de manipular el rango de ataque.

        Gizmos.DrawWireSphere(circuloAtaque.position, rangoAtaque);
    }

    void Update()
    {
        //Agregando el parametro de 'Piso' y 'Velocidad' a las variables creadas para estos por medio de la variable 
        //'animcion' con '.SetBool' (esta última es de tipo verdadero o falso).

        animacion.SetBool("Piso", piso);
        animacion.SetFloat("Velocidad", Mathf.Abs(fuerzas.velocity.x));

        /*
            -------------
           |SALTO ENTRADA|
            -------------
        */


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Evaluando para el primer salto si está tocando el suelo.

            if (piso)
            {
                salto1 = true;
                salto2 = true;
            }

            //Evaluando para el segundo salto si se encuentra habilitado para realizarlo.

            else if (salto2)
            {
                salto1 = true;

                //Convirtiendo  'salto2' a 'false' para que no pueda realizar un tercer salto.

                salto2 = false;

                //Activando la animación del doble salto (o repitiendo la animación de salto).

                animacion.SetBool("Salto2", true);
            }
        }

        /*
               -----------------------------
              |MOVIMIENTO HORIZONTAL ENTRADA|
               -----------------------------
        */

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            animacion.SetBool("Quieto", false);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            animacion.SetBool("Quieto", false);
        }

        /*
            --------------
           |ATAQUE ENTRADA|
            --------------
        */

        if (Input.GetKeyDown(KeyCode.P))
        {
            AtaqueP2();
        }

    }

    void FixedUpdate()
    {
        //Agregando método de movimiento horizontal.

        FuerzaHorizontal();

        //Agregando método de salto.

        FuerzaSalto();
    }

    /*
        -------------
       |SALTO FUERZAS|   
        -------------
    */
    public void FuerzaSalto()
    {
        //inicializando condicional evaluando si 'salto1' es verdadero.

        if (salto1)
        {
            //Agregandole fuerza a la variable 'fuerzas' con ayuda de un 'vector2' (el cual pide X [En este caso 0] - Y [En este caso 300]).

            fuerzas.AddForce(new Vector2(0, 275));

            //Convirtiendo 'salto1' a 'false' para que no siga subiendo infinitamente y espere a que el jugador presione de nuevo para activarlo.

            salto1 = false;
        }
    }

    /*
        -----------------------------
       |MOVIMIENTO HORIZONTAL FUERZAS|
        -----------------------------
    */
    public void FuerzaHorizontal()
    {
        float ejeX = Input.GetAxis("MovHorizontal");

        fuerzas.AddForce(Vector2.right * Velocidad * ejeX);

        float limite = Mathf.Clamp(fuerzas.velocity.x, -vMax, vMax);

        fuerzas.velocity = new Vector2(limite, fuerzas.velocity.y);

        Debug.Log(fuerzas.velocity.x);
    }

    //Funición para verificar cuando nuestro personaje (en este caso) colisiona con 'x' cosa.

    void OnCollisionStay2D(Collision2D colision)
    {

        //Inicio un condicional preguntando si el 'tag' (especie de etiqueta para diferenciar de que tipo es cada objeto)
        //del 'gameobject' de la variable 'colision' es igual al tag del objeto el cual esta colisionando nuestro jugador.

        if (colision.gameObject.tag == "Piso")
        {
            //Cambiando piso a 'true' y la animación de salto y doble salto a 'false'.

            piso = true;
            animacion.SetBool("Salto", false);
            animacion.SetBool("Salto2", false);
            animacion.SetBool("Caída", false);

            //Evaluando si se mantiene precionada cualquier tecla de movimiento y cambiando 'correr' a true si es así.

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                animacion.SetBool("Correr", true);

            }


        }

        if (colision.gameObject.tag == "Piso" && Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            animacion.SetBool("Correr", true);
            animacion.SetBool("Quieto", false);
        }

        if (colision.gameObject.tag == "Piso" && Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            animacion.SetBool("Correr", false);
            animacion.SetBool("Quieto", true);
        }



    }

    //Funición para verificar cuando nuestro personaje no colisiona con 'x' cosa.

    void OnCollisionExit2D(Collision2D colision)
    {

        //Evaluando si estamos tocando el suelo y cambiando 'piso' a false y 'salto' a true en caso de que no sea así.

        if (colision.gameObject.tag == "Piso")
        {
            piso = false;

            animacion.SetBool("Salto", true);

            //Evaluando si las variables 'salto1' y 'salto2' son falsas y cambiando sus respectivas animaciones a false.
            //cambiando caída a true.

            if (salto2 == false && salto1 == false)
            {
                animacion.SetBool("Caída", true);
                animacion.SetBool("Salto", false);
                animacion.SetBool("Salto2", false);
            }
        }

        //Evaluando si esta tocando el piso y poniento 'correr' a false en caso de que no sea así.

        if (colision.gameObject.tag == "Piso")
        {
            animacion.SetBool("Correr", false);
        }


    }
}
