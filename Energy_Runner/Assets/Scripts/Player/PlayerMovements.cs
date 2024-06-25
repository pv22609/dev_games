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
    public float laneChangeDuration = 0.5f; // Tempo para completar a mudan�a de faixa

    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private bool isChangingLane = false;

    public float jumpForce = 7f; // For�a do salto
    private Rigidbody rb; // Refer�ncia ao componente Rigidbody
    private bool isGrounded = true; // Verifica se o jogador est� no ch�o

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Mudan�a de faixa
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

        // Encontra a posi��o correspondente na trilha de destino
        Vector3 startPos = startLane.EvaluatePositionAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance);
        float closestPosOnTargetLane = FindClosestPositionOnPath(targetLane, startPos);

        while (elapsedTime < laneChangeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / laneChangeDuration;

            // Interpola��o da posi��o entre a faixa atual e a faixa de destino
            Vector3 newPosition = Vector3.Lerp(
                startLane.EvaluatePositionAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance),
                targetLane.EvaluatePositionAtUnit(closestPosOnTargetLane, CinemachinePathBase.PositionUnits.Distance),
                t
            );

            // Interpola��o da rota��o entre a faixa atual e a faixa de destino
            Quaternion newRotation = Quaternion.Lerp(
                startLane.EvaluateOrientationAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance),
                targetLane.EvaluateOrientationAtUnit(closestPosOnTargetLane, CinemachinePathBase.PositionUnits.Distance),
                t
            );

            dollyCart.transform.position = newPosition;
            dollyCart.transform.rotation = newRotation;

            yield return null;
        }

        dollyCart.m_Path = targetLane;
        dollyCart.m_Position = closestPosOnTargetLane;
        isChangingLane = false;
    }

    // Fun��o para encontrar a posi��o mais pr�xima na trilha de destino
    float FindClosestPositionOnPath(CinemachinePathBase path, Vector3 targetPosition)
    {
        float closestDistance = float.MaxValue;
        float closestPosition = 0f;
        float stepSize = 1f; // Ajuste conforme necess�rio para precis�o

        for (float pos = 0f; pos < path.PathLength; pos += stepSize)
        {
            float distance = Vector3.Distance(targetPosition, path.EvaluatePositionAtUnit(pos, CinemachinePathBase.PositionUnits.Distance));
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPosition = pos;
            }
        }

        return closestPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Verifica se o jogador colidiu com o ch�o (ou outra superf�cie adequada)
        if (collision.gameObject.CompareTag("Ground"))
        {
            Console.WriteLine("Estou a tocar no ch�o!!");
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Quando o jogador deixa de colidir com o ch�o
        if (collision.gameObject.CompareTag("Ground"))
        {
            Console.WriteLine("N�o estou no ch�o!");
            isGrounded = false;
        }
    }
}
