using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAsteroid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.layer == 7) {
            Destroy(collision.gameObject);
            AudioController.instance.StrikeAsteroidSfx(gameObject.tag, transform);
            gameObject.SetActive(false);
        }
    }
}
