using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeLineTest : MaskableGraphic
{
    Symbol symbol;
    private float fenbianlv => 800f / Screen.width;
    public RectTransform weiba;
    public Symbol Symbol
    {
        get
        {
            if (!symbol)
            {
                symbol = GetComponent<Symbol>();
            }
            return symbol;
        }
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
        UnderLine(toFill);
        Line(toFill);
    }

    private void UnderLine(VertexHelper toFill)
    {
        AddUnderLine(toFill, 0);
        if (Symbol.SymbolHeads.Count > 1)
        {
            AddUnderLine(toFill, Symbol.SymbolHeads.Count - 1);
        }
    }

    private void AddUnderLine(VertexHelper toFill, int yinfu)
    {
        var head = Symbol.SymbolHeads[yinfu];
        var size = GetPixelAdjustedRect().size;
        var lenght = Symbol.RectChildren[yinfu].rect.width + 4;
        var h = size.y / 4;
        if (head.gamut < 0)
        {
            int g = Mathf.Abs(head.gamut);
            int count = g / 2;
            //float mod = (g % 2) * 0.5f;
            for (int i = 0; i < count; i++)
            {
                toFill.AddUIVertexQuad(GetQuad(new Vector2(-2, -h * (i + 1)),
                    new Vector2(lenght, -h * (i + 1))));
            }
        }
        else if (head.gamut > 8)
        {
            int g = head.gamut - 8;
            int count = g / 2;
            for (int i = 0; i < count; i++)
            {
                toFill.AddUIVertexQuad(GetQuad(new Vector2(-2, size.y + h * (i + 1)),
                    new Vector2(lenght, size.y + h * (i + 1))));
            }
        }
    }

    private void Line(VertexHelper toFill)
    {
        var size = GetPixelAdjustedRect().size;
        Vector2 pos = Symbol.RectTransform.position;
        if (Symbol.tailDir == 0)
        {
            Vector2 downPos = Symbol.RectChildren[0].position;
            Vector2 st = (downPos - pos)* fenbianlv - new Vector2(1f, 0);
            float dis = 0;
            if (Symbol.RectChildren.Count > 0)
            {
                Vector2 upPos = Symbol.RectChildren[Symbol.RectChildren.Count - 1].position;
                dis = Vector2.Distance(upPos, downPos);
            }
            Vector2 et = new Vector2(st.x, (st.y + dis + Symbol.RectTransform.rect.height) * fenbianlv);
            weiba.localPosition = et;
            weiba.localEulerAngles = Vector3.zero;
            Debug.Log(st + "  " + et);
            toFill.AddUIVertexQuad(GetQuad(st, et));
        }
        else
        {
            var a1 = Symbol.RectChildren[Symbol.RectChildren.Count - 1];
            Vector2 downPos = a1.position;
            Vector2 st = (downPos - pos) * fenbianlv - new Vector2(a1.rect.width-1, 0);
            float dis = 0;
            if (Symbol.RectChildren.Count > 0)
            {
                Vector2 upPos = Symbol.RectChildren[0].position;
                dis = Vector2.Distance(upPos, downPos);
            }
            Vector2 et = new Vector2(st.x, (st.y - dis - Symbol.RectTransform.rect.height+ a1.rect.height*0.5f) * fenbianlv);
            weiba.localPosition = et;
            weiba.localEulerAngles = new Vector3(180, 0, 0);
            Debug.Log(st + "  " + et);
            toFill.AddUIVertexQuad(GetQuad(st, et));
        }
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
        for (var i = 0; i < vertex.Length; i++) vertex[i].color = Color.red;
        return vertex;
    }
}
