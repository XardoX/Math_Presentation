using System;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Action OnBeingDragged;

    [field: SerializeField]
    public bool IsDraggingEnabled { get; set; }

    [SerializeField]
    private bool enableSnapping = true;

    [SerializeField]
    private float snapDistance = 1f, lerpDuration = 0.5f;

    private float timeElapsed = 1000f;

    Vector3 mousePostion, newPos;

    private void Update()
    {
        if (!IsDraggingEnabled) return;
        if (timeElapsed < lerpDuration)
        {
            timeElapsed += Time.deltaTime;
            var lerpedPosition = Vector3.Lerp(transform.position, newPos, timeElapsed / lerpDuration);
            transform.position = lerpedPosition;
            OnBeingDragged?.Invoke();
        }
    }

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(transform.position);
    }

    private void OnMouseDown()
    {
        if (!IsDraggingEnabled) return;
        mousePostion = Input.mousePosition - GetMousePos();
        timeElapsed = 0;
    }

    private void OnMouseUp()
    {
        if (!IsDraggingEnabled) return;
    }

    private void OnMouseDrag()
    {
        if (!IsDraggingEnabled) return;
        newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePostion);

        if (enableSnapping && Input.GetKey(KeyCode.LeftControl))
        {
            newPos = new Vector3(
                RoundToNearestGrid(newPos.x),
                RoundToNearestGrid(newPos.y),
                newPos.z);
        }
        timeElapsed = 0;
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