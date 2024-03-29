using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    // ������ �� ������, ������ �������� ������������ ����.
    [SerializeField]
    Transform target;

    // �������� �������� ������.
    public float rotSpeed = 1.5f;

    // ���� ��������.
    private float _rotY;

    // ��������� ������ ������������ ����.
    private Vector3 _offset;

    private void Start()
    {
        _rotY = transform.eulerAngles.y;

        // ���������� ���������� �������� �����
        // ������� � �����.
        // ��������� _offset �������� ��������
        // � ��������� ������ � �������� �������.
        _offset = target.position - transform.position;
    }

    private void LateUpdate()
    {
        // ������ ����������� �� ��������.
        _rotY -= Input.GetAxis("Horizontal") * rotSpeed;
        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);
        transform.position = target.position - (rotation * _offset);
        transform.LookAt(target);
    }

    #region ������ ����� private void LateUpdate()
    //private void LateUpdate()
    //{
    //    float horInput = Input.GetAxis("Horizontal");

    //    // ��������� ������� ������ ��� ������ ������
    //    // �� ���������.
    //    if (horInput != 0)
    //    {
    //        _rotY += horInput * rotSpeed;
    //    }
    //    else
    //    {
    //        // ��� ������� ������� � ������� ����.
    //        _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
    //    }

    //    Quaternion rotation = Quaternion.Euler(0, _rotY, 0);

    //    // ������������ ��������� ��������, ����������
    //    // � ������������ � ��������� ������.
    //    // �������� ������ �������� �� ����������
    //    // ��� ��������� ��������� �������� ����� ��������.
    //    // ����� ���������� ��������� ������, ������� ��������
    //    // ����� �������� �� ��������� �������� �������.

    //    // 1. ���������� ��������� ������, ����� �������� �
    //    // �������� ������������ ���������.
    //    // 2. �������� ������ �������� �� ���������� ���
    //    // ��������� �������� � ���������� ��������.
    //    // 3. ��������� �� ��������� ������, �����
    //    // ���������� �������� ������������ ������.
    //    transform.position = target.position - (rotation * _offset);

    //    // ������ ������ ���������� �� ����,
    //    // ��� �� ������������ ���� ���� ���
    //    // �� �������������.
    //    transform.LookAt(target);

    //}
    #endregion
}
