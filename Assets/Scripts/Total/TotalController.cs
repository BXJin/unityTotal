using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TotalSituation
{
    Move,
    Stop,
    HoriVerti,
    ToGusim,
    ToGusimPerfectly,
    RotateToPrism,
    RotateToPrismPerfectly,
    ClearPrism,
    HaveToMove,
    LookLens,
    LookGusim,
    FocusOn,
    LessRotate,
    max
}
public class TotalController : MonoBehaviour
{
    [SerializeField]
    GameObject m_buttonCanvas;
    [SerializeField]
    GameObject m_gusimScope;
    [SerializeField]
    GameObject m_scopeCamera;
    [SerializeField]
    float m_speed = 4f;
    [SerializeField]
    GameObject[] m_targetsPrism;
    [SerializeField]
    GameObject[] m_targetsPrismPerfect;
    [SerializeField]
    GameObject[] m_targetsGusim;
    [SerializeField]
    GameObject[] m_targetsGusimPerfect;
    [SerializeField]
    GameObject m_rotateObject;
    [SerializeField]
    GameObject m_rotateLens;
    [SerializeField]
    TotalSituation m_currentSituation = TotalSituation.Move;
    [SerializeField]
    int m_targetPrsimNum = 0;
    [SerializeField]
    int m_targetGusimNum = 0;
    public bool m_isBlur = true;
    bool m_isRotatePerfect = false;
    bool m_clearGusim = false;
    TotalSituation m_prevSituation;
    public void SetSituation(TotalSituation situation)
    {
        m_currentSituation = situation;
    }
    void RotateToPrism()
    {
        m_isBlur = true;
        var target = m_targetsPrism[m_targetPrsimNum].transform.position;
        Vector3 dir = target - m_rotateObject.transform.position;
        m_rotateObject.transform.rotation = Quaternion.Lerp(m_rotateObject.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1);
    }
    void RotateToPrismPerfectly()
    {
        //m_isBlur = true;
        var target = m_targetsPrismPerfect[m_targetPrsimNum].transform.position;
        Vector3 dir = target - m_rotateLens.transform.position;
        m_rotateLens.transform.rotation = Quaternion.Lerp(m_rotateLens.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1);
    }
    void LessRotate()
    {
        var target = m_targetsPrism[m_targetPrsimNum].transform.position;
        Vector3 dir = target - m_rotateObject.transform.position;
        m_rotateObject.transform.rotation = Quaternion.Lerp(m_rotateObject.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1);
    }
    void RotateToGusim()
    {
        var target = m_targetsGusim[m_targetGusimNum].transform.position;
        Vector3 dir = target - m_rotateObject.transform.position;
        m_rotateObject.transform.rotation = Quaternion.Lerp(m_rotateObject.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1);
    }
    void RotateToGusimPerfectly()
    {

        var target = m_targetsGusimPerfect[m_targetGusimNum].transform.position;
        Vector3 dir = target - m_rotateLens.transform.position;
        m_rotateLens.transform.rotation = Quaternion.Lerp(m_rotateLens.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1);
    }
    void Move()
    {
        var target = m_targetsGusimPerfect[m_targetGusimNum].transform.position;
        //Debug.Log(target);
        target.y = 0f;
        transform.position = Vector3.MoveTowards(transform.position, target, m_speed*0.1f);
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
            if (hit.transform.tag == "Gusim")
                GusimZoomIn();
            TotalButton.Instance.HitColliderButton(hit.transform.tag, m_currentSituation);
        }
    }
    void ZoomIn()
    {
        m_prevSituation = m_currentSituation;
        m_currentSituation = TotalSituation.LookLens;
        if (m_prevSituation == TotalSituation.ToGusimPerfectly)
            m_clearGusim = true;
        if (m_prevSituation == TotalSituation.RotateToPrismPerfectly)
            m_isRotatePerfect = true;
        m_buttonCanvas.SetActive(false);
        m_scopeCamera.SetActive(true);
        /*
        if (m_isBlur)
        {
            m_currentSituation = TotalSituation.LookLens;
            m_buttonCanvas.SetActive(false);
            m_scopeCamera.SetActive(true);
            //m_lensCanvas.SetActive(true);
            //m_blurEffect.SetActive(true);
        }
        else
        {
            m_currentSituation = TotalSituation.LookLens;
            m_buttonCanvas.SetActive(false);
            m_scopeCamera.SetActive(true);
           // m_lensCanvas.SetActive(true);
           // m_blurEffect.SetActive(false);
        }
        */
    }
    void GusimZoomIn()
    {
        m_prevSituation = m_currentSituation;
        m_currentSituation = TotalSituation.LookGusim;
        if (m_prevSituation == TotalSituation.Move)
            m_targetGusimNum++;
        m_buttonCanvas.SetActive(false);
        m_gusimScope.SetActive(true);
    }
    void ZoomOut()
    {
        m_currentSituation = m_prevSituation;
        m_buttonCanvas.SetActive(true);
        m_scopeCamera.SetActive(false);
        //m_lensCanvas.SetActive(false);
    }
    void GusimZoomOut()
    {
        m_currentSituation = TotalSituation.Stop;
        m_buttonCanvas.SetActive(true);
        m_gusimScope.SetActive(false);
    }
    void SituationFunction()
    {
        if (m_currentSituation == TotalSituation.Move)
            Move();
        //else if (m_currentSituation == LevelSituation.Stop)
            //Stop();
        else if (m_currentSituation == TotalSituation.ToGusim)
        {
            RotateToGusim();
        }
        else if (m_currentSituation == TotalSituation.ToGusimPerfectly)
        {
            RotateToGusimPerfectly();
        }
        else if (m_currentSituation == TotalSituation.RotateToPrism && m_clearGusim)
        {
            RotateToPrism();
        }
        else if(m_currentSituation == TotalSituation.RotateToPrismPerfectly)
        {
            RotateToPrismPerfectly();
        }
        
        else if (m_currentSituation == TotalSituation.LookLens && Input.GetKeyDown(KeyCode.Escape))
        {
            ZoomOut();
        }
        else if (m_currentSituation == TotalSituation.LookGusim && Input.GetKeyDown(KeyCode.Escape))
        {
            GusimZoomOut();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, 0f, -20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !TotalButton.Instance.IsShow)
        {
            ShootRay();
        }

        SituationFunction();
        
        if (m_currentSituation == TotalSituation.RotateToPrismPerfectly && m_isRotatePerfect == true)
        {
            m_currentSituation = TotalSituation.ClearPrism;
            m_targetPrsimNum++;
            if (m_targetPrsimNum == 2)
            {
                //m_targetGusimNum++;
                m_currentSituation = TotalSituation.HaveToMove;
            }              
            m_isRotatePerfect = false;
        }
    }
}
