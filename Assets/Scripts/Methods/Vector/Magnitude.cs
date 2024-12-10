using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MathPresentation.LocalizationWrapper;
namespace MathPresentation.Methods
{
    public class Magnitude : Method
    {
        [SerializeField]
        private TextMeshProUGUI magnitudeText;

        private string magnitudeString;

        private void Start()
        {
            magnitudeText.transform.parent = chart.Overlay.transform;
            magnitudeText.transform.localScale = Vector3.one;
        }

        protected override void OnMethodEnable()
        {
            description = Data.DescriptionString.GetLocalizedString();
            magnitudeText.gameObject.SetActive(true);
            magnitudeString = Localization.GetVectors("MAGNITUDE_VALUE");
        }

        protected override void OnMethodDisable()
        {
            magnitudeText.gameObject.SetActive(false);
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.one * 2, true, true));
        }

        protected override void UpdateMethod()
        {
            var magnitude = A.Length;

            magnitudeText.text = magnitudeString + magnitude.ToString("0.00");
            var offset = 0.25f;
            if (A.Value.x < 0f)
            {
                offset *= -1;
                magnitudeText.transform.rotation = A.Rotation * Quaternion.Euler(0, 0, 180f);
            }
            else
            {
                magnitudeText.transform.rotation = A.Rotation;
            }
            magnitudeText.transform.position = Vector3.Lerp(A.Value, A.Value * 2, 0.5f) + A.transform.up * offset;
        }
    }
}