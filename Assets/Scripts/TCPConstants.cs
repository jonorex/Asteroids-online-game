using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TCPConstants : MonoBehaviour
{

    public const string SHOOT_PERMISSION = "SP";
    public const string MOVEMENT_PERMISSION = "MP";
    public const string ROTATION_PERMISSION = "RP";

    public const string NEW_PLAYER_CONSTANT = "NP";
    public const string YOUR_ID = "YI";
    public const string ROTATE_CONST = "R";
    public static TCPConstants instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
