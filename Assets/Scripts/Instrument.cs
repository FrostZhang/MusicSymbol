using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instrument : LayoutGroup
{
    public float childhight = 50;   //子高
    public float grouphight = 50;   //组间距

    public override void CalculateLayoutInputVertical()
    {
        Debug.Log(base.rectTransform.rect.size);
    }

    public override void SetLayoutHorizontal()
    {
        Debug.Log("SetLayoutHorizontal");
        int count = base.rectChildren.Count;
        int temp = 0;
        float cw = base.padding.left;
        int row = 0;    //行数
        float maxWith = base.rectTransform.rect.size.x - base.padding.right;
        for (int i = 0; i < count; i++)
        {
            float cureentCw = base.rectChildren[i].sizeDelta.x;
            if (cw + cureentCw > maxWith)
            {
                row += 1;
                cw = base.padding.left;
            }

            base.SetChildAlongAxis(base.rectChildren[i], 0, cw);
            base.SetChildAlongAxis(base.rectChildren[i], 1, row * childhight + base.padding.top + 
                Mathf.Clamp(row, 0, 1) * grouphight, childhight);
            cw += cureentCw;
        }
    }

    public override void SetLayoutVertical()
    {

        Debug.Log("SetLayoutVertical");

    }

}