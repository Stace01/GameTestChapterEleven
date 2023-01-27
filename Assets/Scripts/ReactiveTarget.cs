using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // �����, ��������� ��������� ��������.
    public void ReactToHit()
    {
        WanderingAI behavior = GetComponent<WanderingAI>();

        // ���������, ����������� �� � ��������� �������� WanderingAI;
        // �� ����� � �������������. 
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        StartCoroutine(Die());
    }

    // ������������ �����, ��� 1,5 ������� � ���������� ���.
    private IEnumerator Die()
    {
        this.transform.Rotate(-75, 0, 0);

        yield return new WaitForSeconds(1.5f);

        // ������ ����� ���������� ��� ����.

        // ����� ��������� ��� this.gameObject, ��������� � �������
        // GameObject, � �������� ����������� ������ ��������.
        Destroy(this.gameObject);
    }
}