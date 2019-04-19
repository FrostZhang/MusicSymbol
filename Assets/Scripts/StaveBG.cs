using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaveBG : MaskableGraphic
{
    [Range(2, 8)]
    public int lineNum = 5;

    protected override void OnEnable()
    {
        base.OnEnable();
        rectTransform.pivot = Vector2.zero;
    }

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        var size = GetPixelAdjustedRect().size;
        var lenght = size.x;
        var h = size.y / (lineNum - 1);
        for (int i = 0; i < lineNum; i++)
        {
            vh.AddUIVertexQuad(GetQuad(new Vector2(0, i * h), new Vector2(lenght, i * h)));
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
        for (var i = 0; i < vertex.Length; i++) vertex[i].color = Color.black;
        return vertex;
    }
}
