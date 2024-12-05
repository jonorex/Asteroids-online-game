using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAnimatorController : MonoBehaviour
{

    public Animator animator;
    
    public static ShipAnimatorController Instance {get; private set;}


    void Awake() {
        Instance = this;
    }

    public void GhoastMode() {
        bool state = !animator.GetBool("isGhoat");
        animator.SetBool("isGhoat", state);
    }

}
