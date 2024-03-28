using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace MathPresentation
{
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
            newValueText.gameObject.name = vector.Id + "_ValueText";
            var newIdText = Instantiate(vectorIdTextPrefab, transform);
            newIdText.gameObject.name = vector.Id + "_IdText";

            newIdText.text = vector.Id;

            vectorsOverlaysInfo.Add(new VectorOverlayInfo(newIdText, newValueText));
            vector.OnSelected += SetSelectedVector;
            vector.OnUpdated += UpdateVectors;
            vector.OnDisabled += RemoveVectorOverlay;
            Debug.Log("co jest");
        }

        public void RemoveVectorOverlay(MyVector vector)
        {
            var id = vectors.FindIndex(_ => vector);
            if (id < 0) return;

            if (vectorsOverlaysInfo.Count > id)
            {
                vectorsOverlaysInfo[id].Clear();
                vectorsOverlaysInfo[id] = null;
                vectorsOverlaysInfo.RemoveAt(id);
            }

            vector.OnSelected -= SetSelectedVector;
            vector.OnUpdated -= UpdateVectors;
            vector.OnDisabled -= RemoveVectorOverlay;

            vectors.RemoveAt(id);
            Debug.Log("kurwa");
        }

        public void UpdateVectors()
        {
            for (int i = 0; i < vectors.Count; i++)
            {
                var direction = vectors[i].Normalized;
                var infoPos = direction;
                var sign = vectors[i].ArrowFromPointZero ? 1 : -1;

                infoPos.x += 1 * Mathf.Sign(direction.x);
                infoPos.y += 1 * Mathf.Sign(direction.y);

                UpdateVectorOverlayInfo(i, infoPos, sign);

                UpdateId(i, infoPos, sign);
            }

            if (selectedVector != null)
            {
                vectorInfo.Set((Vector2)selectedVector.Value);
            }
        }

        private void UpdateVectorOverlayInfo(int i, Vector3 infoPos, int sign)
        {
            vectorsOverlaysInfo[i].ValueText.rectTransform.position = vectors[i].transform.position + infoPos * offset * sign;

            var x = vectors[i].Value.x.ToString("0.0");
            var y = vectors[i].Value.y.ToString("0.0");
            vectorsOverlaysInfo[i].ValueText.text = $"({x}, {y})";
        }

        private void UpdateId(int i, Vector3 infoPos, int sign)
        {
            sign = vectors[i].ShowArrowPoint ? sign * -1 : sign;
            var idOffset = vectors[i].ShowPoint ? Vector3.zero : infoPos * offset * sign * .4f;
            vectorsOverlaysInfo[i].Id.color = vectors[i].ShowPoint ? Color.black : vectors[i].Color;
            vectorsOverlaysInfo[i].Id.rectTransform.position = vectors[i].transform.position + idOffset;
        }

        private void SetSelectedVector(MyVector v)
        {
            selectedVector = v;
            vectorInfo.ToggleVisibility(true);
            vectorInfo.Set((Vector2)selectedVector.Value);
        }

        [System.Serializable]
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
}