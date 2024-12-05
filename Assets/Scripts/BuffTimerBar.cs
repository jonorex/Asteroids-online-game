using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffTimerBar : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imgBuff, bar;
    public Sprite Buff;
    public string buffStr;

    //public Sprite Explosion, X3, Missale;


    float BuffTime = 10f, timeCount = 0f;
    //public string _buff = "";
    Image img;
    
    void Start()
    {
        
        gameObject.SetActive(true);
        img = imgBuff.gameObject.GetComponent<Image>();
        img.sprite = Buff;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount <=  BuffTime) {
            timeCount += Time.deltaTime;
            bar.transform.localScale = new Vector3(1-0.1f*timeCount,1,1);
        } else {
            gameObject.SetActive(false);
            ResetPlayer();
        }
    }

    void ResetPlayer() {
        switch (buffStr) {
            case "ExplosionBuff":
                PlayerController.instance.ButtonExplosion.SetActive(false);
                PlayerController.instance.BtnExplosionAnim.SetActive(false);
                break;
            case "x3Buff":
                PlayerController.instance.lFire.SetActive(false);
                PlayerController.instance.rFire.SetActive(false);
                break;
            case "MissileBuff":
                PlayerController.instance.missaleBuff = false;
                break;
        }
    }



    public void ResetBuff() {
        timeCount = 0f;
        gameObject.SetActive(true);
    }

}
