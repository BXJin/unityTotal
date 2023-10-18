using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void GoLobby()
    {
        SceneManager.LoadScene("MyLobby");
    }
    public void GoLevel()
    {
        SceneManager.LoadScene("Level");
    }
    public void GoExplainLevel()
    {
        SceneManager.LoadScene("ExplainLevel");
    }
    public void GoTotal()
    {
        SceneManager.LoadScene("Total");
    }
    public void GoExplainTotal()
    {
        SceneManager.LoadScene("ExplainTotal");
    }

}
