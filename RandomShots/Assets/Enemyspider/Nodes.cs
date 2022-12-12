using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Threading.Tasks;

public class Nodes : MonoBehaviour
{

    public int id;
    public Renderer render;
    public Color defaultColor;
    public Color newColor;
    void Awake()
    {
        render = gameObject.GetComponent<Renderer>();
        render.material.color = defaultColor;
    }

    public void ChangeColor()
    {
        render.material.color = newColor;
    }
    public void BackColorDefault()
    {
        render.material.color = defaultColor;
    }
}
