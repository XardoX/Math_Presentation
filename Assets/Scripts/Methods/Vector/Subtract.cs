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
                C.Offset = Vector3.zero;
        }

        protected override void OnMethodEnable()
        {
            description = $"Subtracts {B.Name} from {A.Name} which results in {C.Name}";
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left));
            vectors.Add(chart.GetFreeVector(Vector2.right));
            vectors.Add(chart.GetFreeVector(false, true));
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
            if(showAsDirection)
            {
                C.Offset = B.Value;
            }
            C.Value = A.Value - B.Value;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetShowAsDirection(showAsDirection);
        }
#endif
    }
}