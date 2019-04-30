using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CusEditorLine : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private float old;
    private float fenbielv;

    public Vector2 limit;

    private Vector2 oldpa;
    public RectTransform pa;
    private void Awake()
    {
        pa = transform.parent.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        old = eventData.position.y;
        fenbielv = (800f / Screen.width);
        limit.x = 10;
        limit.y = Screen.height * 0.5f * fenbielv;
        oldpa = pa.sizeDelta;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float h = Mathf.Clamp(oldpa.y + (eventData.position.y - old) * fenbielv,
            limit.x, limit.y);
        pa.sizeDelta = new Vector2(oldpa.x, h);
        Debug.Log(h);
    }

}
