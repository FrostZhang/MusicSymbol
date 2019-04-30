using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Measure : UIBehaviour
{
    public Barline bar;
    RectTransform tr;

    private float w;
    private float min;
    Group group;

    public int meterX = 4;
    public int meterY = 4;

    public List<Symbol> symbols { get; protected set; }

    public List<RectTransform> moveItem { get; protected set; }

    protected override void Awake()
    {
        base.Awake();
        tr = GetComponent<RectTransform>();
        moveItem = new List<RectTransform>();
        if (tr.childCount>2)
        {
            for (int i = 1; i < tr.childCount; i++)
            {
                moveItem.Add(tr.GetChild(i).GetComponent<RectTransform>());
            }
        }
        bar.Onbegindrag = Onbegindrag;
        bar.OndragX = OndragX;
        bar.Onenddrag = Onenddrag;
    }

    protected override void Start()
    {
        if (!group)
        {
            group = tr.GetComponentInParent<Group>();
        }
    }

    private void Onenddrag(float c)
    {
        ChangeX(c);
        w = tr.sizeDelta.x;
    }

    private void Onbegindrag()
    {
        w = tr.sizeDelta.x;
        var r = tr.GetChild(tr.childCount - 1).GetComponent<RectTransform>();
        if (r)
        {
            min = r.anchoredPosition.x + r.sizeDelta.x;
        }
    }

    private void OndragX(float c)
    {
        ChangeX(c);
    }

    public bool ChangeX(float c)
    {
        float wn = w + c;
        //if (wn < min)
        //{
        //    if (MoveSyb(tr.childCount - 1, c))
        //    {
        //        tr.sizeDelta = new Vector2(w + c, tr.sizeDelta.y);
        //        group.OnMeaChange(tr.GetSiblingIndex(), w + c);
        //        return true;
        //    }
        //    return false;
        //}
        if (wn > min)
        {
            tr.sizeDelta = new Vector2(wn, tr.sizeDelta.y);
            group.OnMeaChange(tr.GetSiblingIndex(), w + c);
            return true;
        }
        return false;
        //return true;
    }

    private bool MoveSyb(int n, float c)
    {
        var sb = tr.GetChild(n).GetComponent<Symbol>();
        if (sb)
        {
            if (!sb.MoveX(c))
            {
                return MoveSyb(n - 1, c);
            }
            else
                return true;
        }
        else
        {
            return false;
        }
    }


    public void AddMoveItem(RectTransform moveit)
    {
        moveItem.Add(moveit);
        moveItem.Sort((x, y) => { return (int)(x.anchoredPosition.x - y.anchoredPosition.x); });
        //Hack 默认0是背景 1是小节线
        for (int i = 0; i < moveItem.Count; i++)
        {
            moveItem[i].SetSiblingIndex(i + 2);
        }
    }

}
