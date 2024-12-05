using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    float sirenTime = 5f;
    float timeCount;

    // Update is called once per frame
    void Update()
    {
//        Debug.Log(timeCount);
        if(timeCount >= sirenTime) {
            AudioController.instance.SirenSfx(transform);
            
            timeCount = 0;
        }
        timeCount += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 9) {
            AudioController.instance.StrikeAsteroidSfx(collision.gameObject.tag, transform);
            
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
        } else if(collision.gameObject.layer == 7) {
            AudioController.instance.StrikeAsteroidSfx("AsterBig1", transform);
            GameController.instance.SumScore(gameObject.tag);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

    }
}
