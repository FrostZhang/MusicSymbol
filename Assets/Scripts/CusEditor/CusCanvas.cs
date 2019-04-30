using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CusCanvas : MonoBehaviour
{
    public static CusCanvas Instance;
    public GraphicRaycaster cusEditgraphicRaycaster;

    public List<RaycastResult> raycastResults;
    public List<RaycastResult> cusEditraycastResults;

    public Action OnMouseDown;

    private GraphicRaycaster graphicRaycaster;

    private void Awake()
    {
        Instance = this;
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        raycastResults = new List<RaycastResult>();
        cusEditraycastResults = new List<RaycastResult>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public List<RaycastResult> GetRaycastResults()
    {
        List<RaycastResult> list = new List<RaycastResult>();
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.pressPosition = Input.mousePosition;
        eventData.position = Input.mousePosition;
        graphicRaycaster.Raycast(eventData, list);
        return list;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;
            cusEditraycastResults.Clear();
            cusEditgraphicRaycaster.Raycast(eventData, cusEditraycastResults);
            if (cusEditraycastResults.Count>0)
            {
                return;
            }
            raycastResults.Clear();
            graphicRaycaster.Raycast(eventData, raycastResults);
            if (raycastResults.Count > 0)
            {
                OnMouseDown?.Invoke();
            }
        }
    }
}
