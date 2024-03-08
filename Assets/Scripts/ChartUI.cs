using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MathPresentation
{
    using Methods;
    public class ChartUI : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup methodInfo;

        [SerializeField]
        private TextMeshProUGUI methodTitleText,
            methodDescriptionText;

        public void SetMethodText(Method method)
        {
            methodInfo.alpha = 1f;
            methodTitleText.text = method.Title;
            methodDescriptionText.text = method.Description;
        }

        public void HideMethodText(Method method)
        {
            methodInfo.alpha = 0f;

        }
    }
}