using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnController : MonoBehaviour
{
    // Start is called before the first frame update
    

    public static SpawnController instance;

    public static int id;

    public ArrayList ids;

    int asterCount=0;

    int crashAsters = 0;
    
    void Awake() {
        ids = new ArrayList();
        instance = this;
    }

    public int AddAster() {
        AsterData asterData = new AsterData();
        ids.Add(++id);
        
        return id;
    }

    public void RemoveAster(int id) {

        ids.Remove(id);

        Debug.Log($"Count asters: {ids.Count}");

        AsterBars.Instance.SetMyAsterCount(++crashAsters);

    }

    public void IncAsterCount(int i) {
        asterCount+=i;
        AsterBars.Instance.SetTotalAsters(asterCount);
    }
}
