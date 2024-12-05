using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsterBars : MonoBehaviour
{

    GameObject bar, enemyBar;

    public int asterCount, enemyAsterCount;
    public int totalEnemyAsters, totalMyAsters;

    public static AsterBars Instance;

    public Image img, imgEnemy;

    float c, ec;

    Transform barChild1, eBarchild2;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        barChild1 = transform.GetChild(0);
        eBarchild2 =  transform.GetChild(1);
        bar = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        enemyBar = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        
        foreach (int p in Players.instance.playersShips.Keys) {

           // Debug.Log($"nave do jogador p {p}: {Players.instance.playersShips[p]}" );
            if (p == Players.instance.myId)
                img.sprite = Constants.Instance.GetImage(Players.instance.playersShips[p]);
            else 
                imgEnemy.sprite = Constants.Instance.GetImage(Players.instance.playersShips[p]);
        }

        
        
        //SpawnController.instance.asterCount;
        
    }

    public void SetTotalEnemyAsters(int t) {
        totalEnemyAsters = t;
        updateBar();
    }

    public void SetTotalAsters(int t) {
        ClientTCP.instance.MessageSetTotalAsters(t);
        totalMyAsters = t;
        updateBar();
    }

    public void SetMyAsterCount(int c) {
        asterCount = c;
        ClientTCP.instance.MessageSetAsterCount(c);
        updateBar();
    }

    public void SetEnemyAsterCount(int count) {
        enemyAsterCount = count;
        //ClientTCP.instance.MessageSetAsterCount(c);
        updateBar();

        Debug.Log($"cond {ec>(c+.2f)}");

        GenerateEnemyAster(ec>(c+0.2f));
        //enemyAsterCount/(totalEnemyAsters+totalMyAsters) - asterCount/(totalEnemyAsters+totalMyAsters)
        
    }


    void GenerateEnemyAster(bool cond) {
        if (cond) {
            int r = Random.Range(0,100);

            if (r>68) {
                //grande
                SpawnEnemyAsters.Instance.Spawn(Constants.Instance.AsterBig);
            } else if (r>57) {
                //médio
                SpawnEnemyAsters.Instance.Spawn(Constants.Instance.AsterMed);
            } else {
                //pequeno
                SpawnEnemyAsters.Instance.Spawn(Constants.Instance.AsterSmall);
            }
        }
    }
  

    public void updateBar() {
        c = (float)(asterCount-enemyAsterCount)/(totalEnemyAsters+totalMyAsters);
        ec = (float)(enemyAsterCount-asterCount)/(totalEnemyAsters+totalMyAsters);
        bar.transform.localScale = new Vector3(1+c,1,1);
        enemyBar.transform.localScale = new Vector3(1+ec,1,1);
       // Debug.Log($"c {c} ec {ec}");
        

       
    }


    // Update is called once per frame
    void Update()
    {
        updateBar();
    }
}
