using System;
using UnityEngine;

public class MyVector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private string id;

    [SerializeField]
    private bool showArrow;

    [SerializeField]
    private bool showLine;

    [SerializeField]
    private Color color;

    [Header("References")]
    [SerializeField]
    private SpriteRenderer arrow;

    [SerializeField]
    private SpriteRenderer line;

    [SerializeField]
    private SpriteRenderer point;

    [SerializeField]
    private SpriteMask mask;

    public Action<MyVector> OnSelected;

    public string Id => id;

    public Color Color => color;

    public void ToggleArrow(bool toggle)
    {
        arrow.gameObject.SetActive(toggle);
        showArrow = toggle;
    }

    public void ToggleLine(bool toggle)
    {
        showLine = toggle;
        line.gameObject.SetActive(toggle);
    }

    public void SetColor(Color color)
    {
        this.color = color;
        arrow.color = color;
        line.color = color;
        point.color = color;
    }

    private void Start()
    {
        SetColor(color);
    }

    private void LateUpdate()
    {
        arrow.size = new Vector2(transform.position.magnitude, arrow.size.y);
        var direction = Quaternion.FromToRotation(Vector3.right, transform.position);
        arrow.transform.rotation = direction;
        line.transform.rotation = direction;
        mask.transform.localScale = Vector3.one* transform.position.magnitude;

        line.transform.position = Vector3.Lerp(Vector3.zero, transform.position, 0.5f);
        line.transform.localScale = new Vector3(transform.position.magnitude, line.transform.localScale.y, line.transform.localScale.z);

    }

    private void OnMouseDown()
    {
        OnSelected?.Invoke(this);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ToggleArrow(showArrow);  
        ToggleLine(showLine);
        SetColor(color);
    }
#endif
}
