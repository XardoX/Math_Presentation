using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace MathPresentation.Methods
{
    public class Subtract : Method
    {
        [SerializeField]
        private bool showAsDirection;

        public void SetShowAsDirection(bool show)
        {
            showAsDirection = show;
            if (showAsDirection == false && vectors.Count > 1)
                vectors[2].Offset = Vector3.zero;
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left));
            vectors.Add(chart.GetFreeVector(Vector2.right));
            vectors.Add(chart.GetFreeVector(false, true));
            vectors[2].TogglePoint(false);
        }

        private void LateUpdate()
        {
            if(showAsDirection)
            {
                vectors[2].Offset = vectors[1].Value;
            }
            vectors[2].Value = vectors[0].Value - vectors[1].Value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetShowAsDirection(showAsDirection);
        }
#endif
    }
}