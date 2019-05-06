using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol_ZU : LayoutGroup
{
    public List<Symbol> symbols=new List<Symbol>();
    [Range(0, 1)]
    public int tailDir;

    private ZuLine zuline;

    public ZuLine Zuline
    {
        get
        {
            if (!zuline)
            {
                if (rectChildren.Count>0)
                {
                    zuline = rectChildren[0].GetComponent<ZuLine>();
                }
            }
            return zuline;
        }
    }

    public Vector2 FirstPos { get; set;  }
    public float LinDir { get; set; } = 1;
    protected override void Awake()
    {
        base.Awake();
        foreach (var item in symbols)
        {
            item.symbol_zu = this;
        }
    }
    public override void CalculateLayoutInputVertical()
    {
        for (int i = 0; i < symbols.Count; i++)
        {
            var n = i;
            symbols[n].OnMove = SetLayoutHorizontal;
        }
    }

    public override void SetLayoutHorizontal()
    {
        Zuline.SetAllDirty();
    }

    public override void SetLayoutVertical()
    {
    }

	
}
