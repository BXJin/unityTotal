using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    Transform m_Target; // �Ѿư� Ÿ��
    [SerializeField]
    float m_Distance = 5.0f; // Ÿ�ٰ��� �Ÿ�
    [SerializeField]
    float m_XSpeed = 120.0f; // x�� ȸ�� �ӵ�
    [SerializeField]
    float m_YSpeed = 120.0f; // y�� ȸ�� �ӵ�

    [SerializeField]
    float m_YMinLimit = -20f; // y�� ���� ȸ������
    [SerializeField]
    float m_YMaxLimit = 80f; // y�� �ִ� ȸ�� ����

    [SerializeField]
    float m_DistanceMin = .5f; // ��ü���� ���� ����� �Ÿ�, �ٷ� ����
    [SerializeField]
    float m_DistanceMax = 15f; // ��ü���� ���� �� �Ÿ�, �ٷ� ����

    [SerializeField]
    float m_X = 0.0f;
    [SerializeField] 
    float m_Y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        m_X = angles.y;
        m_Y = angles.x;
    }

    void LateUpdate()
    {
        if (m_Target)
        {
            if (Input.GetMouseButton(1))
            {
                m_X += Input.GetAxis("Mouse X") * m_XSpeed * m_Distance * 0.02f;
                m_Y -= Input.GetAxis("Mouse Y") * m_YSpeed * 0.02f;

                m_Y = ClampAngle(m_Y, m_YMinLimit, m_YMaxLimit);
            }

            Quaternion rotation = Quaternion.Euler(m_Y, m_X, 0);

            float distance = Vector3.Distance(m_Target.position, transform.position);
            // Mathf.Clamp(������ǥ, �ּ���ǥ, �ִ���ǥ)
            m_Distance = Mathf.Clamp(m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, m_DistanceMin, m_DistanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -m_Distance);
            Vector3 position = rotation * negDistance + m_Target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public float ClampAngle(float angle, float min, float max) // �� ���� �Լ� 
    {
        if (angle < -360F)
            angle += 360F;

        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}