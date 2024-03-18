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
        }

        protected override void OnMethodEnable()
        {
            description = $"Subtracts {B.Name} from {A.Name} which results in {C.Name}";
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.left));
            vectors.Add(chart.GetFreeVector(Vector2.down * 2));
            vectors.Add(chart.GetFreeVector(false, true));
            C.TogglePoint(false);
        }

        protected override void UpdateMethod()
        {
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