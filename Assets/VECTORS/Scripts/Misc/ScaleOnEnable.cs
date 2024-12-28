using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleOnEnable : MonoBehaviour
{
    [SerializeField]
    private Transform transformToScale;

    [SerializeField]
    private float duration = 0.2f;

    [SerializeField]
    private Ease ease;

    private void OnEnable()
    {
        if (transformToScale == null) transformToScale = transform;
        transformToScale.localScale = Vector3.zero;
        transformToScale.DOScale(Vector3.one, duration).SetEase(ease);
    }

    private void OnDisable()
    {
        transformToScale.localScale = Vector3.one;
    }
}
