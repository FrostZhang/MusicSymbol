using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteTest : MaskableGraphic
{
    int gamut;
    int tailDir;
    Symbol symbol;
    private float fenbianlv => 800f / Screen.width;

    public Symbol Symbol
    {
        get
        {
            if (!symbol)
            {
                symbol = GetComponentInParent<Symbol>();
            }
            return symbol;
        }
    }

    public void ReBuild(int gamut, int tailDir)
    {
        this.gamut = gamut;
        this.tailDir = tailDir;
        base.SetAllDirty();
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
        //UnderLine(toFill);
        //Line(toFill);
    }

    private void UnderLine(VertexHelper toFill)
    {
        if (gamut < 0)
        {
            int g = Mathf.Abs(gamut);
            int count = g / 2;
            float mod = ((g + 1) % 2) * 0.5f;
            var size = GetPixelAdjustedRect().size;
            var lenght = size.x + 2;
            if (tailDir == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    toFill.AddUIVertexQuad(GetQuad(new Vector2(-2, size.y * (i + 1) - mod * size.y),
                        new Vector2(lenght, size.y * (i + 1) - mod * size.y)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    toFill.AddUIVertexQuad(GetQuad(new Vector2(-2, -size.y * i + mod * size.y),
                        new Vector2(lenght, -size.y * i + mod * size.y)));
                }
            }
        }
        else if (gamut > 8)
        {
            int g = gamut - 8;
            int count = g / 2;
            float mod = ((g + 1) % 2) * 0.5f;
            var size = GetPixelAdjustedRect().size;
            var lenght = size.x + 2;

            if (tailDir == 0)
            {
                for (int i = 0; i < count; i++)
                {
                    toFill.AddUIVertexQuad(GetQuad(new Vector2(-2, -size.y * i + mod * size.y),
                        new Vector2(lenght, -size.y * i + mod * size.y)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    toFill.AddUIVertexQuad(GetQuad(new Vector2(-2, size.y * (i + 1) - mod * size.y),
                        new Vector2(lenght, size.y * (i + 1) - mod * size.y)));
                }
            }
        }
    }

    private void Line(VertexHelper toFill)
    {
        var size = GetPixelAdjustedRect().size;
        if (tailDir == 0)
        {
            Vector2 st = new Vector2(size.x - 1, size.y - 2);
            float dis = 0;
            if (Symbol.RectChildren.Count > 0)
            {
                Vector2 upPos = Symbol.RectChildren[Symbol.RectChildren.Count - 1].position;
                Vector2 downPos = Symbol.RectChildren[0].position;
                dis = Vector2.Distance(upPos, downPos);
            }
            Vector2 et = new Vector2(st.x, st.y + dis + Symbol.RectTransform.rect.size.y);
            Debug.Log(st + "  " + et);
            toFill.AddUIVertexQuad(GetQuad(st, et));
        }

        //if (Symbol.RectChildren.Count < 2)
        //{
        //    RectTransform rect = Symbol.RectChildren[0].GetChild(1).GetComponent<RectTransform>();
        //    rect.sizeDelta = new Vector2(rect.sizeDelta.x, lineH);
        //    return;
        //}
        //if (tailDir == 0)
        //{
        //    Vector3 upPos = Symbol.RectChildren[Symbol.RectChildren.Count - 1].GetChild(1).position;
        //    RectTransform rect = Symbol.RectChildren[0].GetChild(1).GetComponent<RectTransform>();
        //    Vector3 downPos = rect.position;
        //    rect.sizeDelta = new Vector2(rect.sizeDelta.x, Vector2.Distance(upPos, downPos) * fenbianlv + lineH);
        //}
        //else
        //{
        //    Vector3 upPos = Symbol.RectChildren[0].GetChild(1).position;
        //    RectTransform rect = Symbol.RectChildren[Symbol.RectChildren.Count - 1].GetChild(1).GetComponent<RectTransform>();
        //    Vector3 downPos = rect.position;
        //    rect.sizeDelta = new Vector2(rect.sizeDelta.x, Vector2.Distance(upPos, downPos) * fenbianlv + lineH);
        //}
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
