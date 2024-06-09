using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // Refer�ncia ao Transform do jogador
    public Vector3 offset; // Deslocamento da c�mera em rela��o ao jogador

    // Start is called before the first frame update
    void Start()
    {
        // Inicializa a posi��o da c�mera com o deslocamento definido no Inspector
        transform.position = player.position + offset;

        // Logs para depura��o
        Debug.Log("Initial Camera Position: " + transform.position);
        Debug.Log("Player Position: " + player.position);
        Debug.Log("Offset: " + offset);
        Debug.Log("Camera Position after Offset Applied: " + transform.position);
    }

    // LateUpdate � chamado uma vez por frame, ap�s todas as atualiza��es terem sido processadas
    void LateUpdate()
    {
        // Calcula a posi��o desejada com base na posi��o do jogador e no deslocamento
        Vector3 desiredPosition = player.position + offset;

        // Suaviza a transi��o da posi��o atual da c�mera para a posi��o desejada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f);

        // Atualiza a posi��o da c�mera
        transform.position = smoothedPosition;
    }
}
