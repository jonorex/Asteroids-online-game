using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject prefab;

    public float atackSpeed;
    float timeCount;


    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;

            if(timeCount >= atackSpeed)
            {  
                GameObject go = Instantiate(prefab, transform.position, transform.rotation);
                AudioController.instance.SirenSfx(transform);
                timeCount = 0f;
            }
    }
}
