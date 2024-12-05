using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = PlayerController.instance.gameObject.transform.position;
        transform.position = pos;
        transform.rotation = PlayerController.instance.transform.rotation;
        transform.Rotate(new Vector3(0,0,-180));
    }
}
