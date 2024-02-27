using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VectorOverlay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI vectorValuesTextPrefab;

    [SerializeField]
    private Vector2 offset;

    private MyVector[] vectors;

    private List<TextMeshProUGUI> vectorValues = new();

    private void Start()
    {
        vectors = FindObjectsOfType<MyVector>();

        CreateVectorOverlay();
    }

    public void CreateVectorOverlay()
    {
        foreach (var v in vectors)
        {
            var newValueText = Instantiate(vectorValuesTextPrefab, transform);
            vectorValues.Add(newValueText);
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < vectors.Length; i++)
        {
            vectorValues[i].rectTransform.position = vectors[i].transform.position + (Vector3)offset;
            vectorValues[i].text = $"({vectors[i].transform.position.x}, {vectors[i].transform.position.y})";
        }
    }
}
