using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class ChangeLane : MonoBehaviour
{
    public CustomDollyCart dollyCart;
    public CinemachinePathBase leftLane;
    public CinemachinePathBase middleLane;
    public CinemachinePathBase rightLane;
    public float laneChangeDuration = 0.5f; // Tempo para completar a mudança de faixa

    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private bool isChangingLane = false;

    public float jumpForce = 7f; // Força do salto
    private Rigidbody rb; // Referência ao componente Rigidbody
    private bool isGrounded = true; // Verifica se o jogador está no chão

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mudança de faixa
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentLane > 0 && !isChangingLane)
        {
            currentLane--;
            StartCoroutine(SwitchLane());
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && currentLane < 2 && !isChangingLane)
        {
            currentLane++;
            StartCoroutine(SwitchLane());
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    IEnumerator SwitchLane()
    {
        isChangingLane = true;

        CinemachinePathBase startLane = dollyCart.m_Path;
        CinemachinePathBase targetLane = null;

        switch (currentLane)
        {
            case 0:
                targetLane = leftLane;
                break;
            case 1:
                targetLane = middleLane;
                break;
            case 2:
                targetLane = rightLane;
                break;
        }

        float elapsedTime = 0f;

        while (elapsedTime < laneChangeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / laneChangeDuration;

            // Interpolação da posição entre a faixa atual e a faixa de destino
            Vector3 newPosition = Vector3.Lerp(
                startLane.EvaluatePositionAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance),
                targetLane.EvaluatePositionAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance),
                t
            );

            // Interpolação da rotação entre a faixa atual e a faixa de destino
            Quaternion newRotation = Quaternion.Lerp(
                startLane.EvaluateOrientationAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance),
                targetLane.EvaluateOrientationAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance),
                t
            );

            dollyCart.transform.position = newPosition;
            dollyCart.transform.rotation = newRotation;

            yield return null;
        }

        dollyCart.m_Path = targetLane;
        isChangingLane = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica se o jogador colidiu com o chão (ou outra superfície adequada)
        if (collision.gameObject.CompareTag("Ground"))
        {
            Console.WriteLine("Estou a tocar no chão!!");
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Quando o jogador deixa de colidir com o chão
        if (collision.gameObject.CompareTag("Ground"))
        {
            Console.WriteLine("Não estou no chão!");
            isGrounded = false;
        }
    }
}
