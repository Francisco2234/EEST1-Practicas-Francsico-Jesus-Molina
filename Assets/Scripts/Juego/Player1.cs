using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1 : MonoBehaviour
{
    //Lista:

    public GameObject Proyectil;

    //Variables publicas:

    public bool piso;
    public int dañoAtaque = 3;
    public int fuerzaAtaque = 150;
    public int fuerzaSalto = 300;
    public int energiaMaxima = 100;
    public int energiaActual;
    public int vida = 1000;
    public int vidaActual;
    public float Velocidad = 200;
    public float velocidadSonic = 600;
    public float vMax = 0;
    public float rangoAtaque = 0;
    public float tiempo = 0.10f;
    public int rangoEnergia = 30;
    public float esperaAtaque = 8f;
    public float esperaDisparar = 2f;
    public BarraEnergia1 barraEP1;
    public BarraDeVidaP1 barraP1;
    public Transform areaDisparo1;
    public Transform circuloAtaque1;
    public LayerMask enemigoP2;

    public GameObject camara;
    public Transform cam;

    //Variables privadas:

    private GameObject barraE1;
    private GameObject barra1;
    private Transform objetivoSonic;
    private GameObject objetoObjetivoSonic;
    private GameObject Jugador;
    private float poderDisparar = 0f;
    private float poderAtacar = 0f;
    private bool DPresionada = false;
    public bool transformacionIzq = true;
    public bool transformacionDer = false;
    private bool salto2;
    private bool salto1;
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

        //Buscando en la escena el gameobject spawn del player1.

        Jugador = GameObject.Find("Player1");

        //Buscando en la escena el gameobject que sirve como objetivo para el poder de sonic.

        objetoObjetivoSonic = GameObject.Find("SonicObjetivo");

        //Asignandole a 'objetivoSonic' el gameobject encontrado.

        objetivoSonic = objetoObjetivoSonic.transform;

        //Buscando en la escena el game object de la barra de vida 'P1'.

        barra1 = GameObject.Find("P1barra");

        //Asignandolé un componente de tipo 'BarraDeVidaP1' del objeto encontrado.

        barraP1 = barra1.GetComponent<BarraDeVidaP1>();

        //Buscando en la escena el game object de la barra de energía 'P1'.

        barraE1 = GameObject.Find("BarraEP1");

        //Asignandolé un componente de tipo 'BarraEnergia1' del objeto encontrado.

        barraEP1 = barraE1.GetComponent<BarraEnergia1>();

        //Asignandolé el valor de 'vida' a 'vidaActual'.

        vidaActual = vida;

        //Asignandolé el valor de 'energiaMaxima' a 'energiaActual'.

        energiaActual = energiaMaxima;

        //Invocando el método 'SaludMaxima' y agregandolé como parametro la variable 'vida'.

        barraP1.SaludMaxima(vida);

        //Invocando el método 'EnergiaMaxima' y agregandole como parametro la variable 'energiaMaxima'.

        barraEP1.EnergiaMaxima(energiaMaxima);

        camara = GameObject.Find("Camera");

        cam = camara.GetComponent<Camera>().transform;
    }

    void Update()
    {
        //Agregando el parametro de 'Piso' y 'Velocidad' a las variables creadas para estos por medio de la variable 
        //'animcion' con '.SetBool' (esta última es de tipo verdadero o falso).

        animacion.SetBool("Piso", piso);
        animacion.SetFloat("Velocidad", Mathf.Abs(fuerzas.velocity.x));

        //Movimiento Salto Método.

        SaltoEntrada();

        //Movimiento Horizontal Método.

        MovEntrada();

        //Ataque entrada Método.

        AtaqueEntrada();
    }

    /*
        -------------
       |SALTO ENTRADA|
        -------------
    */
    public void SaltoEntrada()
    {
        if (Input.GetKeyDown(KeyCode.W))
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
        //Evaluando si poder atacar es menor que el tiempo para realizar esta acción.

        if (poderAtacar <= Time.time)
        {
            //Evaluando si se presionó la tecla 'J'.

            if (Input.GetKeyDown(KeyCode.J))
            {
                //Invocando al método 'AtaqueP1'.

                AtaqueP1();

                //Sumandole al tiempo uno más y lo dividopor 'esperaAtaque' y todo esto lo asigno a 'poderAtacar'.

                poderAtacar = Time.time + 1f / esperaAtaque;
            
                //Evaluo si se presionó la tecla 'U' y si la longitud actual de la barra de energia es mayo igual a rangoEnergía.
            }
            else if (Input.GetKeyDown(KeyCode.U) && barraEP1.barraEnergia.value >= rangoEnergia && poderDisparar <= Time.time)
            {
                //Activo la animación de poder.

                animacion.SetTrigger("Poder");

                //Invocando al métoo 'Disparar'.

                Disparar();

                //poderDispara espera.

                poderDisparar = Time.time + 1f / esperaDisparar;

                //Método Energia.

                Energia(rangoEnergia);
            }

        }
    }

    /*
        --------------
       |MÉTODO ENERGÍA|
        --------------

    */ 
    public void Energia(int Perdida)
    {
        //Restandole a 'energiaActual' el parámetro 'Perdida'.
        
        energiaActual -= Perdida;

        //Invocando el método 'EnergiaActual' del script 'BarraEnergia1' y agregandole 'energiaActual' como parámetro.

        barraEP1.EnergiaActual(energiaActual);
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
       |ATAQUE MÉTODO|   
        -------------
    */
    void AtaqueP1()
    {
        //Activando animación de ataque.

        animacion.SetTrigger("Ataque");

        //Agregando una variable del tipo 'Colider2D' y asignandolé  Un circulo apartir de la posición del objeto que elegí
        //Agregandole 'rangoAtaque' y a que tipo de mascara de objeto afecta esto.

        Collider2D[] colisionEnemigo = Physics2D.OverlapCircleAll(circuloAtaque1.position, rangoAtaque, enemigoP2);

        //Iterando en 'colisionEnemigo' y guardandolo en 'enemigo'.

        foreach(Collider2D enemigo in colisionEnemigo)
        {
            //Obteniendo un componente 'Player2' de 'enemigo'
            //llamando a el método 'DañoP2' y agregandole como parametro daño el 'dañoAtaque'.

            enemigo.GetComponent<Player2>().DañoP2(dañoAtaque);

            //Declarando una variable Vector3 'dirección' y asignandole la distancia que tenemos con respecto al enemigo.
            //Lo anterior mencionado lo hacemos restando la posición actual del enemigo con la nuestra y lo almacenamos.

            Vector3 direccion = enemigo.transform.position - transform.position;

            //Obteniendo el componente 'Rigidbody2D' del enemigo y añadiendole una fuerza de ataque.
            //Y, sumando a lo anterior, multiplicandolo por la dirección. 

            enemigo.GetComponent<Rigidbody2D>().AddForce(direccion *fuerzaAtaque);
        }
    }

    /*
        -----------
       |DAÑO MÉTODO|   
        -----------
    */
    public void DañoP1(int daño)
    {
        //Restandole a la vida actual el daño provocado usando el parametro entero 'daño'.

        vidaActual -= daño;

        //Invocando el método 'Salud' del script 'BarraEnergia1' y agregandole 'vidaActual' como parámetro.

        barraP1.Salud(vidaActual);

        //Activando la animación de daño.

        animacion.SetTrigger("Daño");

        //Evaluando si la vida actual es igual o menor a cero.

        if (vidaActual <= 0)
        {
            //Método 'MuerteP1'.

            MuerteP1();
        }
    }

    /*
        -------------
       |MUERTE MÉTODO|   
        -------------
    */
    public void MuerteP1()
    {
        //Activando la animación de muerte.

        animacion.SetBool("Muerte", true);

        //Desabilitando el script.

        this.enabled = false;
    }

    private void OnDrawGizmosSelected()
    {
        //Evaluando si hay un objeto correspondiente a 'circuloAtaque'.

        if (circuloAtaque1 == null)
        {
            //Retornando.

            return;
        }

        //Dibujando un circulo gráficamente en el editor para mayor comodidad a la hora de manipular el rango de ataque.

        Gizmos.DrawWireSphere(circuloAtaque1.position, rangoAtaque);
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
        //Creando una varable floato y asignandole una acción tomada desde el eje horizontal.

        float ejeX = Input.GetAxis("Horizontal");

        //Añadiendo fuerzas con un vecto2.righty multiplicandolo por la 'Velocidad' y la variable 'ejeX'.

        fuerzas.AddForce(Vector2.right * Velocidad * ejeX);

        //Declarando 'limite' y asignandole la funcion clamp que a su vez le introdusco la velocidad y las variables minimas y maximas.

        float limite = Mathf.Clamp(fuerzas.velocity.x, -vMax, vMax);

        //Asignandole a 'fuerzas.velocidad' con un nuevo vector2 el limite y dejando la fuerza actual que tiene en el eje 'Y'.

        fuerzas.velocity = new Vector2(limite, fuerzas.velocity.y);
    }

    /*
        -----------------------------
       |MOVIMIENTO HORIZONTAL ENTRADA|
        -----------------------------
    */
    public void MovEntrada()
    {
        //Evaluando si se presiono la 'A' o la 'B' y que la trasnformaciónIzq es 'true'.

        if (Input.GetKeyDown(KeyCode.A) && transformacionIzq == true)
        {
            //asignando dentro de la escala local una nueva escala con ayuda de un vector2.

            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

            //Asignando una nueva escala local para el spawn.

            Jugador.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

            //Desactivando la animación 'Quieto'.

            animacion.SetBool("Quieto", false);

            //Desactivando la transformación izquierda y activando la derecha y la D presionada.

            transformacionIzq = false;
            transformacionDer = true;
            DPresionada = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) && transformacionDer == true)
        {
            //Evaluando si la tecla 'D' no fue precionada.

            if (DPresionada == true)
            {
                //Haciendo lo contrario al anterior.

                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                Jugador.transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
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
        --------------
       |PODER GENERICO|
        --------------
    */
    public void Disparar()
    {
        //Evaluando los distintos tags de los posibles gameobjects y sus respectivas acciones de tiempodependiendo del personaje.

        if(gameObject.tag == "Minato")
        {
            StartCoroutine("EsperaMinato");

            EsperaMinato();
        }
        else if(gameObject.tag == "Zabuza")
        {
            StartCoroutine("EsperaZabuza");

            EsperaZabuza();
        }
        else if(gameObject.tag == "Ninja")
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
            Poder();
        }
        else if(gameObject.tag == "Megaman")
        {
            StartCoroutine("EsperaMegaman");

            EsperaMegaman();
        }
    }

    /*
         -------------------
        |FUNCIONES DE ESPERA|
         -------------------
    */
    IEnumerator EsperaMinato()
    {
        yield return new WaitForSeconds(0.35f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    IEnumerator EsperaZabuza()
    {
        yield return new WaitForSeconds(1f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    IEnumerator EsperaNinja()
    {
        yield return new WaitForSeconds(0.12f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    IEnumerator EsperaRyu()
    {
        yield return new WaitForSeconds(0.10f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    IEnumerator EsperaZero()
    {
        yield return new WaitForSeconds(0.27f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    IEnumerator EsperaSubZero()
    {
        yield return new WaitForSeconds(0.27f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    IEnumerator EsperaSonic()
    {
        yield return new WaitForSeconds(3.15f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    IEnumerator EscalarSonic()
    {
        yield return new WaitForSeconds(3.15f);

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    IEnumerator EsperaMegaman()
    {
        yield return new WaitForSeconds(0.59f);

        Instantiate(Proyectil, areaDisparo1.transform.position, areaDisparo1.transform.rotation);
    }

    /*
        -----------
       |PODER SONIC|
        -----------
    */
    public void Poder()
    {
        //Cambiando el valor de la gravedad a cero.

        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;

        //Inicializando una variable del tipo float y asignandolé la velocidad por el tiempo.

        float escalar = velocidadSonic * Time.deltaTime;

        //transformando mi posición a la de un nuevo vector tres el cual le otorgo mi posición, laposición donde quiero dirigirme y la variable float.

        transform.position = Vector3.MoveTowards(transform.position, objetivoSonic.position, escalar);
    }

    //Funición para verificar cuando nuestro personaje (en este caso) colisiona con 'x' cosa.

    void OnCollisionStay2D(Collision2D colision)
    {
        //Evaluando si chocamos con los limites del mapa.

        if(colision.gameObject.tag == "Limites")
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

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                animacion.SetBool("Correr", true);

            }
        }

        if (colision.gameObject.tag == "Piso" && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            animacion.SetBool("Correr", true);
            animacion.SetBool("Quieto", false);
        }

        if (colision.gameObject.tag == "Piso" && Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            animacion.SetBool("Correr", false);
            animacion.SetBool("Quieto", true);
        }
    }

    //Método desactivar script para el ataque de sub zero.

    public void Desactivar()
    {
        this.enabled = false;
    }

    //Método desactivar script para el ataque de sub zero.

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


 


  

