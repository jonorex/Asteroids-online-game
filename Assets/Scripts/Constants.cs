using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    
    public static Constants Instance;
    public GameObject AsterBig, AsterMed, AsterSmall;

    public Sprite n1, n2, n3, n4;

    Dictionary<int, Sprite> imgsDict; 

    void Awake() {
        if (Instance ==  null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        imgsDict = new Dictionary<int, Sprite>();

        imgsDict.Add(1, n1);
        imgsDict.Add(2, n2);
        imgsDict.Add(3, n3);
        imgsDict.Add(4, n4);

    }

    public Sprite GetImage(int k) {
        return imgsDict[k];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
