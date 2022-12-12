using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aristas : MonoBehaviour
{
    public int id;
    public int idOrigen;
    public int idDestino;
    public int idPeso;
    public Renderer render;
    public Color defaultColor;
    public Color newColor;
    void Awake()
    {
        render = render.gameObject.GetComponent<Renderer>();
        render.material.color = defaultColor;
    }

    public void ChangeColor()
    {
        render.material.color = newColor;
    }
}
