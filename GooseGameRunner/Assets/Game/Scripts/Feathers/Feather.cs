using UnityEngine;

public class Feather : MonoBehaviour
{

    public float featherHeight = 2f;
    public float featherWidth = 0.5f;

    // ����� ��� �������� ����
    void Start()
    {
        // ������ ����
        transform.localScale = new Vector2(featherWidth, featherHeight);
    }

    // ����� ���� ���������� �������
    public void CollectFeather()
    {
        //����������� ���� ����
        MainGoose mainGoose = FindObjectOfType<MainGoose>();
        if (mainGoose != null)
        {
            mainGoose.experience++;
        }
        // ���������� ������ ����
        Destroy(gameObject); 
    }
}