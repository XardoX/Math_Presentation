using UnityEngine;

public class Reflect : Method
{
    private MyVector inDirection, inNormal, result;

    private void OnEnable()
    {
        inDirection = chart.GetFreeVector(Vector3.left + Vector3.up, true, true, true);
        inDirection.InvertArrow(true);
        inNormal = chart.GetFreeVector(Vector3.right, true, false, false, true);
        inNormal.SetLineType(true);
        result = chart.GetFreeVector(false, true, true);
        result.TogglePoint(false);
    }

    private void LateUpdate()
    {
        result.transform.position = Vector3.Reflect(inDirection.Value, inNormal.Normalized);
    }
}
