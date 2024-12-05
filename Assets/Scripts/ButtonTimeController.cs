using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class TimeBarController : MonoBehaviour
{
    // Start is called before the first frame update
    float buffTime = 10f, timeCount;
    public static TimeBarController buffTimer;

    void Start()
    {
        buffTimer = this;
    }

    

    // Update is called once per frame
    void Update()
    {   
        if(gameObject.activeSelf == true) {
            if(timeCount <= buffTime) {
                gameObject.transform.localScale = new Vector3(1-0.1f*timeCount, 1, 1);
            }

        timeCount += Time.deltaTime;
        }

        if(timeCount >= buffTime) {

            gameObject.SetActive(false);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
            
    }


    public void ActiveBuff(float buffTime) {
        gameObject.SetActive(true);
        this.buffTime = buffTime;
        timeCount = 0;
    }

}
