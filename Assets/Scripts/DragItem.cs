using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DragItem : UIBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action Onbegindrag;
    public Action<float> OndragX;
    public Action<float> Onenddrag;

    private float old;

    public void OnBeginDrag(PointerEventData eventData)
    {
        old = eventData.position.x;
        Onbegindrag?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        OndragX?.Invoke((eventData.position.x - old) * (800f / Screen.width));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Onenddrag?.Invoke((eventData.position.x - old) * (800f / Screen.width));
    }
}
