using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using Unity.VisualScripting;

public class ChangeLane : MonoBehaviour
{
    public CustomDollyCart dollyCart;
    public CinemachinePathBase leftLane;
    public CinemachinePathBase middleLane;
    public CinemachinePathBase rightLane;
    public float laneChangeDuration = 0.5f; // Tempo para completar a mudança de faixa

    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private bool isChangingLane = false;

    private Rigidbody rb; // Referência ao componente Rigidbody

    public GameObject level2Objects; // Referência ao GameObject pai com a tag "ObjLevel2"
    private int checkpointPassCount = 0; // Contador de passagens pelo checkpoint

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

        // Encontra a posição correspondente na trilha de destino
        Vector3 startPos = startLane.EvaluatePositionAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance);
        float closestPosOnTargetLane = FindClosestPositionOnPath(targetLane, startPos);

        while (elapsedTime < laneChangeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / laneChangeDuration;

            // Interpolação da posição entre a faixa atual e a faixa de destino
            Vector3 newPosition = Vector3.Lerp(
                startLane.EvaluatePositionAtUnit(dollyCart.m_Position, CinemachinePathBase.PositionUnits.Distance),
                targetLane.EvaluatePositionAtUnit(closestPosOnTargetLane, CinemachinePathBase.PositionUnits.Distance),
                t
            );

            // Interpolação da rotação entre a faixa atual e a faixa de destino
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

    // Função para encontrar a posição mais próxima na trilha de destino
    float FindClosestPositionOnPath(CinemachinePathBase path, Vector3 targetPosition)
    {
        float closestDistance = float.MaxValue;
        float closestPosition = 0f;
        float stepSize = 1f; // Ajuste conforme necessário para precisão

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

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Barreira"))
        {
            Debug.Log("Bati numa barreira!!");
            Destroy(collider.gameObject);

            // Reduz a vida no LifeController
            LifeController lifeController = FindObjectOfType<LifeController>();
            if (lifeController != null)
            {
                lifeController.ReduceLife(10f); // Reduz a vida em 10 unidades
            }
        }

        if (collider.gameObject.CompareTag("Clock"))
        {
            Debug.Log("Bati num clock!!");
            Destroy(collider.gameObject);

            // Aumenta o tempo no TimeController
            TimeController timeController = FindObjectOfType<TimeController>();
            if (timeController != null)
            {
                timeController.IncreaseTime(7f); // Aumenta o tempo em 7 segundos
            }
        }

        if (collider.gameObject.CompareTag("Bottle"))
        {
            Debug.Log("Bati numa Bottle!!");
            Destroy(collider.gameObject);

            // Aumenta a vida no LifeController
            LifeController lifeController = FindObjectOfType<LifeController>();
            if (lifeController != null)
            {
                lifeController.IncreaseLife(5f); // Aumenta a vida em 5 unidades
            }
        }

        if (collider.gameObject.CompareTag("Checkpoint"))
        {
            Debug.Log("Passei pelo Checkpoint!!");

            checkpointPassCount++;

            if (checkpointPassCount == 2)
            {
                Console.Write("estou dentro do if antes da função activate!!!");
                // Ativar os objetos de nível 2 na segunda passagem pelo checkpoint
                ActivateObjNivel2();
            }
        }
    }

    void ActivateObjNivel2()
    {
        if (level2Objects != null)
        {
            level2Objects.SetActive(true);
        }
        else
        {
            Debug.LogError("ObjLevel2 não encontrado na cena!");
        }
    }
}
