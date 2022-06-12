using UnityEngine;
using System.Collections;

public class PlaneHover : MonoBehaviour
{
    public Color hovercolor;
    private Color original;
    private Renderer rend;
    private bool selected;


    private void Start()
    {
        rend = transform.GetComponent<Renderer>();
        original = rend.material.color;
    }

    private void OnMouseEnter()
    {
        //GetComponent<Renderer>().enabled = true;
        SetHovered();
    }

    private void OnMouseExit()
    {
        if (!selected)
            SetOriginal();
    }

    private void SetHovered()
    {
        rend.material.color = hovercolor;
    }

    private void SetOriginal()
    {
        rend.material.color = original;
    }

    private void Update()
    {
        if (selected && Input.GetMouseButtonUp(0))
        {
            SetOriginal();
            selected = false;
        }
    }
}