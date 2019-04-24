using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteTest : MaskableGraphic
{
    int gamut;
    int tailDir;
    public void ReBuild(int gamut, int tailDir)
    {
        this.gamut = gamut;
        this.tailDir = tailDir;
        base.SetAllDirty();
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        toFill.Clear();
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
        if (gamut > 8)
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
