using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelButton : SingletonMonoBehaviour<LevelButton>
{  
    [SerializeField]
    MachineController m_Level;
    [Header("Buttons")]
    [SerializeField]
    GameObject m_moveButton;
    [SerializeField]
    GameObject m_stopButton;
    [SerializeField]
    GameObject m_horizonButton;
    [SerializeField]
    GameObject m_rotateButton;
    [SerializeField]
    GameObject m_focusOnButton;
    [SerializeField]
    GameObject m_lessRotateButton;

    [Header("CheckList")]
    [SerializeField]
    GameObject m_checkList;
    [SerializeField]
    Image m_stopX;
    [SerializeField]
    Image m_stopY;
    [SerializeField]
    Image m_HorizonX;
    [SerializeField]
    Image m_HorizonY;
    [SerializeField]
    Image m_rotateX;
    [SerializeField]
    Image m_rotateY;
    [SerializeField]
    Image m_focusX;
    [SerializeField]
    Image m_focusY;
    [SerializeField]
    Image m_clearX;
    [SerializeField]
    Image m_clearY;

    [Header("Horizon")]
    [SerializeField]
    GameObject m_horizontal;
    [SerializeField]
    Image m_unSortBubble;
    [SerializeField]
    Image m_SortedBubble;

    GameObject m_base;
    bool m_isShow = false;
    public bool IsShow { get { return m_isShow; } }
    LevelSituation m_currentSituation;
    
    public void HitColliderButton(string name, LevelSituation situation)
    {
        m_currentSituation = situation;
        WhatButton(name);
    }
    public void MoveButton()
    {
       if(m_isShow)
       {
            m_currentSituation = LevelSituation.Move;
            m_Level.GetComponent<MachineController>().SetSituation(m_currentSituation);
            if(m_stopY.gameObject.activeSelf == true)
            {
                CheckX(m_stopX, m_stopY);
                CheckX(m_HorizonX, m_HorizonY);
                CheckX(m_rotateX, m_rotateY);
                CheckX(m_focusX, m_focusY);
                CheckX(m_clearX, m_clearY);
            }
       }
    }
    public void StopButon()
    {
        if (m_isShow)
        {
            m_currentSituation = LevelSituation.Stop;
            m_Level.GetComponent<MachineController>().SetSituation(m_currentSituation);
            CheckY(m_stopX, m_stopY);
        }
    }
    public void HorizonButton()
    {
        if (m_isShow)
        {
            m_currentSituation = LevelSituation.Horizontal;
            m_Level.GetComponent<MachineController>().SetSituation(m_currentSituation);
            CheckY(m_HorizonX, m_HorizonY);
            m_unSortBubble.gameObject.SetActive(false);
            m_SortedBubble.gameObject.SetActive(true);
        }
    }
    public void RotateButton()
    {
        if (m_isShow)
        {
            if(m_currentSituation == LevelSituation.Clear)
            {
                CheckX(m_focusX, m_focusY);
                CheckX(m_clearX, m_clearY);
            }
            m_currentSituation = LevelSituation.Rotate;
            m_Level.GetComponent<MachineController>().SetSituation(m_currentSituation);
            if(m_rotateX.gameObject.activeSelf == true )
                CheckY(m_rotateX, m_rotateY);
        }
    }
    public void FocusOnButton()
    {
        if(m_isShow)
        {
            m_currentSituation = LevelSituation.FocusOn;
            m_Level.GetComponent<MachineController>().SetSituation(m_currentSituation);
            m_Level.GetComponent<MachineController>().m_isBlur = false;
            CheckY(m_focusX, m_focusY);
        }
    }
    public void LessRotateButton()
    {
        if (m_isShow)
        {
            m_currentSituation = LevelSituation.LessRotate;
            m_Level.GetComponent<MachineController>().SetSituation(m_currentSituation);
        }
    }
    public void CloseButton()
    {
        m_base.SetActive(false);
        m_isShow = false;
        if (m_currentSituation == LevelSituation.Horizontal)
            m_horizontal.SetActive(false);
    }
    public void CheckListButton()
    {
        if (m_checkList.activeSelf == true)
            m_checkList.SetActive(false);
        else
            m_checkList.SetActive(true);
    }
    public void ClearPart()
    {
        CheckY(m_clearX, m_clearY);
    }
    
    void CheckX(Image imageX, Image imageY)
    {
        imageX.gameObject.SetActive(true);
        imageY.gameObject.SetActive(false);
    }    
    void CheckY(Image imageX, Image imageY)
    {
        imageX.gameObject.SetActive(false);
        imageY.gameObject.SetActive(true);
    }
    void WhatButton(string name)
    {
        if(name == "TriPod")
        {
            if(m_currentSituation == LevelSituation.Move)
            {
                if(!m_isShow)
                   m_stopButton.SetActive(true);
                m_base = m_stopButton;
                m_isShow = true;
            }
            else if(m_currentSituation == LevelSituation.Stop || m_currentSituation == LevelSituation.Clear)
            {
                if (!m_isShow)
                    m_moveButton.SetActive(true);
                m_base = m_moveButton;
                m_isShow = true;
            }
        }
        else if(name == "way")
        {
            if (m_currentSituation == LevelSituation.Stop)
            {
                if (!m_isShow)
                    m_horizonButton.SetActive(true);
                m_horizontal.SetActive(true);
                m_unSortBubble.gameObject.SetActive(true);
                m_SortedBubble.gameObject.SetActive(false);
                m_base = m_horizonButton;
                m_isShow = true;
            }
        }
        else if (name == "base")
        {
            if(m_currentSituation == LevelSituation.Horizontal || m_currentSituation == LevelSituation.Clear)
            {
                if (!m_isShow)
                    m_rotateButton.SetActive(true);
                m_base = m_rotateButton;
                m_isShow = true;
            }
        }
        else if(name == "handle")
        {
            if(m_currentSituation == LevelSituation.Rotate || m_currentSituation == LevelSituation.LessRotate)
            {
                if (!m_isShow)
                    m_focusOnButton.SetActive(true);
                m_base = m_focusOnButton;
                m_isShow = true;
            }
        }
        else if(name == "handle_base")
        {
            if (m_currentSituation == LevelSituation.FocusOn || m_currentSituation == LevelSituation.Rotate)
            {
                if (!m_isShow)
                    m_lessRotateButton.SetActive(true);
                m_base = m_lessRotateButton;
                m_isShow = true;
            }
        }
    }
    
}
