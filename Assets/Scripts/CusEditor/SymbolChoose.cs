using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolChoose : MonoBehaviour
{
    public SymbolbaseTime nodeType;

    // Use this for initialization
    void Start()
    {
        var sbs = GetComponentsInChildren<Toggle>();
        for (int i = 0; i < sbs.Length; i++)
        {
            var t = sbs[i].GetComponent<Toggle>();
            var n = i;
            t.onValueChanged.AddListener((_) => OnChooseSymbol(_, n));
        }
        CusCanvas.Instance.OnMouseDown += OnMousedown;

    }

    private void OnMousedown()
    {
        if (nodeType != SymbolbaseTime.none)
        {
            var a = CusCanvas.Instance.raycastResults[0];
            var b = a.gameObject.GetComponent<StaveBG>();
            if (b)
            {
                var m = b.transform.parent.GetComponent<Measure>();
                var ss = m.moveItem;
                Vector3 pos = Input.mousePosition;
                bool hasSb = false;
                for (int i = 0; i < ss.Count; i++)
                {
                    if (RectTransformUtility.RectangleContainsScreenPoint(ss[i], pos))
                    {
                        hasSb = true;
                        //ss[i].meterTime
                    }
                }
                if (!hasSb)
                {
                    AddSymbol(m);
                }
            }
        }
    }

    private void AddSymbol(Measure m)
    {
        var s = GameControl.Instance.prefabs["Symbol"];
        var sb = Instantiate(s, Input.mousePosition, Quaternion.identity, m.transform);
        var symbol = sb.GetComponent<Symbol>();
        symbol.SymbolbaseTime = nodeType;
        StartCoroutine(_SetGamutbyAnchoredpos(symbol));
        m.AddMoveItem(symbol.RectTransform);
    }

    IEnumerator _SetGamutbyAnchoredpos(Symbol symbol)
    {
        yield return null;
        var p = symbol.RectTransform.anchoredPosition;
        float y = p.y;
        p.y = 0;
        symbol.RectTransform.anchoredPosition = p;
        symbol.SymbolHeads[0].SetGamutbyAnchoredpos(y);
    }

    private void OnChooseSymbol(bool b, int i)
    {
        if (b)
        {
            nodeType = (SymbolbaseTime)i;
        }
        else
        {
            nodeType = SymbolbaseTime.none;
        }
    }

}
