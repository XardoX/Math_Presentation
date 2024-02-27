using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [field: SerializeField]
    public bool IsDraggingEnabled { get; set; }

    [SerializeField]
    private bool enableSnapping = true;
    [SerializeField]
    private float snapDistance = 1f;

    Vector3 mousePostion;



    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        if (!IsDraggingEnabled) return;
        mousePostion = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        if (!IsDraggingEnabled) return;
        var newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePostion);
        if (enableSnapping)
        {
            newPos = new Vector3(
                RoundToNearestGrid(newPos.x),
                RoundToNearestGrid(newPos.y),
                newPos.z);
        }
        transform.position = newPos;
    }

    private float RoundToNearestGrid(float pos)
    {
        float xDiff = pos % snapDistance;
        bool isPositive = pos > 0 ? true : false;
        pos -= xDiff;
        if (Mathf.Abs(xDiff) > (snapDistance / 2))
        {
            if (isPositive)
            {
                pos += snapDistance;
            }
            else
            {
                pos -= snapDistance;
            }
        }
        return pos;
    }
}