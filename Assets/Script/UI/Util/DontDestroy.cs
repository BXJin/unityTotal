﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy<T> : MonoBehaviour where T : DontDestroy<T>
{
    public static T Instance { get; private set; }
    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            OnAwake();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == (T)this)
        {
            OnStart();
        }
    }
}
