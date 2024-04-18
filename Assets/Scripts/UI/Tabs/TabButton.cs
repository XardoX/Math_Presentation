using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace MathPresentation.UI.Tabs
{
    public class TabButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        private RectTransform rectTransform;

        public UnityEvent OnClick => button.onClick;

        public RectTransform Rect => rectTransform;

        private void Awake()
        {
            if(button == null)
            {
                button = GetComponent<Button>();
            }

            rectTransform = GetComponent<RectTransform>();
        }
    }
}