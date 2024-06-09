using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Referência ao Transform do jogador
    public Vector3 offset; // Deslocamento da câmera em relação ao jogador

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa a posição da câmera com o deslocamento definido no Inspector
        transform.position = player.position + offset;

        // Logs para depuração
        Debug.Log("Initial Camera Position: " + transform.position);
        Debug.Log("Player Position: " + player.position);
        Debug.Log("Offset: " + offset);
        Debug.Log("Camera Position after Offset Applied: " + transform.position);
    }

    // LateUpdate é chamado uma vez por frame, após todas as atualizações terem sido processadas
    void LateUpdate()
    {
        // Calcula a posição desejada com base na posição do jogador e no deslocamento
        Vector3 desiredPosition = player.position + offset;

        // Suaviza a transição da posição atual da câmera para a posição desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f);

        // Atualiza a posição da câmera
        transform.position = smoothedPosition;
    }
}
