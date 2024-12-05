using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject Enemy;
    public float spawnTime;

    private float timeCount;
    int count_spawn;

    void Update()
    {
        if (count_spawn < spawnAsteroids.st2 && SpawnController.instance.ids.Count <= 6) {   
            timeCount += Time.deltaTime;
            Debug.Log("Enemy spawn");
            if(timeCount >= spawnTime)
            {   
                count_spawn++;
                GameObject go = Instantiate(Enemy, transform.position, transform.rotation);
                go.SetActive(true);
                timeCount = 0f;
            }
        }
        
    }
}
