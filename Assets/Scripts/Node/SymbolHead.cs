using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SymbolHead : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public int gamut;
    public NoteTest nodeA;
    public RectTransform line;
    public Image fuwei;
    public Image nodehead;

    private Vector2 oldDrag;
    private float hpos;

    float jianju;
    float asp;

    int mingamut;
    int maxgamut;
    RectTransform targetline;
    RectTransform rectTransform;
    private int _gamut;

    Symbol symbol;
    public Symbol Symbol
    {
        get
        {
            if (symbol == null)
            {
                symbol = GetComponentInParent<Symbol>();
            }
            return symbol;
        }
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldDrag = eventData.position;
        hpos = rectTransform.anchoredPosition.y;
        jianju = rectTransform.sizeDelta.y / 2;
        asp = 800f / Screen.width;
        GetHLimit();
        Symbol.BeginMoveX();
    }

    private void GetHLimit()
    {
        int index = rectTransform.GetSiblingIndex();
        if (index > 0)
        {
            mingamut = Symbol.SymbolHeads[index - 1].gamut + 1;
        }
        else
            mingamut = -9;
        if (index < Symbol.SymbolHeads.Count - 1)
        {
            maxgamut = Symbol.SymbolHeads[index + 1].gamut - 1;
        }
        else
            maxgamut = 18;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 v = (eventData.position - oldDrag) * asp;
        gamut = (int)((hpos + v.y) / jianju);
        gamut = Mathf.Clamp(gamut, mingamut, maxgamut);
        if (_gamut != gamut)
        {
            _gamut = gamut;
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, gamut * jianju, 0);
            nodeA.ReBuild(gamut, Symbol.tailDir);
            Symbol.CalLine();
        }
        Symbol.MoveX(v.x);
    }

    public void SetGamutbyAnchoredpos(float anchoredPosY)
    {
        jianju = rectTransform.sizeDelta.y / 2;
        GetHLimit();
        gamut = (int)(anchoredPosY / jianju);
        gamut = Mathf.Clamp(gamut, mingamut, maxgamut);
        if (_gamut != gamut)
        {
            _gamut = gamut;
            rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, gamut * jianju, 0);
            nodeA.ReBuild(gamut, Symbol.tailDir);
            Symbol.CalLine();
        }
    }

    public void OnpaChangeH()
    {
        jianju = rectTransform.sizeDelta.y / 2;
        rectTransform.anchoredPosition = new Vector3(rectTransform.anchoredPosition.x, gamut * jianju, 0);
    }

}
