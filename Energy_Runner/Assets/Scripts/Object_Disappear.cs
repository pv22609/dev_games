using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Disappear : MonoBehaviour
{
    // Este m�todo ser� chamado quando ocorrer uma colis�o
    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que colidiu � o jogador (tag "Player")
        if (collision.gameObject.CompareTag("Player"))
        {
            // Desativa o objeto
            gameObject.SetActive(false);
        }
    }
}
