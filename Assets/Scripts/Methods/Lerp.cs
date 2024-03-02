using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lerp : Method
{
    [SerializeField]
    private MyVector vectorA, vectorB, vectorC;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private bool unclampedLerp;

    [SerializeField]
    private bool isSlerp;

    private float t = 0.5f;

    private void Start()
    {
        slider.onValueChanged.AddListener(SetLerpValue);
        outputText.text = t.ToString("0.00");
    }
    private void Update()
    {
        if(isSlerp)
        {
            if (unclampedLerp)
            {
                vectorC.transform.position = Vector3.SlerpUnclamped(vectorA.Value, vectorB.Value, t);

            }
            else
            {
                vectorC.transform.position = Vector3.Slerp(vectorA.Value, vectorB.Value, t);
            }
        }
        else if(unclampedLerp)
        {
            vectorC.transform.position = Vector2.LerpUnclamped(vectorA.Value, vectorB.Value, t);

        }
        else
        {
            vectorC.transform.position = Vector2.Lerp(vectorA.Value, vectorB.Value, t);
        }
    }

    private void SetLerpValue(float t)
    {
        this.t = t; 
        outputText.text = t.ToString("0.00");
    }
}
