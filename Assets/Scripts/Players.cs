using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Players : MonoBehaviour
{
    public int myId;
    public Dictionary<int, int> playersShips;
    public static Players instance;
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else{ 
            Destroy(gameObject);
        }
        playersShips = new Dictionary<int, int>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddPlayer(int id) {
        playersShips.Add(id, 0);
    }

    public void AddShip(int id, int ship) {
        
        playersShips[id] = ship;

    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
