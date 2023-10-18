using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalButton : SingletonMonoBehaviour<TotalButton>
{
    [SerializeField]
    TotalController m_Total;
    [SerializeField]
    GameObject m_wayButton;
    [SerializeField]
    GameObject m_baseGusimButton;
    [SerializeField]
    GameObject m_LookGusimButton;
    [SerializeField]
    GameObject m_RotateToPrismButton;
    [SerializeField]
    GameObject m_RotateToPrismPerfectlyButton;
    [SerializeField]
    GameObject m_moveToNextGusimButton;

    GameObject m_base;
    bool m_isShow = false;
    public bool IsShow { get { return m_isShow; } }
    TotalSituation m_currentSituation;
    public void HitColliderButton(string name, TotalSituation situation)
    {
        m_currentSituation = situation;
        WhatButton(name);
    }
    public void WayButton()
    {
        if (m_isShow)
        {
            m_currentSituation = TotalSituation.HoriVerti;
            m_Total.GetComponent<TotalController>().SetSituation(m_currentSituation);
        }
    }
    public void BaseGusimButton()
    {
        if (m_isShow)
        {
            m_currentSituation = TotalSituation.ToGusim;
            m_Total.GetComponent<TotalController>().SetSituation(m_currentSituation);
        }
    }
    public void LookGusimPerfectly()
    {
        if (m_isShow)
        {
            m_currentSituation = TotalSituation.ToGusimPerfectly;
            m_Total.GetComponent<TotalController>().SetSituation(m_currentSituation);
        }
    }
    public void RotateToPrismButton()
    {
        if (m_isShow)
        {
            m_currentSituation = TotalSituation.RotateToPrism;
            m_Total.GetComponent<TotalController>().SetSituation(m_currentSituation);
        }
    }
    public void RotateToPrismPerfectlyButton()
    {
        if (m_isShow)
        {
            m_currentSituation = TotalSituation.RotateToPrismPerfectly;
            m_Total.GetComponent<TotalController>().SetSituation(m_currentSituation);
        }
    }
    public void MoveToNextGusim()
    {
        if(m_isShow)
        {
            m_currentSituation = TotalSituation.Move;
            m_Total.GetComponent<TotalController>().SetSituation(m_currentSituation);
        }
    }
    public void CloseButton()
    {
        m_base.SetActive(false);
        m_isShow = false;
    }
    void WhatButton(string name)
    {
        if (name == "way")
        {
            if (m_currentSituation == TotalSituation.Stop)
            {
                if (!m_isShow)
                    m_wayButton.SetActive(true);
                m_base = m_wayButton;
                m_isShow = true;
            }
        }
        else if (name == "base")
        {
            if (m_currentSituation == TotalSituation.HoriVerti)
            {
                if (!m_isShow)
                    m_baseGusimButton.SetActive(true);
                m_base = m_baseGusimButton;
                m_isShow = true;
            }
            else if(m_currentSituation == TotalSituation.ToGusimPerfectly || m_currentSituation == TotalSituation.ClearPrism)
            {
                if (!m_isShow)
                    m_RotateToPrismButton.SetActive(true);
                m_base = m_RotateToPrismButton;
                m_isShow = true;
            }
        }
        else if(name == "baseLens")
        {
            if (m_currentSituation == TotalSituation.ToGusim)
            {
                if (!m_isShow)
                    m_LookGusimButton.SetActive(true);
                m_base = m_LookGusimButton;
                m_isShow = true;
            }
            if (m_currentSituation == TotalSituation.RotateToPrism)
            {
                if (!m_isShow)
                    m_RotateToPrismPerfectlyButton.SetActive(true);
                m_base = m_RotateToPrismPerfectlyButton;
                m_isShow = true;
            }
        }
        else if(name == "TriPod")
        {
            if(m_currentSituation == TotalSituation.HaveToMove)
            {
                if (!m_isShow)
                    m_moveToNextGusimButton.SetActive(true);
                m_base = m_moveToNextGusimButton;
                m_isShow = true;
            }
        }
    }
    // Start is called before the first frame update

}
