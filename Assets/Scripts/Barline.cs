using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class Barline : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
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
