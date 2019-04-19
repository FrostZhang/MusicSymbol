using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Instrument_Test : LayoutGroup
{
    public float childhight = 50;   //子高
    public float grouphight = 50;   //组间距
    public float staveHight = 80;   //大谱表间距

    public List<Group> groups;

    public override void CalculateLayoutInputVertical()
    {

    }

    public override void SetLayoutHorizontal()
    {
        int count = base.rectChildren.Count;
        float cw = base.padding.left;
        float maxWith = base.rectTransform.rect.size.x - base.padding.right;
        for (int i = 0; i < count; i++)
        {
            RectTransform rectC = base.rectChildren[i];
            int meaCount = rectC.childCount;
            int row = 0;    //行数
            float meacw = 0;
            for (int n = 0; n < meaCount; n++)
            {
                var rc = rectC.GetChild(n).GetComponent<RectTransform>();
                float cureentCw = rc.sizeDelta.x;
                if (cureentCw + cw >= maxWith)
                {
                    cureentCw = maxWith - cw - 1;
                    rc.sizeDelta = new Vector2(cureentCw, rc.sizeDelta.y);
                }
                if (meacw + cureentCw + cw > maxWith)
                {
                    row += 1;
                    meacw = 0;
                }

                base.SetChildAlongAxis(rc, 0, meacw);
                float ypos = 0;
                ypos = Mathf.Clamp(row, 0, 1) * (childhight * count + (count - 1) * grouphight + staveHight);
                base.SetChildAlongAxis(rc, 1, ypos, childhight);
                meacw += cureentCw;
            }
            base.SetChildAlongAxis(base.rectChildren[i], 0, cw, 0);
            base.SetChildAlongAxis(base.rectChildren[i], 1, i * (childhight + grouphight), 0);
        }
    }

    public override void SetLayoutVertical()
    {

    }

    public void Change()
    {
        Debug.Log(1);
        this.SetDirty();
    }

    protected override void Awake()
    {
        base.Awake();
        groups = GetComponentsInChildren<Group>().ToList();
    }

    public void OnGroupChange(int groupOrder,int groupChildOrder,float with)
    {
        for (int i = 0; i < groups.Count; i++)
        {
            if (i!=groupOrder)
            {
                groups[i].SetChildW(groupChildOrder, with);
            }
        }
        Change();
    }
}
