using Extensions;
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

        private void Start()
        {
            distanceText.gameObject.SetActive(false);
            distanceText.transform.parent = chart.Overlay.transform;
            distanceText.transform.localScale = Vector3.one;
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left + Vector2.up));
            vectors.Add(chart.GetFreeVector(Vector2.right * 3));
        }

        protected override void UpdateMethod()
        {
            line.transform.position = Vector3.Lerp(A.Value, B.Value, 0.5f);
            var target = Quaternion.Euler(0, 0, 90) * (A.Value - B.Value);
            line.transform.rotation = Quaternion.LookRotation(Vector3.forward,target);

            var distance = Vector3.Distance(A.Value, B.Value);
            line.SetLength(distance);

            distanceText.text = "Distance: " + distance.ToString("0.00");
            var offset = 0.25f;
            if(A.Value.x > B.Value.x)
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
            description = $"Calculates distance between {A.Name} and {B.Name}";
            distanceText.gameObject.SetActive(true);
        }

        protected override void OnMethodDisable()
        {
            distanceText.gameObject.SetActive(false);
        }
    }
}