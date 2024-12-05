using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rig;
    public float speed;

    public GameObject prefab;

    public bool color=false;

    public Transform uPRef, BotRef, RightRef, LeftRef;

    private Vector3 newP;

    

    bool timeCount = false;

    int thisId;




    // Start is called before the first frame update


    void Start()
    {

        thisId =  SpawnController.instance.AddAster();
        //thisId = SpawnController.instance.AddId();
        transform.Rotate(new Vector3(0, 0, Random.Range(-145, -45)));
        StartCoroutine(TimerCount());
        //SpawnController.instance.ids.Add(gameObject);


    }

    private IEnumerator TimerCount()
    {
        yield return new WaitForSeconds(5f);
        timeCount = true;
    }

    void Update()
    {
        if (timeCount)
        {
            ReplacePosition.Instance.VerifyPosition(transform);
            if (transform.position.y >= 5.82f)
            {
                newP = new Vector3(transform.position.x, -7.93f + 1, transform.position.z);
                //Destroy(gameObject);
                transform.position = newP;
            }
            else if (transform.position.y <= -7.93f)
            {
                newP = new Vector3(transform.position.x, 5.82f - 1, transform.position.z);
                //Destroy(gameObject);
                transform.position = newP;
            }
            else if (transform.position.x <= -10.27f)
            {
                newP = new Vector3(17.2f - 1, transform.position.y, transform.position.z);
                transform.position = newP;
                //Destroy(gameObject);
            }
            else if (transform.position.x >= 17.2f)
            {
                newP = new Vector3(-10.27f + 1, transform.position.y, transform.position.z);
                transform.position = newP;
                //Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        rig.MovePosition(transform.position + transform.right * speed * Time.deltaTime);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 7
            || collision.gameObject.tag == "Enemy"
            || collision.gameObject.layer == 11
            || collision.gameObject.layer == 8)
        {


            
                AudioController.instance.StrikeAsteroidSfx(gameObject.tag, transform);


                if (!color) {
                    GameController.instance.SumScore(gameObject.tag);
                    

                    if (Random.Range(0,100) > 90) {
                        GameController.instance.GenereateBuff(transform.position);
                    }
                }
                

                
                



                //var itemToRemove = SpawnController.instance.ids.FirstOrDefault(item => item.GetComponent<Asteroid>().thisId == thisId);


                GameController.instance.AsteroidsChildGenerator(transform.position, transform.rotation, gameObject.tag, color);
                
                SpawnController.instance.RemoveAster(thisId);

                Destroy(gameObject);
                
            






        }
    }

}
