using System;
using UnityEngine;

public class MyVector : MonoBehaviour
{
    public Action<MyVector> OnSelected;

    public Action<MyVector> OnDisabled;

    [Header("Settings")]
    [SerializeField]
    private string id;

    [SerializeField]
    private bool showArrow;

    [SerializeField]
    private bool arrowFromPointZero;

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

    [SerializeField]
    private DragAndDrop dragAndDrop;

    private Quaternion rotation;

    private bool isFree = true;

    public string Id => id;

    public Vector3 Value
    {
        get => transform.position;
        set => transform.position = value;
    }

    public Vector3 Normalized => transform.position.normalized;

    public Quaternion Rotation => rotation;

    public float Length => transform.position.magnitude;

    public float SqrLength => transform.position.sqrMagnitude;

    public Color Color => color;

    public bool IsFree => isFree;

    public void Init(Vector3 value, bool interactable = true, bool arrow = true, bool arrowType = false, bool line = false)
    {
        transform.position = value;
        ToggleArrow(arrow);
        SetArrowType(arrowType);
        ToggleLine(line);

        dragAndDrop.IsDraggingEnabled = interactable;
    }

    public void Toggle(bool value)
    {
        isFree = !value;
        gameObject.SetActive(value);
    }

    public void SetId(string id) => this.id = id;

    public void ToggleArrow(bool toggle)
    {
        arrow.gameObject.SetActive(toggle);
        showArrow = toggle;
    }

    public void SetArrowType(bool fromPointZero)
    {
        if(fromPointZero)
        {
            arrow.transform.position = Vector3.zero;
        }
        else
        {
            arrow.transform.position = transform.position;
        }
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
        rotation = Quaternion.FromToRotation(Vector3.right, transform.position);

        arrow.transform.rotation = rotation;
        line.transform.rotation = rotation;

        mask.transform.localScale = Vector3.one * transform.position.magnitude;

        line.transform.position = Vector3.Lerp(Vector3.zero, transform.position, 0.5f);
        line.size = new Vector2(transform.position.magnitude, line.size.y);

        if (arrowFromPointZero)
        {
            arrow.transform.position = Vector3.zero;
        }
    }

    private void OnMouseDown()
    {
        OnSelected?.Invoke(this);
    }

    private void OnDisable()
    {
        isFree = true;
        OnDisabled?.Invoke(this);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        ToggleArrow(showArrow);  
        ToggleLine(showLine);
        SetColor(color);
        SetArrowType(arrowFromPointZero);
    }
#endif
}
