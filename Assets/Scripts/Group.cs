using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Group : MonoBehaviour
{
    RectTransform tr;
    Instrument_Test instrument;
    private void Awake()
    {
        tr = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (!instrument)
        {
            instrument = tr.GetComponentInParent<Instrument_Test>();
        }
    }

    public void OnMeaChange(int order, float with)
    {
        instrument.OnGroupChange(tr.GetSiblingIndex(), order, with);
    }

    public void SetChildW(int order, float with)
    {
        if (tr.childCount <= order)
        {
            Debug.LogError("每行不对称");
            return;
        }
        var rect = tr.GetChild(order).GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(with, rect.sizeDelta.y);
    }

}
