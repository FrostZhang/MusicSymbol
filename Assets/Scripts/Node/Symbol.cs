﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Symbol : LayoutGroup
{
    [Header("符尾 0朝上 1朝下")]
    [Range(0, 1)]
    public int tailDir;

    [Header("时值")]
    public SymbolbaseTime symbolbaseTime;
    public float meterTime = 0.5f;

    //归宿于组员
    public Symbol_ZU symbol_zu { get; set; }
    //当归属于组的时候  获得其交叉点
    public Vector2 zupos { get; set; }

    private bool symbolTimeZero;
    private SymbolbaseTime _last_symbolTime = SymbolbaseTime.none;
    public List<SymbolHead> SymbolHeads
    {
        get
        {
            if (symbolHeads.Count == rectChildren.Count)
            {
                return symbolHeads;
            }
            else
            {
                symbolHeads.Clear();
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    symbolHeads.Add(rectChildren[i].GetComponent<SymbolHead>());
                }
                return symbolHeads;
            }
        }
    }

    public RectTransform Parent
    {
        get
        {
            if (!_parent)
            {
                _parent = rectTransform.parent.GetComponent<RectTransform>();
            }
            return _parent;
        }
    }

    public new RectTransform rectTransform { get { return base.rectTransform; } }

    public Action OnMove;

    private Vector2 moveLimit;  //移动的时候限制左右
    private Vector2 oldPos;  //移动的时候限制左右
    private RectTransform _parent;
    List<SymbolHead> symbolHeads = new List<SymbolHead>();

    protected override void Awake()
    {
        base.Awake();
    }

    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {
        float h = rectTransform.rect.size.y / 4;
        rectTransform.sizeDelta = new Vector2(2 * h, 0);
        foreach (var item in rectChildren)
        {
            if (symbolbaseTime == SymbolbaseTime.node0)
            {
                item.sizeDelta = new Vector2(h * 1.5f, h);
                item.anchoredPosition = new Vector2(h * 1.5f, item.anchoredPosition.y);
            }
            else
            {
                item.sizeDelta = new Vector2(h, h);
                item.anchoredPosition = new Vector2(h, item.anchoredPosition.y);
            }
            item.localEulerAngles = new Vector3(180 * tailDir, 0, 0);
        }

        if (symbolbaseTime == SymbolbaseTime.node0)
        {
            if (!symbolTimeZero)
            {
                symbolTimeZero = true;
                for (int i = 0; i < rectChildren.Count; i++)
                {
                    rectChildren[i].GetChild(1).GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
        else
        {
            if (symbolTimeZero)
            {
                symbolTimeZero = false;
                ActiveLine(tailDir);
            }
        }
        if (symbolbaseTime != _last_symbolTime)
        {
            SyT();
            _last_symbolTime = symbolbaseTime;
        }
        CalLine();
    }

    float fenbianlv => 800f / Screen.width;
    //计算符尾长度
    public void CalLine()
    {
        if (symbolTimeZero)
        {
            return;
        }

        if (symbol_zu != null && symbol_zu.symbols[0] != this && symbol_zu.symbols[symbol_zu.symbols.Count - 1] != this)
        {
            Zuline();
        }
        else
        {
            float lineH = rectTransform.rect.size.y - 7;
            Line(lineH);
        }

    }
    //计算符尾长度 组模式
    private void Zuline()
    {
        int i = tailDir == 0 ? 0 : rectChildren.Count - 1;
        RectTransform rect = rectChildren[i].GetChild(1).GetComponent<RectTransform>();
        Vector2 p = rect.position;
        var y = (p.x - symbol_zu.FirstPos.x) / symbol_zu.LinDir + symbol_zu.FirstPos.y;
        Debug.Log(symbol_zu.FirstPos + " " + symbol_zu.LinDir + " " + y);
        zupos = new Vector2(p.x, y);
        float lineH = (zupos - p).magnitude * fenbianlv;
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, lineH);
    }
    //计算符尾长度 非组或组员边界
    private void Line(float lineH)
    {
        if (rectChildren.Count < 2)
        {
            RectTransform rect = rectChildren[0].GetChild(1).GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, lineH);
            return;
        }
        if (tailDir == 0)
        {
            Vector3 upPos = rectChildren[rectChildren.Count - 1].GetChild(1).position;
            RectTransform rect = rectChildren[0].GetChild(1).GetComponent<RectTransform>();
            Vector3 downPos = rect.position;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, Vector2.Distance(upPos, downPos) * fenbianlv + lineH);
        }
        else
        {
            Vector3 upPos = rectChildren[0].GetChild(1).position;
            RectTransform rect = rectChildren[rectChildren.Count - 1].GetChild(1).GetComponent<RectTransform>();
            Vector3 downPos = rect.position;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, Vector2.Distance(upPos, downPos) * fenbianlv + lineH);
        }
    }

    //更新时值 更新音符尾巴图
    private void SyT()
    {
        meterTime = 8;
        int n = (int)symbolbaseTime;
        for (int i = 0; i < n + 1; i++)
        {
            meterTime *= 0.5f;
        }
        if (Application.isPlaying)
        {
            int wei = Mathf.Max(-1, n - 3);
            int head = Mathf.Clamp(n, 0, 2);
            for (int i = 0; i < SymbolHeads.Count; i++)
            {
                SymbolHeads[i].nodehead.sprite = GameControl.Instance.nodeHeads[head];
                if (wei < 0 || symbol_zu != null)
                {
                    SymbolHeads[i].fuwei.CrossFadeAlpha(0, 0, true);
                }
                else
                {
                    SymbolHeads[i].fuwei.CrossFadeAlpha(1, 0, true);
                    SymbolHeads[i].fuwei.sprite = GameControl.Instance.tails[wei];
                }
            }
        }
    }

    //对音符时  隐藏竖线
    private void ActiveLine(int tailDir)
    {
        Debug.Log("ActiveLine");
        if (tailDir == 0)
        {
            for (int i = 1; i < rectChildren.Count; i++)
            {
                rectChildren[i].GetChild(1).GetComponent<CanvasGroup>().alpha = 0;
            }
            rectChildren[0].GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
        }
        else
        {
            for (int i = 0; i < rectChildren.Count - 1; i++)
            {
                rectChildren[i].GetChild(1).GetComponent<CanvasGroup>().alpha = 0;
            }
            rectChildren[rectChildren.Count - 1].GetChild(1).GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    public void BeginMoveX()
    {
        int i = rectTransform.GetSiblingIndex();
        Transform left = Parent.GetChild(i - 1);
        if (left && left.name.Contains("Symbol"))
        {
            var rect = left.GetComponent<RectTransform>();
            moveLimit.x = rect.anchoredPosition.x + rect.sizeDelta.x;
        }
        else
        {
            moveLimit.x = 0;
        }
        if (Parent.childCount == i + 1)
        {
            moveLimit.y = Parent.sizeDelta.x - rectTransform.sizeDelta.x;
        }
        else
        {
            Transform right = Parent.GetChild(i + 1);
            if (right && right.name.Contains("Symbol"))
            {
                var rect = right.GetComponent<RectTransform>();
                moveLimit.y = rect.anchoredPosition.x - rectTransform.sizeDelta.x;
            }
        }
        oldPos = rectTransform.anchoredPosition;
    }

    public bool MoveX(float value)
    {
        OnMove?.Invoke();
        float x = oldPos.x + value;
        if (x > moveLimit.x && x < moveLimit.y)
        {
            rectTransform.anchoredPosition = new Vector2(x, oldPos.y);
            return true;
        }
        x = Mathf.Clamp(x, moveLimit.x, moveLimit.y);
        rectTransform.anchoredPosition = new Vector2(x, oldPos.y);
        return false;
    }
}

public enum SymbolbaseTime
{
    node0, node2, node4, node8, node16, node32, node64, none
}