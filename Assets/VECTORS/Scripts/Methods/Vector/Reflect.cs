using UnityEngine;
namespace MathPresentation.Methods
{
    public class Reflect : Method
    {
        private MyVector inDirection, inNormal, result;

        protected override void OnMethodEnable()
        {
            description = Data.DescriptionString.GetLocalizedString(new
            {
                A = A.Name,
                B = B.Name,
                C = C.Name
            });
        }

        protected override void SetVectors()
        {
            inDirection = chart.GetFreeVector(Vector3.left + Vector3.up, true, true);
            inDirection.InvertArrow(true);
            inDirection.SetArrowType(true);

            inNormal = chart.GetFreeVector(Vector3.right, true, false, true);
            inNormal.SetLineType(true);

            result = chart.GetFreeVector(false, true);
            result.SetArrowType(true);
            result.TogglePoint(false);

            vectors.Add(inDirection);
            vectors.Add(inNormal);
            vectors.Add(result);
        }

        protected override void UpdateMethod()
        {
            result.Value = Vector3.Reflect(inDirection.Value, inNormal.Normalized);
        }
    }
}