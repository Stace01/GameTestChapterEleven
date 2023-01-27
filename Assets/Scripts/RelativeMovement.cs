using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���������� ������ ���������� ��������
// ���������� ������ RequireComponent().

// ����� RequireComponent() ���������� Unity
// ��������� ������� � ������� GameObject
// ���������� ����������� � ������� ����.
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    // �������� ����� ������ �� ������,
    // ������������ �������� ����� ����������� �����������.
    [SerializeField]
    Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;

    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    // �������� ����������� ����.
    public float pushForce = 3.0f;

    private float _vertSpeed;

    private CharacterController _charController;

    private Animator _animator;

    // ����� ��� ���������� ������ � ������������
    // ����� ���������.
    private ControllerColliderHit _contact;

    private void Start()
    {
        // �������������� �������� �� ���������,
        // ���������� �� ����������� ��������
        // ������� � ������ ������������ �������.
        _vertSpeed = minFall;

        // ������������ ��� ������� � ������ �����������.
        _charController = GetComponent<CharacterController>();

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // �������� � ������� (0, 0, 0), ����������
        // �������� ���������� ��������.
        Vector3 movement = Vector3.zero;

        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        // �������� �������������� ������
        // ��� ������� ������ �� ���������.
        if (horInput != 0 || verInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = verInput * moveSpeed;
            // ������������ �������� �� ���������
            // ��� �� ���������, ��� � �������� ����� ���.
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            // ��������� ��������� ����������, �����
            // ��������� � ��� ����� ���������� ������
            // � ������� ��������.
            Quaternion tmp = target.rotation;

            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);

            // ����������� ����������� ��������
            // �� ��������� � ���������� ����������.
            movement = target.TransformDirection(movement);

            target.rotation = tmp;

            // ����� LookRotation() ��������� ����������,
            // ��������� � ���� �����������.
            Quaternion direction = Quaternion.LookRotation(movement);

            // ����� Quaternion.Lerp()(linear interpolation) ��������� ������� �������
            // �� �������� ��������� � ������� (������ �������� ������������
            // �������� ��������).
            // ������� ������� �� ������� �������� � �������,
            // ���������� �������������.
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }

        _animator.SetFloat("Speed", movement.sqrMagnitude);

        bool hitGround = false;
        RaycastHit hit;

        // ���������, ������ ��� ��������.
        if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // ����������, � ������� ������������ ���������
            // (������ ������� �� ������ ����� �������).
            // ���� ������ ����������� ��������� (��� ���� ��� ���������� �����)
            // � ��������� � ��� ���������� ����. ���������� �������� �������
            // �������, ��� ��� ��� ��������� �� ������ ��������� � ��������
            // ���������� �� ��� ����. �� �� ����� ��������� ���� �����
            // ������� ���������, ����� ������ ���������� ����������
            // �������� ����, ������� ������ ��������� ������� �� 1,9, � �� �� 2,
            // � �����, ��� �������� ��������� ������� ����������.
            float check = (_charController.height + _charController.radius) / 1.9f;
            hitGround = hit.distance <= check;
        }

        // �������� isGrounded ���������� CharacterController
        // ��������� ������������� �� ����������
        // � ������������.

        // ������ �������� �������� isGrounded
        // ������� �� ��������� �������� ����.
        if (hitGround)
        {
            // ������� �� ������ Jump ��� ����������
            // �� �����������.
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed;
            }
            else
            {
                _vertSpeed = minFall; //-0.1f;
                _animator.SetBool("Jumping", false);
            }
        }
        // ���� �������� �� ����� �� �����������, ���������
        // ����������, ���� �� ����� ���������� ���������� ��������.
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;

            if (_vertSpeed < terminalVelocity)
            {
                _vertSpeed = terminalVelocity;
            }
            // �� ������� � �������� ��� ��������
            // � ����� ������ ������.
            if (_contact != null)
            {
                _animator.SetBool("Jumping", true);
            }
            // ����� �������� ���� �� ������������ �����������,
            // �� ������� � ��� �������������.
            if (_charController.isGrounded)
            {
                // ������� ������ �������� � ����������� �� ����,
                // ������� �� �������� � ������� ����� ��������. 
                if (Vector3.Dot(movement, _contact.normal) < 0)
                {
                    movement = _contact.normal * moveSpeed;
                }
                else
                {
                    movement += _contact.normal * moveSpeed;
                }
            }
        }

        movement.y = _vertSpeed;

        movement *= Time.deltaTime;
        _charController.Move(movement);
    }

    // ��� ������������� ������������ ������ �����
    // ������������ ����������� � ������ ��������� ������.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;

        // ��������, ���� �� � ������������ � ������������
        // ������� ��������� Rigidbody, ��������������
        // ������� �� ����������� ����.
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body != null && !body.isKinematic)
        {
            // ���������� ����������� ���� ��������.
            body.velocity = hit.moveDirection * pushForce;
        }
    }
}