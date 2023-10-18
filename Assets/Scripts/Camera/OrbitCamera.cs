using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    Transform m_Target; // 쫓아갈 타겟
    [SerializeField]
    float m_Distance = 5.0f; // 타겟과의 거리
    [SerializeField]
    float m_XSpeed = 120.0f; // x축 회전 속도
    [SerializeField]
    float m_YSpeed = 120.0f; // y축 회전 속도

    [SerializeField]
    float m_YMinLimit = -20f; // y축 최저 회전각도
    [SerializeField]
    float m_YMaxLimit = 80f; // y축 최대 회전 각도

    [SerializeField]
    float m_DistanceMin = .5f; // 물체와의 가장 가까운 거리, 휠로 조정
    [SerializeField]
    float m_DistanceMax = 15f; // 물체와의 가장 먼 거리, 휠로 조정

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
            // Mathf.Clamp(현재좌표, 최소좌표, 최대좌표)
            m_Distance = Mathf.Clamp(m_Distance - Input.GetAxis("Mouse ScrollWheel") * distance, m_DistanceMin, m_DistanceMax);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -m_Distance);
            Vector3 position = rotation * negDistance + m_Target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public float ClampAngle(float angle, float min, float max) // 각 고정 함수 
    {
        if (angle < -360F)
            angle += 360F;

        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}