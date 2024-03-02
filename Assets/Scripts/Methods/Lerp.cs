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
    private SpriteRenderer angleCircle;

    [SerializeField]
    private bool unclampedLerp;

    [SerializeField]
    private bool isSlerp;

    [SerializeField]
    private bool fixSlerpCenter;

    private float t = 0.5f;

    private void Start()
    {
        slider.onValueChanged.AddListener(SetLerpValue);
        outputText.text = t.ToString("0.00");
    }

    private void Update()
    {
        if (isSlerp)
            HandleSlerp();
        else
            HandleLerp();
        
    }

    private void HandleLerp()
    {
        if (unclampedLerp)
        {
            vectorC.transform.position = Vector2.LerpUnclamped(vectorA.Value, vectorB.Value, t);

        }
        else
        {
            vectorC.transform.position = Vector2.Lerp(vectorA.Value, vectorB.Value, t);
        }
    }

    private void HandleSlerp()
    {
        Vector3 center = Vector3.zero;
        if (fixSlerpCenter)
        {
            center = (vectorA.Value + vectorB.Value) * 0.5F;
            //center -= new Vector3(0, 1, 0);
        }

        angleCircle.transform.position = center;

        var distance = Vector3.Distance(vectorA.Value, vectorB.Value);

        angleCircle.transform.localScale = new Vector3(distance, distance, angleCircle.transform.localScale.z);

        Vector3 relCenterA = vectorA.Value - center;
        Vector3 relCenterB = vectorB.Value - center;

        if (unclampedLerp)
        {
            vectorC.transform.position = Vector3.SlerpUnclamped(relCenterA, relCenterB, t);
        }
        else
        {
            vectorC.transform.position = Vector3.Slerp(relCenterA, relCenterB, t);
        }

        vectorC.transform.position += center;
    }

    private void SetLerpValue(float t)
    {
        this.t = t; 
        outputText.text = t.ToString("0.00");
    }
}
