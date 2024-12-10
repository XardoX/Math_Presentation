using Extensions;
using MathPresentation.LocalizationWrapper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Distance : Method
    {
        [SerializeField]
        private Line line;

        [SerializeField]
        private TextMeshProUGUI distanceText;

        private string distanceString;

        private void Start()
        {
            distanceText.transform.parent = chart.Overlay.transform;
            distanceText.transform.localScale = Vector3.one;
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(new Vector2(-3f,1f), true, false));
            vectors.Add(chart.GetFreeVector(new Vector2(3f, 2f), true, false));
        }

        protected override void UpdateMethod()
        {
            line.transform.position = Vector3.Lerp(A.Value, B.Value, 0.5f);
            var target = Quaternion.Euler(0, 0, 90) * (A.Value - B.Value);
            line.transform.rotation = Quaternion.LookRotation(Vector3.forward,target);

            var distance = Vector3.Distance(A.Value, B.Value);
            line.SetLength(distance);

            distanceText.text = distanceString + distance.ToString("0.00");
            var offset = 0.25f;
            if(A.Value.x < B.Value.x)
            {
                offset *= -1;
                distanceText.transform.rotation = line.transform.rotation *= Quaternion.Euler(0,0, 180f);
            }
            else
            {
                distanceText.transform.rotation = line.transform.rotation;
            }
            distanceText.transform.position = line.transform.position + line.transform.up * offset;
        }

        protected override void OnMethodEnable()
        {
            description = Data.DescriptionString.GetLocalizedString(new 
            {
                A = A.Name,
                B = B.Name 
            });
            distanceText.gameObject.SetActive(true);
            distanceString = Localization.GetVectors("DISTANCE_VALUE");
        }

        protected override void OnMethodDisable()
        {
            distanceText.gameObject.SetActive(false);
        }
    }
}