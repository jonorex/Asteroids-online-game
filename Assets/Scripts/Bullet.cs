using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;  
    public Rigidbody2D rig;


    void FixedUpdate()
    {
        rig.MovePosition(transform.position + transform.up * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
       // if(collision.gameObject.tag == "Player") {
            Destroy(gameObject);
       // }
    }
}
