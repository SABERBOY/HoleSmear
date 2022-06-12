using UnityEngine;
using System.Collections;

public class GizmoHover : MonoBehaviour
{
    public Color hovercolor;
    private Color original;
    private Renderer rend;
    private bool selected;
    private float t = 0;


    private void Start()
    {
        rend = transform.GetComponent<Renderer>();
        original = rend.material.color;
    }

    private void OnMouseEnter()
    {
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

    private void OnMouseDown()
    {
        selected = true;
        if (Time.time - t < 0.3f)
        {
            SendMessageUpwards("ChangeMode");
            SetOriginal();
        }

        t = Time.time;
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