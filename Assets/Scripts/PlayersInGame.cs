using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayersInGame : MonoBehaviour
{

    public Dictionary<int, GameObject> playersDict;
    public GameObject ship1, ship2, ship3, ship4;

    

    public GameObject explosionFragment, btnExplosion, btnExplosionAnim, topReference, bottomReference, leftReference, rightReference;

    public Button btnAc;

    public Sprite acSprite, stopSprite;

    public static PlayersInGame instance;

    public int myId;

    void Awake()
    {
        playersDict = new Dictionary<int, GameObject>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        // foreach (int p in Players.instance.playersShips.Keys)
       // {

        InstanciatePlayerShip(Players.instance.myId, Players.instance.playersShips[Players.instance.myId]);
       // }

        

    }
    

    private GameObject SelectShip(int ship)
    {
        Debug.Log("nave escolhida " + ship);
        GameObject go;
        switch (ship)
        {
            case 1:
                go = Instantiate(ship1);
                break;
            case 2:
                go = Instantiate(ship2);
                break;
            case 3:
                go = Instantiate(ship3);
                break;
            default:
                go = Instantiate(ship4);
                break;
        }

        return go;
    }


    public void InstanciatePlayerShip(int id, int ship)
    {

        GameObject playerShip = SelectShip(ship);

        PlayerController pc = playerShip.AddComponent<PlayerController>();
        
        
       
        pc.moveSpeed = 4;
        pc.explosionFragment = explosionFragment;
        pc.lFire = playerShip.transform.GetChild(3).gameObject;
        pc.rFire = playerShip.transform.GetChild(2).gameObject;
        pc.ButtonExplosion = btnExplosion;
        pc.BtnExplosionAnim = btnExplosionAnim;
       
        pc.btnAc = btnAc;
        pc.AcSprite = acSprite;
        pc.StopSprite = stopSprite;

        
    

        playerShip.SetActive(true);
        if (id == Players.instance.myId) {
            SetLayerRecursively(playerShip, 8);
            //FollowCamera.instace.player = playerShip.transform;
        }
        

        playersDict.Add(id, playerShip);



    }



    private void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer; // Altera a layer do objeto atual

        // Itera por todos os filhos do objeto e aplica a mesma layer
        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }

    public void Shoot(int id)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
