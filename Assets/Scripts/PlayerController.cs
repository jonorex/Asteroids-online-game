using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public FloatingJoystick Joystick;
    public FixedJoystick j;
    Rigidbody2D rb;
    Vector2 move;
    public float moveSpeed;

    bool exploasionActive = false;

    public static bool PointerDown = true;

    public float timeExp = .5f;

    public GameObject explosionFragment, lFire, rFire, ButtonExplosion, BtnExplosionAnim, Floating_JoystickReference;
    
    public static PlayerController instance;
    private float engineTime = 3f;

    public bool missaleBuff = false;

    bool ace = true;

    public Button btnAc;

    public Sprite AcSprite, StopSprite;
    private Vector3 lastMousePosition; // Armazena a última posição do mouse
    

     // Defina os limites do mapa 
    public float radius = 500f;

    public Vector3 centerPosition = Vector3.zero;

    ParticleSystem pS;


    
    private Vector2 movement;

    private void Awake() {
        instance = this;
    }

    private void Start(){

        rb = GetComponent<Rigidbody2D> ();
        rb.gravityScale = 0;
        AudioController.instance.PlayEngineSound(transform);

        // Inicializa a posição do mouse
        lastMousePosition = Input.mousePosition;


        // Limpa quaisquer listeners existentes (opcional)
        ButtonExplosion.GetComponent<Button>().onClick.RemoveAllListeners();

            // Adiciona o método ao evento onClick
        ButtonExplosion.GetComponent<Button>().onClick.AddListener(Explosion);
    }

    private void Update() {
        /*
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;

        //rotation

        float hAxis = move.x;
        float vAxis = move.y;
        float zAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, 0f, -zAxis);
        transform.rotation.Set(0f,0f,-zAxis,0f);
        
        //pS.transform.rotation = transform.rotation;
        float a = transform.rotation.z;
        //transform.Rotate(new Vector3(0f, 0f, -zAxis));

        
*/

        // Obtém entrada do teclado
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        ReplacePosition.Instance.VerifyPosition(transform);
        if (Input.GetMouseButtonDown(0))
        {
            transform.GetChild(3).gameObject.GetComponent<spawn>().Ataque();
            transform.GetChild(2).gameObject.GetComponent<spawn>().Ataque();
            transform.GetChild(1).gameObject.GetComponent<spawn>().Ataque();
            
        }


    // Verifica se o mouse foi movido
        if (Input.mousePosition != lastMousePosition)
        {
            // Atualiza a rotação da nave para apontar para a posição atual do mouse
            RotateTowardsMouse();

            // Atualiza a última posição do mouse
            lastMousePosition = Input.mousePosition;

            
        }


        if (Input.GetKeyDown(KeyCode.R)) {
            if (exploasionActive) {
                Explosion();
                exploasionActive = false;
            }

        }



        

        

    }

    private void FixedUpdate() {    

        // Aplica a movimentação ao Rigidbody2D
        rb.velocity = movement * moveSpeed;
        
        
/*
        // Obter a entrada de movimento do jogador
        float moveHorizontal = Input.GetAxis("Horizontal"); // -1 para esquerda, 1 para direita
        float moveVertical = Input.GetAxis("Vertical");     // -1 para baixo, 1 para cima

        // Definir a velocidade do movimento
        Vector2 movement = new Vector2(moveHorizontal, moveVertical) * moveSpeed;

        if (movement.x  != 0 || movement.y != 0) {
            ClientTCP.instance.SendMessageToServer($"MOVE {Players.instance.myId} {movement.x} {movement.y}");
        }

        // Aplicar movimento à nave
        //rb.velocity = movement;
        
        //if (ace){
        //    engineTime+=Time.deltaTime;
        //    rb.MovePosition(transform.position + transform.up * moveSpeed * Time.fixedDeltaTime);
        //}

        */
        
    }





     void RotateTowardsMouse()
    {
        // Converte a posição do mouse para coordenadas do mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0f; // Define o eixo Z como 0, pois estamos em um ambiente 2D

        // Calcula o ângulo e aplica a rotação
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        
        
        

        
    }

    public void Acelerator() {
        btnAc.GetComponent<Image>().sprite =  ace ? AcSprite : StopSprite;
        ace = !ace;
    }

    

    public void Explosion() {
        for(int i = 0; i < 360; i+=10) {
            GameObject go = Instantiate(explosionFragment, transform);
            go.transform.Rotate(0,0,i);
            AudioController.instance.ShootSfx(go.transform);
            Destroy(go,5f);
            StartCoroutine(TimerCount());

        }
        ButtonExplosion.SetActive(false);
        BtnExplosionAnim.SetActive(false);
        BuffTimerBarController.instance.EndBuffTimer();
        
    }
    private IEnumerator TimerCount() {
        yield return new WaitForSeconds(timeExp);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "AsterBig1"
            || collision.gameObject.tag == "AsterMed1"
            || collision.gameObject.tag == "AsterSmall1" 
            || collision.gameObject.tag == "Enemy"
            || collision.gameObject.tag == "EnemyBullet") {
            AudioController.instance.StrikeAsteroidSfx("AsterBig1", transform);
            if (LifeBarController.Instance.Decrement()){
                //Time.timeScale = 0;
                //GameController.instance.ShowGameOver();
                ClientTCP.instance.GameOverMessage();
            }
                
            else {
                gameObject.layer = 6;
                //if (GameController.instance.isLoaded) {
                    //GameController.instance.isLoaded = false;
                    //Time.timeScale = 0;
                    //AdsManager.Instance.interstitial.ShowAd(); 
                //} else {
                    ShipAnimatorController.Instance.GhoastMode();
                    StartCoroutine(GameController.instance.EndGhoast());
               // }
            }
            
            //Destroy(collision.gameObject);
        }else if (collision.gameObject.tag == "ExplosionBuff") {
            //Debug.Log("Bateu");
            Destroy(collision.gameObject);
            exploasionActive = true;
            ButtonExplosion.SetActive(true);
            BtnExplosionAnim.SetActive(true);
            BuffTimerBarController.instance.SetBuffTimer(collision.gameObject.tag);
            //StartCoroutine(TimerExplosion());
            //Explosion();
        } else if(collision.gameObject.tag == "x3Buff") {
            lFire.SetActive(true);
            rFire.SetActive(true);
            //StartCoroutine(TimerCount());
            Destroy(collision.gameObject);
            BuffTimerBarController.instance.SetBuffTimer(collision.gameObject.tag);
        } else if(collision.gameObject.tag == "MissileBuff"){
            missaleBuff = true;
            //StartCoroutine(TimerCountMissele());
            Destroy(collision.gameObject);
            BuffTimerBarController.instance.SetBuffTimer(collision.gameObject.tag);
        } 
        
        //else if(collision.gameObject.tag == "EnemyBullet") {
        //    AudioController.instance.StrikeAsteroidSfx("AsterBig1", transform);
        //    Destroy(collision.gameObject);
        //    GameController.instance.ShowGameOver();
        //}
    }



    //private IEnumerator TimerCount() {
    //    yield return new WaitForSeconds(10f);
    //    lFire.SetActive(false);
    //    rFire.SetActive(false);
    //}
//
    //private IEnumerator TimerCountMissele() {
    //    yield return new WaitForSeconds(10f);
    //    missaleBuff = false;
    //}
//
    //private IEnumerator TimerExplosion() {
    //    yield return new WaitForSeconds(10f);
    //    ButtonExplosion.SetActive(false);
    //    BtnExplosionAnim.SetActive(false);
    //}
}
