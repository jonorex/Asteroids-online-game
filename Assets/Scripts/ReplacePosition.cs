using Unity.VisualScripting;
using UnityEngine;

public class ReplacePosition : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject  bottomReference,topReference, rightReference, leftReference;
    private Vector3 newP;

    public static ReplacePosition Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //VerifyPosition(transform);
    }

    public void VerifyPosition(Transform transform) {
        if (transform.position.y >= topReference.transform.position.y) {
            newP = new Vector3(transform.position.x, bottomReference.transform.position.y+1, transform.position.z);
            transform.position = newP;
        } else if(transform.position.y <= bottomReference.transform.position.y) {
            newP = new Vector3(transform.position.x, topReference.transform.position.y-1, transform.position.z);
            transform.position = newP;
        } else if(transform.position.x <= leftReference.transform.position.x) {
            newP = new Vector3(rightReference.transform.position.x-1, transform.position.y, transform.position.z);
            transform.position = newP;
        }else if(transform.position.x >= rightReference.transform.position.x) {
            newP = new Vector3(leftReference.transform.position.x+1, transform.position.y, transform.position.z);
            transform.position = newP;
        }
    }
}
