using UnityEngine;
using UnityEngine.Diagnostics;

public class spawn : MonoBehaviour
{
    public GameObject prefab, prefab2;
    public void Ataque()
    {
        //PlayerController.instance.Explosion();
        if (gameObject.activeSelf)
        {
            //PlayerController.instance.Explosion();
            GameObject go;
            if (PlayerController.instance.missaleBuff)
            {
                go = Instantiate(prefab2, transform.position, transform.rotation);
                AudioController.instance.HeavyShootSfx(transform);
            }
            else
            {
                go = Instantiate(prefab, transform.position, transform.rotation);
                AudioController.instance.ShootSfx(transform);
            }
//
            //transform.Translate(Vector2.up * speed * Time.deltaTime);
            Destroy(go, 5f);
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
