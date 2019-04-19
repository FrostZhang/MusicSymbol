using System;
using System.Collections;
using System.Collections.Generic;
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

    protected override void Awake()
    {
        base.Awake();
        tr = GetComponent<RectTransform>();
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
        min = 0;
        for (int i = 0; i < tr.childCount; i++)
        {
            min += tr.GetChild(i).GetComponent<RectTransform>().sizeDelta.x;
        }
    }

    private void OndragX(float c)
    {
        ChangeX(c);
    }

    public bool ChangeX(float c)
    {
        float wn = w + c;
        if (wn < min)
        {
            return false;
        }
        tr.sizeDelta = new Vector2(w + c, tr.sizeDelta.y);
        group.OnMeaChange(tr.GetSiblingIndex(), w + c);
        return true;
    }

}
