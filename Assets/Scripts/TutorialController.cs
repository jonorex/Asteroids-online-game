using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{

    public GameObject ImageLeft, 
                 ImageRight, 
                 ImageSepator, 
                 FingerImg, 
                 MoveImg,
                 BtnExplosion, 
                 Asteroid, 
                 AsteroidsBelt;
    
    public Text TxtLeft, TxtRight;

    bool touch = false;

    Transform newPos;

    Button btnAtk, btnMove;

    int countState = 0;
    public static TutorialController Instance;


    void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitDict();

        btnAtk.interactable = false;
        btnMove.interactable = false;

    }
    

    void ImageLeftEvent() {

    }

    void ImageRightEvent() {

    }

    void btnAtkEvent() {
        touch = true;
    }

    
    
    // Update is called once per frame
    void Update()
    {
        UpdateState(TutorialState.states[countState]);
    }

    void UpdateState(TutorialState state) {
        ImageLeft.SetActive(state.ImageLeft);
        ImageRight.SetActive(state.ImageRight);
        ImageSepator.SetActive(state.ImageSepator);
        FingerImg.SetActive(state.FingerImg);
        MoveImg.SetActive(state.MoveImg);
        BtnExplosion.SetActive(state.BtnExplosion);
        Asteroid.SetActive(state.Asteroid);
        AsteroidsBelt.SetActive(state.AsteroidsBelt);
        TxtLeft.text = state.TxtLeftStr;
        TxtRight.text = state.TxtRightStr;
        RectTransform f =  FingerImg.transform as RectTransform;
        f.position = state.fingerPos;
    }

    void Transition() {
        switch (countState) {
            case 0:
                StartCoroutine(Transition1(5f, 1));
                break;
            case 1:
                if(!PlayerController.PointerDown)  StartCoroutine(Transition1(3f,2));
                break;
            case 2:
                StartCoroutine(Transition1(5f, 3));
                break;
            case 3:
                btnAtk.interactable = true;
                if(touch) {
                    touch = false;
                    StartCoroutine(Transition1(3f,4));
                }
                break;
            case 4:
                StartCoroutine(Transition1(3f, 5));
                break;
            case 5:
                btnMove.interactable = true;
                if(touch) {
                    touch = false;
                    countState++;
                    //StartCoroutine(Transition1(3f,6));
                }
                break;
            
                
        }
    }

    void InitDict() {
        Dictionary<string, bool> State = new Dictionary<string, bool>();
        

    }


    private IEnumerator DisableImageTimerCount(GameObject img) {
        yield return new WaitForSeconds(3f);
        img.SetActive(false);
        StartCoroutine(TimerCount());
    }

    private IEnumerator TimerCount() {
        yield return new WaitForSeconds(2f);
        ImageRight.GetComponent<Image>().color = new Color(0,0,0,0.66f);
    }

    private IEnumerator Transition1(float time, int nextState) {
        yield return new WaitForSeconds(time);
        if(nextState == countState-1) {
            countState = nextState;
        }
    }

}
