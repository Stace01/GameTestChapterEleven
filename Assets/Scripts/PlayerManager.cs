using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    private NetworkService _network;

    // �������� �������� ������ ������,
    // ��������� ������ � ���� ���������.
    public ManagerStatus status { get; private set; }

    public int health { get; private set; }
    public int maxHealth { get; private set; }

    public void Startup(NetworkService service)
    {
        Debug.Log("Player manager starting. . .");

        _network = service;

        // ��� �������� ����� ���� ����������������
        // ������������ �������.
        health = 50;
        maxHealth = 100;

        status = ManagerStatus.Started;
    }

    // ������ �������� �� ����� ��������
    // �������� ���������� health, �� �����
    // �������� ��� �������.
    public void ChangeHealth(int value)
    {
        health += value;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        // �������� ��������� health ��
        // �������� PlayerManager.
        // ��������� ����������� ������
        // ���, ����� ����� ChangeHealth()
        // ��������� ���� ������, �������
        // ��������� ��������� �� ���������
        // ��������� health.
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
        Debug.Log("Health: " + health + "/" + maxHealth);
    }
}
