using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public Slider timeSlider; // Referência ao Slider que representa a barra de tempo
    public float totalTime = 60f; // Tempo total em segundos

    private float currentTime; // Tempo atual restante

    void Start()
    {
        currentTime = totalTime; // Inicializa o tempo atual como o tempo total no início
    }

    void Update()
    {
        // Reduz o tempo atual com base no tempo que passou desde o último frame
        currentTime -= UnityEngine.Time.deltaTime;

        // Atualiza o valor do Slider para refletir o tempo atual
        timeSlider.value = currentTime / totalTime;

        // Se o tempo acabou, pode-se tomar alguma ação, como terminar o jogo
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            SceneManager.LoadScene("GameOver");
        }
    }
}
