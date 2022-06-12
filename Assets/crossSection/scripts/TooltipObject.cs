using UnityEngine;
using System.Collections;

public class TooltipObject : MonoBehaviour
{
    private void OnMouseEnter()
    {
        ToolTipManager.SetCurrent(gameObject);
        Debug.Log(name);
    }

    private void OnMouseExit()
    {
        ToolTipManager.SetCurrent(null);
    }
}