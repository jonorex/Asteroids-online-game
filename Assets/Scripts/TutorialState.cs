using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialState : MonoBehaviour
{

    public RectTransform btnExpPos, btnAtkPos, btnAcPos;



    public bool ImageLeft  = false; 
    public bool ImageRight = false; 
    public bool ImageSepator = false; 
    public bool FingerImg = false; 
    public bool MoveImg = false; 
    public bool BtnExplosion = false; 
    public bool Asteroid = false; 
    public bool AsteroidsBelt = false;

    public Vector3 fingerPos;
    public string TxtLeftStr = "", TxtRightStr = "";
    
    private const string TEXT_LEFT_CONTROLABLE_AREA = "CONTROLABLE AREA";
    private const string TEXT_LEFT_ROTATE = "ROTATE";
    private const string TEXT_LEFT_MOVE = "MOVE";
    private const string TEXT_RIGHT_ATK = "ATACK!";
    private const string TEXT_RIGHT_EXPLOSION = "EXPLODE!";
    private const string TEXT_RIGHT_MOVE = "ENABLE MOVEMENTATION";
    private const string TEXT_RIGHT_BUFF = "COLLECT THE BUFF";
 
    public static TutorialState[] states;

    void Awake() {
        //estado inicial desaparece em 5s ou clicando na imagem
        states[0].ImageLeft = true;
        states[0].ImageSepator = true;
        states[0].TxtLeftStr = TEXT_LEFT_CONTROLABLE_AREA;
        states[0].MoveImg = true;
        
        //após o user pressionar o joystick permanece por 3s
        
        states[1].TxtLeftStr = TEXT_LEFT_ROTATE;

        //permanece por 3s ou até o usuário tocar na imagem

        states[2].ImageRight = true;
        states[2].ImageSepator = true;
        states[2].FingerImg = true;
        states[2].fingerPos = btnAtkPos.position;


        //permane até o usuário destruir o asteroid
        states[3].TxtRightStr = TEXT_RIGHT_ATK;
        states[3].Asteroid = true;

        //permanece por 3s ou até o usuário tocar na imagem
        states[4].ImageRight = true;
        states[4].ImageSepator = true;
        states[4].FingerImg = true;
        states[4].fingerPos = btnAcPos.position;
        states[4].TxtRightStr = TEXT_RIGHT_MOVE;

        //após o usuario tocar no botao ac permanece por 3s
        states[5].ImageLeft = true;
        states[5].ImageSepator = true;
        states[5].TxtLeftStr = TEXT_LEFT_MOVE;
        states[5].MoveImg = true;
        
        //permanece até o usuário coletar o buff
        states[6].TxtRightStr = TEXT_RIGHT_BUFF;

        //permarmanece por três segundos ou até o usuário clicar na imagem
        states[7].FingerImg = true;
        states[7].fingerPos = btnExpPos.position;
        states[7].BtnExplosion = true;
        states[7].TxtRightStr = TEXT_RIGHT_EXPLOSION;
        
        //permanece até o usuário tocar no botao explosão
        states[8].AsteroidsBelt = true;
        states[8].TxtRightStr = TEXT_RIGHT_EXPLOSION;
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
