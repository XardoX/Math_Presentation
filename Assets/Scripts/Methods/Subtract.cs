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

        private MyVector vectorA, vectorB, vectorC;

        public void SetShowAsDirection(bool show)
        {
            showAsDirection = show;
            if (showAsDirection == false && vectorC != null)
                vectorC.Offset = Vector3.zero;
        }

        private void OnEnable()
        {
            vectorA = chart.GetFreeVector(Vector2.left);
            vectorB = chart.GetFreeVector(Vector2.right);
            vectorC = chart.GetFreeVector(false, true);
            vectorC.TogglePoint(false);
        }

        private void OnDisable()
        {
            vectorA.Toggle(false);
            vectorB.Toggle(false);
            vectorC.Toggle(false);
        }

        private void LateUpdate()
        {
            if(showAsDirection)
            {
                vectorC.Offset = vectorB.Value;
            }
            vectorC.Value = vectorA.Value - vectorB.Value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetShowAsDirection(showAsDirection);
        }
#endif
    }
}