using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Methods
{
    public class Normalize : Method
    {
        protected override void OnMethodEnable()
        {
            description = Data.DescriptionString.GetLocalizedString(new
            {
                A = A.Name,
                B = B.Name
            });
        }

        protected override void SetVectors()
        {
            vectors.Add(chart.GetFreeVector(Vector2.one * 2, true, true));
            A.SetArrowType(true);

            vectors.Add(chart.GetFreeVector(false, true));
            B.SetArrowType(true);
            B.TogglePoint(false);

            chart.TransferVectorToChart(A, 1);
            chart.TransferVectorToChart(B, 2);
        }

        protected override void UpdateMethod()
        {
            B.Value = A.Normalized;
        }
    }
}
