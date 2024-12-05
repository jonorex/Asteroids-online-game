using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyAsters : MonoBehaviour
{

   // public GameObject spawns;

     int spawnCount;
     Transform TRANSFORM;

    public static SpawnEnemyAsters Instance;

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
        spawnCount = transform.childCount;
        TRANSFORM = transform;

    }

    public  void Spawn(GameObject prefab) {
        int r = Random.Range(0,spawnCount-1) ;
        
        Transform goT= TRANSFORM.GetChild(r);

        GameObject go = Instantiate(prefab, goT.position, goT.rotation);
        go.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, 1f);
        go.GetComponent<Asteroid>().color = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
