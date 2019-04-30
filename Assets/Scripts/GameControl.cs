using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{

    public static GameControl Instance;
    public Sprite[] tails;
    public Sprite[] nodeHeads;
    public Dictionary<string, GameObject> prefabs;

    Graphic[] graphics;
    Color[] graphicscolors;


    private void Awake()
    {
        Instance = this;
        graphics = new Graphic[2];
        graphicscolors = new Color[2];
        prefabs = new Dictionary<string, GameObject>();
        prefabs.Add("Symbol", Resources.Load<GameObject>("prefabs/Symbol"));
    }

    private void Start()
    {
        CusCanvas.Instance.OnMouseDown += OnMousedown;
    }

    private void OnMousedown()
    {
        var list = CusCanvas.Instance.raycastResults;
        if (list != null && list.Count > 0)
        {
            var g = list[0].gameObject.GetComponent<Graphic>();
            if (g)
            {
                ChooseGraphics(g);
            }
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void ChooseGraphics(Graphic gr)
    {
        if (graphics[0] == null)
        {
            graphics[0] = gr;
            graphicscolors[0] = gr.color;
            graphics[0].color = Color.red;
        }
        else if (graphics[1] == null)
        {
            graphics[0].color = Color.yellow;
            graphics[1] = gr;
            graphicscolors[1] = gr.color;
            graphics[1].color = Color.red;
        }
        else
        {
            if (graphics[1] == gr)
            {
                return;
            }
            graphics[0].color = graphicscolors[0];
            graphicscolors[0] = graphicscolors[1];
            graphics[0] = graphics[1];
            graphics[0].color = Color.yellow;
            graphics[1] = gr;
            graphicscolors[1] = gr.color;
            graphics[1].color = Color.red;
        }
    }
}
