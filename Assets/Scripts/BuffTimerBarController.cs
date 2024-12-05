using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTimerBarController : MonoBehaviour
{

   
    public GameObject Te, Tx, Tm;

    public static BuffTimerBarController instance;

    Dictionary<string, GameObject> spriteDict = new Dictionary<string, GameObject>();


    // Start is called before the first frame update

    void Awake() {
        instance = this;
    }

    void Start()
    {
        spriteDict.Add("ExplosionBuff", Te);
        spriteDict.Add("x3Buff", Tx);
        spriteDict.Add("MissileBuff", Tm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetBuffTimer(string buff) {
        
        GameObject go = spriteDict[buff];
        
        go.GetComponent<BuffTimerBar>().ResetBuff(); 
    }  

    public void EndBuffTimer() {
        Te.SetActive(false);
    } 
}
