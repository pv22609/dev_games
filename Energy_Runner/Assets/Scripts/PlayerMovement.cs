using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // Velocidade de movimento do jogador
    public float jumpForce = 7f; // Força do salto
    private Rigidbody rb; // Referência ao componente Rigidbody
    private bool isGrounded; // Verifica se o jogador está no chão

    // Start is called before the first frame update
    void Start()
    {
        // Obtém o componente Rigidbody anexado ao jogador
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica se a tecla da seta para cima está pressionada
        if (Input.GetKey(KeyCode.UpArrow))
        {
            // Move o jogador para a frente na direção em que está virado
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        // Movimento para a direita
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        // Movimento para a esquerda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        // Movimento para a trás
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }
    }

    // Verifica se o jogador está no chão
    private void OnCollisionEnter(Collision collision)
    {
        // Se o jogador colidir com o chão, permitir saltar novamente
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
