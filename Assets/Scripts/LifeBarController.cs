using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LifeBarController : MonoBehaviour
{
    // Start is called before the first frame update
    
    

    public static LifeBarController Instance;
    private ArrayList hearts =  new ArrayList();
    private int count = 0;
    
    void Awake() {
        Instance = this;
        //hearts = new ArrayList();
    }

    void Start()
    {
        
        hearts.Add(gameObject.transform.GetChild(2).gameObject);
        hearts.Add(gameObject.transform.GetChild(1).gameObject);
        hearts.Add(gameObject.transform.GetChild(0).gameObject);
    }

    public bool Decrement() {
        GameObject go = hearts[count] as GameObject;
        go.SetActive(false);
        count++;
        if (count == 3) return true;
        else return false;
    }

    public void Reset() {
        foreach (GameObject item in hearts){
            item.SetActive(true);
        }
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
