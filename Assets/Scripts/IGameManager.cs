using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������� ���������, ������� ����� �������������
// ���������� ������.
public interface IGameManager
{
    // ��� ������������, ������� ��� ����� ����������.
    // �������� status �������� ��������� ����� �����,
    // �������� �� ������ �������������.
    ManagerStatus status { get; }

    // ����� Startup() ������������ ��� ��������� �������� 
    // ������������� ����������, ������� � ��� �����������
    // ��� ��������� � ���� ��������� �������, � �������
    // ����� ��������� ����������.
    void Startup(NetworkService service);
}
