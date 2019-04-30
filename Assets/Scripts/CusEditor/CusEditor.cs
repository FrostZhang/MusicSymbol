using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CusEditor : MonoBehaviour
{
    public static CusEditor Instance;

    public float hight;
    RectTransform rectTransform;
    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Start()
    {
        
        
    }

    private void Onbegindrag()
    {
        
    }
    

}
