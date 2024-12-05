using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player; // Referência ao transform da nave/jogador
    public Vector3 offset; // Distância entre a câmera e o jogador
    public float smoothSpeed = 0.125f; // Velocidade de suavização

    public static FollowCamera instace; 

    void Awake() {
        if (instace == null) {
            instace = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start () {
        offset = new Vector3(0,0,-0.5f);
    }

    void LateUpdate()
    {
        if (player != null) // Certifique-se de que o jogador existe
        {
            // Calcula a posição desejada com o offset
            Vector3 desiredPosition = player.position + offset;

            // Interpola para suavizar o movimento
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Atualiza a posição da câmera
            transform.position = smoothedPosition;
        }
    }
}
