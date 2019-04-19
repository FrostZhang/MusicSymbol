using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
[RequireComponent(typeof(Text))]
public class Note : MonoBehaviour
{
    public NodeType nodeType;
    private Text t;
    private void Awake()
    {
        t = GetComponent<Text>();
    }

#if UNITY_EDITOR
    // Update is called once per frame
    void Update()
    {
        switch (nodeType)
        {
            case NodeType.node1:
                t.text = "a";
                break;
            case NodeType.node2:
                t.text = "b";
                break;
            case NodeType.node4:
                t.text = "c";
                break;
            case NodeType.node8:
                t.text = "d";
                break;
            case NodeType.node16:
                t.text = "e";
                break;
            case NodeType.node32:
                t.text = "f";
                break;
            default:
                break;
        }
    }
#endif

}

public enum NodeType
{
    node1 = 1, node2, node4, node8, node16, node32, _node1 = 1, _node2, _node4, _node8, _node16, _node32
}