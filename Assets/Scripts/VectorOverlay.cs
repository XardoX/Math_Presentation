using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VectorOverlay : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float offset;

    [Header("References")]
    [SerializeField]
    private TextMeshProUGUI vectorValuesTextPrefab;

    [SerializeField]
    private TextMeshProUGUI vectorIdTextPrefab;

    [SerializeField]
    private VectorInfo vectorInfo;

    private List<MyVector> vectors = new();

    private List<VectorOverlayInfo> vectorsOverlaysInfo = new();

    private MyVector selectedVector;

    public void AddVector(MyVector vector)
    {
        vectors.Add(vector);
        var newValueText = Instantiate(vectorValuesTextPrefab, transform);
        var newIdText = Instantiate(vectorIdTextPrefab, transform);
        newIdText.text = vector.Id;

        vectorsOverlaysInfo.Add(new VectorOverlayInfo(newIdText,newValueText));
        vector.OnSelected += SetSelectedVector;
        vector.OnDisabled += RemoveVectorOveralay;
    }

    public void RemoveVectorOveralay(MyVector vector)
    {
        var id = vectors.FindIndex(_ => vector);
        vectorsOverlaysInfo[id].Clear();
        vectorsOverlaysInfo[id] = null;
        vectorsOverlaysInfo.RemoveAt(id);
    }

    private void LateUpdate()
    {
        for (int i = 0; i < vectors.Count; i++)
        {
            var direction = vectors[i].transform.position.normalized;

            var infoPos = direction;
            infoPos.x += 1 * Mathf.Sign(direction.x);
            infoPos.y += 1 * Mathf.Sign(direction.y);

            vectorsOverlaysInfo[i].ValueText.rectTransform.position = vectors[i].transform.position + infoPos * offset;

            var x = vectors[i].transform.position.x.ToString("0.0");
            var y = vectors[i].transform.position.y.ToString("0.0");
            vectorsOverlaysInfo[i].ValueText.text = $"(x={x}, y={y})";

            vectorsOverlaysInfo[i].Id.rectTransform.position = vectors[i].transform.position;
        }

        if(selectedVector != null)
        {
            vectorInfo.Set((Vector2)selectedVector.transform.position);
        }
    }

    private void SetSelectedVector(MyVector v)
    {
        selectedVector = v;
        vectorInfo.ToggleVisibility(true);
        vectorInfo.Set((Vector2)selectedVector.transform.position);
    }

    public class VectorOverlayInfo
    {
        private TextMeshProUGUI id;

        private TextMeshProUGUI valueText;

        public TextMeshProUGUI ValueText => valueText;

        public TextMeshProUGUI Id => id;

        public VectorOverlayInfo(TextMeshProUGUI id, TextMeshProUGUI valueText)
        {
            this.id = id;
            this.valueText = valueText;
        }

        public void Clear()
        {
            Destroy(id.gameObject);
            Destroy(valueText.gameObject);
        }
    }
}
