using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
public class spawnAsteroids : MonoBehaviour
{
    public GameObject prefabBig, prefabMed;

    public float spawnTime;
    private float timeCount;
    public int SPAWN_TIME = 0;
    public static int st2;
    public int count_spawn = 0;
    public int countAsteroids = 0;
    Vector3 centerPosition = Vector3.zero;
    List<GameObject> objects;





    private GameObject getPrefab()
    {
        GameObject go;
        int r = Random.Range(0,100);

        if (r>80) {
            go = prefabBig;
            SpawnController.instance.IncAsterCount(7);
        } else {
            go = prefabMed;
            SpawnController.instance.IncAsterCount(3);
        }

        return go;
    }

    public void ResetSpawnTime()
    {
        SPAWN_TIME = 0;
        st2 = 0;
        count_spawn = 0;
        timeCount = spawnTime;
    }

    public int GetSpawnTime()
    {
        return SPAWN_TIME;
    }
/*
    public void InstanciateAsteroid(AsterData asterData)
    {

        float x = Mathf.Cos(asterData.angleToSpawn) * asterData.radiusToSpawn * GameController.instance.mapRadius;
        float z = Mathf.Sin(asterData.angleToSpawn) * asterData.radiusToSpawn * GameController.instance.mapRadius;
        Vector3 spawnPosition = centerPosition + new Vector3(x, 0, z);


        GameObject go = Instantiate(getPrefab(asterData.prefab), spawnPosition, Quaternion.identity);
        go.transform.LookAt(centerPosition);
        go.transform.Rotate(0, 90, 0);

        go.GetComponent<Asteroid>().thisId = asterData.id;

        go.GetComponent<Asteroid>().asterData = asterData;

        SpawnController.instance.ids.Add(go);

    }
    */

    public void DeleteAster(int id)
    {





        


    }

    void Update()
    {
        

        if(SpawnController.instance.ids != null && SpawnController.instance.ids.Count == 0) {
            count_spawn = 0;
            SPAWN_TIME++;
            st2 = SPAWN_TIME;
            timeCount = spawnTime;
            GameController.instance.UpdateWave(SPAWN_TIME);
            
        }
        

        if( count_spawn < SPAWN_TIME){
            timeCount += Time.deltaTime;

            if(timeCount >= spawnTime)
            {  
                count_spawn++;
                GameObject prefab = getPrefab();
                GameObject go = Instantiate(prefab, transform.position, transform.rotation);
                
                timeCount = 0f;
            }
        }
        

    }
}
