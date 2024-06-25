using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    public Slider lifeSlider; // Referência ao Slider que representa a barra de vida
    public float totalLife = 100f; // Vida total inicial
    private float currentLife; // Vida atual restante

    void Start()
    {
        currentLife = totalLife; // Inicializa a vida atual como a vida total no início
        UpdateLifeUI(); // Atualiza o Slider de vida no início
    }

    void Update()
    {
        if (currentLife <= 0f)
        {
            currentLife = 0f;
            SceneManager.LoadScene("GameOver");
        }

        UpdateLifeUI(); // Atualiza o Slider de vida continuamente
    }

    void UpdateLifeUI()
    {
        lifeSlider.value = currentLife / totalLife;
    }

    public void ReduceLife(float amount)
    {
        currentLife -= amount;
        // Verifica se a vida chegou a zero ou menos
        if (currentLife <= 0f)
        {
            currentLife = 0f;
            SceneManager.LoadScene("GameOver"); // Ou outra ação de game over
        }

        UpdateLifeUI(); // Atualiza o Slider de vida após a redução
    }

    public void IncreaseLife(float amount)
    {
        currentLife += amount;
        // Garante que a vida não ultrapasse o total
        currentLife = Mathf.Min(currentLife, totalLife);

        UpdateLifeUI(); // Atualiza o Slider de vida após o aumento
    }
}
