using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace MathPresentation
{
    using Methods;
    public class ChartController : MonoBehaviour
    {
        [SerializeField]
        private ChartUI view;

        [SerializeField]
        private VectorOverlay overlay;

        [SerializeField]
        private MyVector myVectorPrefab;

        [SerializeField]
        private Color[] vectorColors;

        private Method[] methods;

        private List<MyVector> myVectors = new();

        public MyVector GetFreeVector(Vector3 value, bool interactable = true, bool arrow = true, bool line = false)
        {
            var freeVector = myVectors.FirstOrDefault(_ => _.IsFree);

            if (freeVector == null)
            {
                freeVector = Instantiate(myVectorPrefab, this.transform);
                if(vectorColors.Length > myVectors.Count)
                {
                    freeVector.SetColor(vectorColors[myVectors.Count]);
                }
                else
                {
                    Color.RGBToHSV(vectorColors[0], out float H, out float S, out float V);
                    H = Random.Range(0.0f, 1.0f);
                    var randomColor = Color.HSVToRGB(H, S, V);
                    randomColor.a = vectorColors[0].a;
                    freeVector.SetColor(randomColor);
                }

                string id = Encoding.ASCII.GetString(new byte[] { (byte)(65 + myVectors.Count) });
                freeVector.SetId(id);
                freeVector.gameObject.name = "Vector " + id;
                myVectors.Add(freeVector);
            }
            overlay.AddVector(freeVector);

            freeVector.Init(value, interactable, arrow, line);
            freeVector.Toggle(true);
            return freeVector;
        }

        public MyVector GetFreeVector(bool interactable = true, bool arrow = true, bool line = false) =>
            GetFreeVector(Vector3.zero, interactable, arrow, line);


        private void Awake()
        {
            methods = FindObjectsOfType<Method>(true);

            foreach (var method in methods)
            {
                method.Init(this);
                method.OnEnabled += view.SetMethodText;
                method.OnDisabled += view.HideMethodText;
            }
        }
    }
}