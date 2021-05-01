using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player2 : MonoBehaviour
{
    //Lista:

    public GameObject Proyectil;

    //Variables publicas:

    public bool piso;
    public int vida = 1000;
    public int vidaActual;
    public int dañoAtaque;
    public int fuerzaAtaque = 150;
    public int fuerzaSalto = 300;
    public float Velocidad = 200;
    public float velocidadSonic2 = 600;
    public float vMax = 0;
    public int energiaMaxima2 = 100;
    public int energiaActual2;
    public float tiempo = 0.10f;
    public float rangoAtaque;
    public int rangoEnergia2 = 30;
    public float esperaDisparar2 = 2f;
    public float esperaAtaque2 = 2f;
    public BarraEnergia2 barraEP2;
    public BarraDeVidaP2 barrap2;
    public GameObject barra2;
    public BarraDeVidaP2 barraP2;
    public Transform areaDisparo2;
    public Transform circuloAtaque;
    public LayerMask enemigoP1;

    //Variables privadas:

    private GameObject barraE2;
    public Transform objetivoSonic2;
    public GameObject objetoObjetivoSonic2;
    private GameObject Jugador2;
    private float poderDisparar2 = 0f;
    private float poderAtacar = 0f;
    private bool DPresionada = false;
    private bool transformacionIzq = true;
    private bool transformacionDer = true;
    private bool salto2;
    private bool salto1;
    private GameObject player2;
    private Animator animacion;
    private Rigidbody2D fuerzas;
   

    void Start()
    {
        /*
            -------------------------------------------------------------------------
           |ASIGNANDO A LAS VARIABLES UN COMPONENTE OBTENIDO DE SUS RESPECTIVOS TIPOS|
            -------------------------------------------------------------------------
        */

        animacion = GetComponent<Animator>();
        fuerzas = GetComponent<Rigidbody2D>();
        
        //Buscando un objeto dentro de la escena con el nombre 'Player2'.
        
        Jugador2 = GameObject.Find("Player2");

        //Buscando en la escena el gameobject que sirve como objetivo para el poder de sonic.

        objetoObjetivoSonic2 = GameObject.Find("SonicObjetivo");

        //Asignandole a 'objetivoSonic' el gameobject encontrado.

        objetivoSonic2 = objetoObjetivoSonic2.transform;

        //Buscando en la escena el game object de la barra de vida 'P2'.

        barra2 = GameObject.Find("P2barra");

        //Asignandolé un componente de tipo 'BarraDeVidaP2' del objeto encontrado.

        barraP2 = barra2.GetComponent<BarraDeVidaP2>();

        //Buscando en la escena el game object de la barra de energía 'P2'.

        barraE2 = GameObject.Find("BarraEP2");

        //Asignandolé un componente de tipo 'BarraEnergia2' del objeto encontrado.

        barraEP2 = barraE2.GetComponent<BarraEnergia2>();

        //Asignandolé el valor de 'vida' a 'vidaActual'.

        vidaActual = vida;

        //Asignandolé el valor de 'energiaMaxima2' a 'energiaActual2'.

        energiaActual2 = energiaMaxima2;

        //Invocando el método 'SaludMaxima2' y agregandolé como parametro la variable 'vida'.
       
        barraP2.SaludMaxim2(vida);

        //Invocando el método 'EnergiaMaxima2' y agregandole como parametro la variable 'energiaMaxima2'.

        barraEP2.EnergiaMaxima2(energiaMaxima2);
    }

    void Update()
    {
        //Agregando el parametro de 'Piso' y 'Velocidad' a las variables creadas para estos por medio de la variable 
        //'animcion' con '.SetBool' (esta última es de tipo verdadero o falso).

        animacion.SetBool("Piso", piso);
        animacion.SetFloat("Velocidad", Mathf.Abs(fuerzas.velocity.x));

        //Movimiento salto entrada.

        MovSalto();

        //Movimiento horizontal entrada.

        MovHorizontal();

        //Ataque Entrada.

        AtaqueEntrada();
    }

    void FixedUpdate()
    {
        //Agregando método de movimiento horizontal.

        FuerzaHorizontal();

        //Agregando método de salto.

        FuerzaSalto();
    }

    //Función para dibujar un circulo en el editor correspondiente a un objeto.
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

    /*
        -------------
       |MUERTE MÉTODO|   
        -------------
    */
    public void MuerteP2()
    {
        //Activando la animación de muerte.

        animacion.SetBool("Muerte", true);

        //Destruyendo el spawn del jugador.

        Destroy(player2);

        //Desabilitando el script.

        this.enabled = false;
    }

    /*
        -----------
       |DAÑO MÉTODO|   
        -----------
    */
    public void DañoP2(int daño)
    {
        //Restandole a la vida actual el daño provocado usando el parametro entero 'daño'.

        vidaActual -= daño;

        barraP2.Salud2(vidaActual);

        //Activando la animación de daño.

        animacion.SetTrigger("Daño");

        //Evaluando si la vida actual es igual o menor a cero.

        if (vidaActual <= 0)
        {
            MuerteP2();
        }
    }

    /*
        -------------
       |ATAQUE MÉTODO|   
        -------------
    */
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

            //Declarando una variable Vector3 'dirección' y asignandole la distancia que tenemos con respecto al enemigo.
            //Lo anterior mencionado lo hacemos restando la posición actual del enemigo con la nuestra y lo almacenamos.

            Vector3 direccion = enemigo.transform.position - transform.position;

            //Obteniendo el componente 'Rigidbody2D' del enemigo y añadiendole una fuerza de ataque.
            //Y, sumando a lo anterior, multiplicandolo por la dirección. 

            enemigo.GetComponent<Rigidbody2D>().AddForce(direccion * fuerzaAtaque);
        }
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

            fuerzas.AddForce(new Vector2(0, fuerzaSalto));

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

    /*
        -----------------------------
       |MOVIMIENTO HORIZONTAL ENTRADA|
        -----------------------------
    */
    public void MovHorizontal()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && transformacionIzq == true)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            Jugador2.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            animacion.SetBool("Quieto", false);

            transformacionIzq = false;
            transformacionDer = true;
            DPresionada = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && transformacionDer == true)
        {
            if (DPresionada == true)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                Jugador2.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                animacion.SetBool("Quieto", false);

                transformacionDer = false;
                transformacionIzq = true;
            }
            else
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
                animacion.SetBool("Quieto", false);

                transformacionDer = false;
                transformacionIzq = true;
            }
        }
    }

    /*
        -------------
       |SALTO ENTRADA|
        -------------
    */
    public void MovSalto()
    {
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
    }

    /*
        --------------
       |ATAQUE ENTRADA|
        --------------
    */
    public void AtaqueEntrada()
    {
        /*
            --------------
           |ATAQUE ENTRADA|
            --------------
        */

        if (poderAtacar <= Time.time)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                AtaqueP2();

                poderAtacar = Time.time + 1f / esperaAtaque2;
            }
            else if (Input.GetKeyDown(KeyCode.O) && barraEP2.barraEnergia2.value >= rangoEnergia2 && poderDisparar2 <= Time.time)
            {
                animacion.SetTrigger("Poder");

                Disparar2();

                poderDisparar2 = Time.time + 1f / esperaDisparar2;

                Energia2(rangoEnergia2);
            }
        }
    }

    /*
        --------------
       |MÉTODO ENERGÍA|
        --------------
    */

    public void Energia2(int perdida)
    {
        energiaActual2 -= perdida;

        barraEP2.EnergiaActual2(energiaActual2);
    }

    /*
        --------------
       |PODER GENERICO|
        --------------
    */
    public void Disparar2()
    {
        if (gameObject.tag == "Minato")
        {
            StartCoroutine("EsperaMinato");

            EsperaMinato();
        }
        else if (gameObject.tag == "Zabuza")
        {
            StartCoroutine("EsperaZabuza");

            EsperaZabuza();
        }
        else if (gameObject.tag == "Ninja")
        {
            StartCoroutine("EsperaNinja");

            EsperaNinja();
        }
        else if (gameObject.tag == "Ryu")
        {
            StartCoroutine("EsperaRyu");

            EsperaRyu();
        }
        else if (gameObject.tag == "Zero")
        {
            StartCoroutine("EsperaZero");

            EsperaZero();
        }
        else if (gameObject.tag == "Sub Zero")
        {
            StartCoroutine("EsperaSubZero");

            EsperaSubZero();
        }
        else if (gameObject.tag == "Sonic")
        {
            StartCoroutine("EscalarSonic");
            StartCoroutine("EsperaSonic");

            EsperaSonic();
            EscalarSonic();
            Poder2();
        }
        else if (gameObject.tag == "Megaman")
        {
            StartCoroutine("EsperaMegaman");

            EsperaMegaman();
        }
    }

    IEnumerator EsperaMinato()
    {
        yield return new WaitForSeconds(0.35f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }

    IEnumerator EsperaZabuza()
    {
        yield return new WaitForSeconds(1f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }

    IEnumerator EsperaNinja()
    {
        yield return new WaitForSeconds(0.12f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }

    IEnumerator EsperaRyu()
    {
        yield return new WaitForSeconds(0.10f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }

    IEnumerator EsperaZero()
    {
        yield return new WaitForSeconds(0.27f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }

    IEnumerator EsperaSubZero()
    {
        yield return new WaitForSeconds(0.27f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }

    IEnumerator EsperaSonic()
    {
        yield return new WaitForSeconds(3.15f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }

    IEnumerator EscalarSonic()
    {
        yield return new WaitForSeconds(3.15f);

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    IEnumerator EsperaMegaman()
    {
        yield return new WaitForSeconds(0.59f);

        Instantiate(Proyectil, areaDisparo2.transform.position, areaDisparo2.transform.rotation);
    }
    /*
        -----------
       |PODER SONIC|
        -----------
    }
    */
    public void Poder2()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        float escalar = velocidadSonic2 * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, objetivoSonic2.position, escalar);
    }

    //Funición para verificar cuando nuestro personaje (en este caso) colisiona con 'x' cosa.

    void OnCollisionStay2D(Collision2D colision)
    {
        //Evaluando si chocamos con los limites del mapa.

        if (colision.gameObject.tag == "Limites")
        {
            vidaActual = vidaActual - 30;

            transform.position = new Vector3(0, 6, transform.position.z);
        }

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

    
    public void Desactivar()
    {
        this.enabled = false;
    }

    public void Activar()
    {
        this.enabled = true;
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
