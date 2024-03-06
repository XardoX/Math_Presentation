using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
namespace MathPresentation
{
    public class VectorInfo : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup canvasGroup;

        [SerializeField]
        private TextMeshProUGUI valueText, normalizedValueText, magnitudeText;

        public void Set(Vector3 vector)
        {
            valueText.text = vector.ToString();
            normalizedValueText.text = vector.normalized.ToString();
            magnitudeText.text = vector.magnitude.ToString("0.00");
        }

        public void Set(Vector2 vector)
        {
            valueText.text = vector.ToString();
            normalizedValueText.text = vector.normalized.ToString();
            magnitudeText.text = vector.magnitude.ToString("0.00");
        }

        public void ToggleVisibility(bool toggle)
        {
            canvasGroup.alpha = toggle ? 1 : 0;
        }
    }
}