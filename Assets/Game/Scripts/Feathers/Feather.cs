using UnityEngine;

public class Feather : MonoBehaviour
{

    public float featherHeight = 2f;
    public float featherWidth = 0.5f;

    // Метод при создании пера
    void Start()
    {
        // Размер пера
        transform.localScale = new Vector2(featherWidth, featherHeight);
    }

    // Когда перо собирается игроком
    public void CollectFeather()
    {
        //Увеличиваем опыт гуся
        MainGoose mainGoose = FindObjectOfType<MainGoose>();
        if (mainGoose != null)
        {
            mainGoose.experience++;
        }
        // Уничтожаем объект пера
        Destroy(gameObject); 
    }
}