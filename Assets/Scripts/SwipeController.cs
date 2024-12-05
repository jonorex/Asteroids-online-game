using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] int maxPage;
    public int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    
    public static SwipeController instance;

    private void Awake() {

        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        
        
    
    }

    public void Start() {
        StartCoroutine(DelayTosSendShip());
        
    }

    IEnumerator DelayTosSendShip() {
        yield return new WaitForSeconds(3f);
        ClientTCP.instance.ShipSelectionMessage(currentPage);
    }

    

    public void Next() {
        if (currentPage < maxPage) {
            currentPage++;
            targetPos += pageStep;
            MovePage();
            ClientTCP.instance.ShipSelectionMessage(currentPage);
        }

        
        
    }

    public void Previous() {
        if (currentPage > 1) {
            currentPage--;
            targetPos-= pageStep;
            MovePage();
            ClientTCP.instance.ShipSelectionMessage(currentPage);
        }

        
        
      
    }


    void MovePage() {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);

    }


}
