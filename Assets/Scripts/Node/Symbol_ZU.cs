using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbol_ZU : LayoutGroup
{
    public List<Symbol> symbols=new List<Symbol>();
    [Range(0, 1)]
    public int tailDir;

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }

	
}
