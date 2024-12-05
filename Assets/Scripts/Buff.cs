using UnityEngine;

public class Buff : MonoBehaviour
{
    public Rigidbody2D rig;
    public float speed;


    // Update is called once per frame
    void FixedUpdate() 
    {
        rig.MovePosition(transform.position + Vector3.down * speed * Time.deltaTime);
    }
}
