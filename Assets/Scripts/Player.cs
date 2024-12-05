using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject explosionFragment;
    
    ArrayList explosion;

    void Start()
    {
        explosion = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Explosion() {
        for(int i = 0; i < 180; i+=10) {
            GameObject go = Instantiate(explosionFragment);
            go.transform.Rotate(0,0,i);
            explosion.Add(go);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "AsterBig1") {
            Debug.Log("Bateu");
        }
        
        else if (collision.gameObject.name ==  "ExplosionBuff") {
            Destroy(collision.gameObject);
            Explosion();
        }
    }
   
}
