using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CustomDollyCart : MonoBehaviour
{
    public CinemachinePathBase m_Path; // Referência ao CinemachineSmoothPath
    public float m_Position = 0f; // Posição inicial no caminho
    public float m_Speed = 10f; // Velocidade de movimento ao longo do caminho

    void Update()
    {
        // Incrementa a posição ao longo do tempo baseado na velocidade
        m_Position += m_Speed * Time.deltaTime;

        // Mantém a posição dentro dos limites do caminho
        if (m_Path != null)
        {
            if (m_Position > m_Path.PathLength)
                m_Position -= m_Path.PathLength;
            else if (m_Position < 0)
                m_Position += m_Path.PathLength;

            // Atualiza a posição do jogador ao longo do caminho
            transform.position = m_Path.EvaluatePositionAtUnit(m_Position, CinemachinePathBase.PositionUnits.Distance);
            transform.rotation = m_Path.EvaluateOrientationAtUnit(m_Position, CinemachinePathBase.PositionUnits.Distance);
        }
    }
}
