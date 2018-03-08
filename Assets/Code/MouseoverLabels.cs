using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseoverLabels : MonoBehaviour {

    public string text;

    private bool show = false;


    private void OnMouseOver()
    {
        show = true;
    }

    private void OnMouseExit()
    {
        show = false;
    }

    private void OnGUI()
    {
        if (show)
        {
            GUI.Label(new Rect(Input.mousePosition.x + 10, Screen.height - (Input.mousePosition.y - 10), 500, 100), text);
        }
    }
}
