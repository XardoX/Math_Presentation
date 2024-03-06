using System;
using UnityEngine;
namespace MathPresentation
{
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
        private bool isArrowInverted;

        [SerializeField]
        private bool showLine;

        [SerializeField]
        private bool infiniteLine;

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

        private Vector3 offset;

        private bool isFree = true;

        public string Id => id;

        public Vector3 Value
        {
            get => transform.position + offset;
            set => transform.position = value + offset;
        }

        public Vector3 Normalized => Value.normalized;

        public Quaternion Rotation => rotation;

        public float Length => Value.magnitude;

        public float SqrLength => Value.sqrMagnitude;

        public Color Color => color;

        public bool IsFree => isFree;

        public Vector3 Offset { get => offset; set => offset = value; }

        public void Init(Vector3 value, bool interactable = true, bool arrow = true, bool line = false)
        {
            transform.position = value;
            ToggleArrow(arrow);
            ToggleLine(line);

            dragAndDrop.IsDraggingEnabled = interactable;
        }

        public void Toggle(bool value)
        {
            isFree = !value;
            gameObject.SetActive(value);
        }

        public void SetId(string id) => this.id = id;

        public void TogglePoint(bool toggle)
        {
            point.gameObject.SetActive(toggle);
        }

        public void ToggleArrow(bool toggle)
        {
            arrow.gameObject.SetActive(toggle);
            showArrow = toggle;
        }

        public void SetArrowType(bool fromPointZero)
        {
            arrowFromPointZero = fromPointZero;
            if (fromPointZero)
            {
                arrow.transform.position = Vector3.zero;
            }
            else
            {
                arrow.transform.position = Value;
            }
        }

        public void InvertArrow(bool inverted)
        {
            isArrowInverted = inverted;
            var sign = inverted ? -1 : 1;
            arrow.transform.localScale = new Vector3(arrow.transform.localScale.x * sign, arrow.transform.localScale.y, arrow.transform.localScale.z);
            arrow.size = new Vector2(arrow.size.x * sign, arrow.size.y);
        }

        public void ToggleLine(bool toggle)
        {
            showLine = toggle;
            line.gameObject.SetActive(toggle);
        }

        public void SetLineType(bool isInfinite)
        {
            infiniteLine = isInfinite;
            line.transform.position = Vector3.zero;
            line.size = new Vector2(100f, line.size.y); //todo size based on screen/camera view
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
            var arrowDir = isArrowInverted ? -1 : 1;
            arrow.size = new Vector2(Value.magnitude * arrowDir, arrow.size.y);

            rotation = Quaternion.FromToRotation(Vector3.right, transform.position);

            arrow.transform.rotation = rotation;

            mask.transform.localScale = Vector3.one * Value.magnitude;

            if (arrowFromPointZero)
            {
                arrow.transform.position = Vector3.zero;
            }

            if (showLine)
                HandleLine();
        }

        private void HandleLine()
        {
            line.transform.rotation = rotation;
            if (infiniteLine)
            {
                line.transform.position = Vector3.zero;
            }
            else
            {
                line.transform.position = Vector3.Lerp(Vector3.zero, Value, 0.5f);
                line.size = new Vector2(Value.magnitude, line.size.y);
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
            InvertArrow(isArrowInverted);
            SetArrowType(arrowFromPointZero);
            ToggleLine(showLine);
            SetLineType(infiniteLine);
            SetColor(color);
        }
#endif
    }
}