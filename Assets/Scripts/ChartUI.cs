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
        private TextMeshProUGUI methodTitleText,
            methodDescriptionText;

        public void SetMethodText(Method method)
        {
            methodTitleText.text = method.Title;
            methodDescriptionText.text = method.Description;
        }
    }
}