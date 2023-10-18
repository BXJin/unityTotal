using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TotalExplainManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_base;
    [SerializeField]
    TextMeshProUGUI m_tmpName;
    [SerializeField]
    TextMeshProUGUI m_tmpExplain;

    bool isShow = false;
    public void ShowToolTip(string name, string explain)
    {
        if (!isShow)
        {
            m_base.SetActive(true);

            // pos = pos * 1000f;

            var pos = new Vector3(m_base.GetComponent<RectTransform>().rect.width * 15f,
                                m_base.GetComponent<RectTransform>().rect.height * 1.6f,
                                0);

            m_base.transform.position = pos;
            m_tmpName.text = name;
            m_tmpExplain.text = explain;
            isShow = true;
        }
    }
    public void HideToolTip()
    {
        m_base.SetActive(false);
        isShow = false;
    }
    void PrintExplain(string name)
    {
        if (name == "way")
            ShowToolTip("수평 나사", "수평 미세 조정");
        else if (name == "lens")
            ShowToolTip("렌즈", "물체의 빛 받아 온다");
        else if (name == "Vertical")
            ShowToolTip("수직 나사", "미세 수직 이동, 고정");
        else if (name == "Horizontal")
            ShowToolTip("수평 나사", "미세 수평 이동, 고정");
        else if (name == "back_lens")
            ShowToolTip("안구 렌즈", "눈으로 관찰 렌즈, 초점 고정");
        else if (name == "Gusim")
            ShowToolTip("구심경", "구심점 관찰 렌즈");
    }
    void ShootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.blue);
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.transform.position);
            PrintExplain(hit.transform.tag);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootRay();
        }
    }
}
