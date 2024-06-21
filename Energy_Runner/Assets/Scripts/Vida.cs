using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void vidaMaxima(float vidaMax)
    {
        slider.maxValue = vidaMax;
    }

    public void vidaAtual(float vidaAct)
    {
        slider.value = vidaAct;
    }

    public void initVida(float vidaTot)
    {
        vidaMaxima(vidaTot);
        vidaAtual(vidaTot);
    }
}
