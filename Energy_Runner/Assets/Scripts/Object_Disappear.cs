using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Disappear : MonoBehaviour
{
    // Este método será chamado quando ocorrer uma colisão
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que colidiu é o jogador (tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desativa o objeto
            gameObject.SetActive(false);
        }
    }
}
