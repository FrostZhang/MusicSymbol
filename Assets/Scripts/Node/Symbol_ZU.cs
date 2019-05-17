using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol_ZU : MonoBehaviour
{
    public List<Symbol> symbols=new List<Symbol>();
    [Range(0, 1)]
    public int tailDir;

    private ZuLine zuline;
    private RectTransform _rectTransform;

    public ZuLine Zuline
    {
        get
        {
            if (!zuline)
            {
                if (RectTransform.childCount>0)
                {
                    zuline = RectTransform.GetChild(0).GetComponent<ZuLine>();
                }
            }
            return zuline;
        }
    }

    public RectTransform RectTransform {
        get {
            if (!_rectTransform)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
            return _rectTransform;
        }
    }

    public Vector2 FirstPos { get; set;  }
    public float LinDir { get; set; } = 1;
    protected void Awake()
    {
        foreach (var item in symbols)
        {
            item.symbol_zu = this;
        }
        for (int i = 0; i < symbols.Count; i++)
        {
            var n = i;
            symbols[n].OnMove = SetLayoutHorizontal;
        }
    }

    public void SetLayoutHorizontal()
    {

        StartCoroutine(_SetDirty());
    }

    IEnumerator _SetDirty()
    {
        Zuline.CalDir();
        foreach (var item in symbols)
        {
            item.CalLine();
        }
        yield return null;
        Zuline.SetAllDirty();
    }
	
}
