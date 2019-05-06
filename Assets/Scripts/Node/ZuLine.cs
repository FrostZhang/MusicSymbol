using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZuLine : MaskableGraphic
{
    Symbol_ZU zu;
    private Camera cam;

    public Symbol_ZU Zu
    {
        get
        {
            if (!zu)
            {
                zu = GetComponentInParent<Symbol_ZU>();
            }
            return zu;
        }
    }

    public Camera Cam
    {
        get
        {
            if (!cam)
            {
                cam = Camera.main.GetComponent<Camera>();
            }
            return cam;
        }
    }


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        int count = Zu.symbols.Count;
        if (count < 2)
        {
            Debug.Log("连接组数量必须 >=2");
            return;
        }
        Symbol f = Zu.symbols[0];
        Symbol e = Zu.symbols[count - 1];
        foreach (var item in Zu.symbols)
        {
            if ((int)item.symbolbaseTime < 3)
            {
                Debug.Log("连接组的所有音符时值必须 < 4分音符");
            }
        }
        int i = (int)f.symbolbaseTime - 2;
        var fpos = f.SymbolHeads[f.SymbolHeads.Count - 1].fuwei.rectTransform.position;
        zu.FirstPos = fpos;
        var epos = e.SymbolHeads[e.SymbolHeads.Count - 1].fuwei.rectTransform.position;
        Vector2 _fpos, _epos;
        _fpos = fpos - rectTransform.position;
        float Fenbianlv = 800f / Screen.width;
        _fpos *= Fenbianlv;
        _epos = epos - rectTransform.position;
        _epos *= Fenbianlv;
        Vector2 dir = _epos - _fpos;
        zu.LinDir = dir.x / dir.y; //将斜率赋值

        var et = Zu.symbols[0];

        vh.AddUIVertexQuad(GetQuad(_fpos, _epos));
    }

    private UIVertex[] GetQuad(Vector2 startPos, Vector2 endPos)
    {
        var dis = Vector2.Distance(startPos, endPos);
        var y = 1 * (endPos.x - startPos.x) / dis;
        var x = 1 * (endPos.y - startPos.y) / dis;
        if (y <= 0) y = -y;
        else x = -x;
        var vertex = new UIVertex[4];
        vertex[0].position = new Vector3(startPos.x + x, startPos.y + y);
        vertex[1].position = new Vector3(endPos.x + x, endPos.y + y);
        vertex[2].position = new Vector3(endPos.x - x, endPos.y - y);
        vertex[3].position = new Vector3(startPos.x - x, startPos.y - y);
        for (var i = 0; i < vertex.Length; i++) vertex[i].color = Color.black;
        return vertex;
    }
}
