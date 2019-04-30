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
        int linnum = CalLineNum(zu.symbols, ref count);
    }

    private int CalLineNum(List<Symbol> symbols, ref int count)
    {
        Symbol f = symbols[0];
        Symbol e = symbols[count - 1];
        if (f.meterTime != e.meterTime)
        {
            Debug.Log("连接组的首尾时值必须一致");
            return 0;

        }
        foreach (var item in symbols)
        {
            if ((int)item.symbolbaseTime < 3)
            {
                Debug.Log("连接组的所有音符时值必须 < 4分音符");
                return 0;
            }
        }
        int i = (int)f.symbolbaseTime - 2;
        var fpos = f.SymbolHeads[f.SymbolHeads.Count - 1].fuwei.rectTransform.position;
        var epos = e.SymbolHeads[e.SymbolHeads.Count - 1].fuwei.rectTransform.position;

        Vector2 _fpos, _epos;
        _fpos = fpos- rectTransform.position ;
        _epos = epos-rectTransform.position ;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, fpos, Cam, out _fpos);
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, epos, Cam, out _epos);
        Debug.Log("开始连接" + _fpos + " " + _epos);
        //Debug.Log(a+" "+b);
        GetQuad(_fpos, _epos);
        return i;
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
