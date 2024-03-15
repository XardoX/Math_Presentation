using Extensions;
using System;
using UnityEngine;
namespace MathPresentation
{
    public class MyVector : MonoBehaviour
    {
        public Action<MyVector> OnSelected,
            OnUnselected,
            OnDisabled;

        public Action OnUpdated;

        [Header("Settings")]
        [SerializeField]
        private string id;

        [SerializeField]
        private bool showPoint,
            showArrow,
            arrowFromPointZero,
            showArrowPoint,
            isArrowInverted,
            showLine,
            infiniteLine;

        [SerializeField]
        private Color color;

        [Header("References")]
        [SerializeField]
        private DragAndDrop dragAndDrop;

        [SerializeField]
        private SpriteRenderer arrow,
            line,
            point,
            arrowPoint;

        [SerializeField]
        private SpriteMask mask;

        [SerializeField]
        private Collider2D coll;

        [SerializeField]
        private Animator animator;

        private Quaternion rotation;

        private Vector3 offset;

        private bool isFree = true;

        public string Id => id;

        /// <summary>
        /// <b>id</b> with rich tags color
        /// </summary>
        public string Name => id.Color(color);

        public Vector3 Value
        {
            get => transform.position + offset;
            set
            {
                transform.position = value + offset;
                UpdateVector(true);
            }
        }

        public Vector3 Normalized => Value.normalized;

        public Quaternion Rotation => rotation;

        public float Length => Value.magnitude;

        public float SqrLength => Value.sqrMagnitude;

        public Color Color => color;

        public bool IsFree => isFree;

        public Vector3 Offset { get => offset; set => offset = value; }
        public bool ArrowFromPointZero => arrowFromPointZero;

        public bool ShowArrowPoint => showArrowPoint;

        public bool ShowPoint => showPoint;

        public void Init(Vector3 value, bool interactable = true, bool arrow = true, bool line = false)
        {
            transform.position = value;
            ToggleArrow(arrow);
            ToggleLine(line);
            dragAndDrop.IsDraggingEnabled = interactable;
            coll.enabled = interactable;

            ToggleArrowPoint(false);
            TogglePoint(true);
            InvertArrow(false);
            SetArrowType(false);
            SetLineType(false);

            UpdateVector();
        }

        public void Toggle(bool value)
        {
            isFree = !value;
            gameObject.SetActive(value);
        }

        public void SetId(string id) => this.id = id;

        public void TogglePoint(bool toggle)
        {
            showPoint = toggle;
            animator.SetBool("PointDisabled", !toggle);
        }

        public void ToggleArrowPoint(bool toggle)
        {
            showArrowPoint = toggle;
            arrowPoint.gameObject.SetActive(toggle);
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
            arrow.transform.localScale = new Vector3(Math.Abs(arrow.transform.localScale.x) * sign, arrow.transform.localScale.y, arrow.transform.localScale.z);
            arrow.size = new Vector2(Math.Abs(arrow.size.x) * sign, arrow.size.y);
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
            arrowPoint.color = color;
        }

        public void UpdateVector(bool withoutNotify = false)
        {
            rotation = Quaternion.FromToRotation(Vector3.right, transform.position);

            mask.transform.localScale = Vector3.one * Value.magnitude;

            if(showArrow)
                UpdateArrow();

            if (showLine)
                UpdateLine();

            if (showArrowPoint)
                arrowPoint.transform.rotation = rotation;

            if(withoutNotify == false)
                OnUpdated?.Invoke();
        }


        private void Awake()
        {
            dragAndDrop.OnBeingDragged +=() => UpdateVector();
        }

        private void Start()
        {
            SetColor(color);
        }

        private void UpdateArrow()
        {
            var arrowDir = isArrowInverted ? -1 : 1;
            arrow.size = new Vector2(Value.magnitude * arrowDir, arrow.size.y);
            arrow.transform.rotation = rotation;
            if (arrowFromPointZero)
            {
                arrow.transform.position = Vector3.zero;
            }
        }

        private void UpdateLine()
        {
            line.transform.rotation = rotation;
            var sizeOffset = showArrowPoint ? 0.5f : 0f;

            if (infiniteLine)
            {
                line.transform.position = Vector3.zero;
            }
            else
            {
                line.transform.position = Vector3.Lerp(Vector3.zero, Value, 0.5f);
                line.size = new Vector2(Value.magnitude - sizeOffset, line.size.y);
            }
        }

        private void OnMouseDown()
        {
            OnSelected?.Invoke(this);
        }

        private void OnMouseUp()
        {
            OnUnselected?.Invoke(this);
        }

        private void OnMouseEnter()
        {
            animator.SetBool("IsMouseOver", true);
        }

        private void OnMouseExit()
        {
            animator.SetBool("IsMouseOver", false);
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
            ToggleArrowPoint(showArrowPoint);
            ToggleLine(showLine);
            SetLineType(infiniteLine);
            SetColor(color);
        }
#endif
    }
}