using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public Slider timeSlider; // Refer�ncia ao Slider que representa a barra de tempo
    public float totalTime = 100f; // Tempo total em segundos
    private float currentTime; // Tempo atual restante

    void Start()
    {
        currentTime = totalTime; // Inicializa o tempo atual como o tempo total no in�cio
    }

    void Update()
    {
        // Reduz o tempo atual com base no tempo que passou desde o �ltimo frame
        currentTime -= Time.deltaTime;

        // Atualiza o valor do Slider para refletir o tempo atual
        timeSlider.value = currentTime / totalTime;

        // Se o tempo acabou, pode-se tomar alguma a��o, como terminar o jogo
        if (currentTime <= 0f)
        {
            currentTime = 0f;
            SceneManager.LoadScene("GameOver");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Clock"))
        {
            Debug.Log("Bati num clock!!");
            Destroy(collider.gameObject);

            // Aumenta o tempo no TimeController
            IncreaseTime(10f); // Aumenta o tempo em 10 segundos (ajuste conforme necess�rio)
        }
    }

    public void IncreaseTime(float amount)
    {
        currentTime += amount;
        // Garante que o tempo n�o ultrapasse o total
        currentTime = Mathf.Min(currentTime, totalTime);
    }
}
