using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    [Tooltip("���� �� ���������")]
    public int damage;
    [SerializeField]
    [Tooltip("��������� ��������")]
    public float range;
    [SerializeField]
    [Tooltip("�������� ��������")]
    public float fireRate; 

    public virtual void Fire()
    {
        // ������ �������� ����� ��� ���� ������
    }
}