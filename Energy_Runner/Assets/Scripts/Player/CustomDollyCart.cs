using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CustomDollyCart : MonoBehaviour
{
    public CinemachinePathBase m_Path; // Refer�ncia ao CinemachineSmoothPath
    public float m_Position = 0f; // Posi��o inicial no caminho
    public float m_Speed = 10f; // Velocidade de movimento ao longo do caminho

    void Update()
    {
        // Incrementa a posi��o ao longo do tempo baseado na velocidade
        m_Position += m_Speed * Time.deltaTime;

        // Mant�m a posi��o dentro dos limites do caminho
        if (m_Path != null)
        {
            if (m_Position > m_Path.PathLength)
                m_Position -= m_Path.PathLength;
            else if (m_Position < 0)
                m_Position += m_Path.PathLength;

            // Atualiza a posi��o do jogador ao longo do caminho
            transform.position = m_Path.EvaluatePositionAtUnit(m_Position, CinemachinePathBase.PositionUnits.Distance);
            transform.rotation = m_Path.EvaluateOrientationAtUnit(m_Position, CinemachinePathBase.PositionUnits.Distance);
        }
    }
}
