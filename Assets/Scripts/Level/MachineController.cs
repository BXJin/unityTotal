using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelSituation
{
    Move,
    Stop,
    Horizontal,
    Rotate,
    LookLens,
    FocusOn,
    LessRotate,
    Clear,
    max
}
public class MachineController : MonoBehaviour
{
    [SerializeField]
    GameObject m_buttonCanvas;
    [SerializeField]
    GameObject m_lensCanvas;
    [SerializeField]
    GameObject m_blurEffect;
    [SerializeField]
    GameObject m_scopeCamera;
    [SerializeField]
    float m_speed = 4f;
    [SerializeField]
    GameObject[] m_targets;
    [SerializeField]
    GameObject m_rotateObject;
    [SerializeField]
    LevelSituation m_currentSituation = LevelSituation.Stop;
    [SerializeField]
    int m_targetNum = 0;
    LevelSituation m_prevSituation;
    //bool m_isRotate = false;
    //[SerializeField]
    public bool m_isBlur = true;
    bool m_isRotatePerfect = false;
    Vector3 m_currentPostion;
    Vector3 m_movePosition;
    public void SetSituation(LevelSituation situation)
    {
        m_currentSituation = situation;
    }
    void Stop()
    {
        m_movePosition = transform.position;
        if (m_currentPostion != m_movePosition && m_targetNum != 0)
            m_targetNum--;
        m_currentPostion = m_movePosition;
    }
    void Rotate()
    {
        m_isBlur = true;
        var target = m_targets[m_targetNum].transform.position + new Vector3(1f,0f,1f);
        Vector3 dir = target - m_rotateObject.transform.position;
        m_rotateObject.transform.rotation = Quaternion.Lerp(m_rotateObject.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1);      
    }
    void LessRotate()
    {
        var target = m_targets[m_targetNum].transform.position;
        Vector3 dir = target - m_rotateObject.transform.position;
        m_rotateObject.transform.rotation = Quaternion.Lerp(m_rotateObject.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1);
    }
    void Move()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += m_speed * dir * Time.deltaTime;
    }
    void ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "back_lens")
                ZoomIn();         
            LevelButton.Instance.HitColliderButton(hit.transform.tag, m_currentSituation);           
        }
    }
    void ZoomIn()
    {
        if(m_isBlur && m_currentSituation != LevelSituation.Clear)
        {
            m_prevSituation = m_currentSituation;
            m_currentSituation = LevelSituation.LookLens;
            m_buttonCanvas.SetActive(false);
            m_scopeCamera.SetActive(true);
            m_lensCanvas.SetActive(true);
            m_blurEffect.SetActive(true);
        }   
        else
        {
            m_prevSituation = m_currentSituation;
            m_currentSituation = LevelSituation.LookLens;
            m_buttonCanvas.SetActive(false);
            m_scopeCamera.SetActive(true);
            m_lensCanvas.SetActive(true);
            m_blurEffect.SetActive(false);
        }
    }
    
    void ZoomOut()
    {
        if(m_currentSituation != LevelSituation.Clear)
            m_currentSituation = m_prevSituation;
        m_buttonCanvas.SetActive(true);
        m_scopeCamera.SetActive(false);
        m_lensCanvas.SetActive(false);
    }
    void SituationFunction()
    {
        if (m_currentSituation == LevelSituation.Move)
            Move();
        else if (m_currentSituation == LevelSituation.Stop)
            Stop();
        else if (m_currentSituation == LevelSituation.Rotate)
        {
            m_isRotatePerfect = false;
            Rotate();
        }
        else if ((m_currentSituation == LevelSituation.LookLens || m_currentSituation == LevelSituation.Clear) && Input.GetKeyDown(KeyCode.Escape))
        {
            ZoomOut();
        }
        else if (m_currentSituation == LevelSituation.LessRotate)
        {
            m_isRotatePerfect = true;
            LessRotate();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !LevelButton.Instance.IsShow)
        {
            ShootRay();
        }

        SituationFunction();

        if(m_currentSituation == LevelSituation.LookLens && m_isBlur == false && m_isRotatePerfect == true)
        {
            m_currentSituation = LevelSituation.Clear;
            LevelButton.Instance.ClearPart();
            m_targetNum++;          
            m_isBlur = true;
            m_isRotatePerfect = false;
        }
    }
}
