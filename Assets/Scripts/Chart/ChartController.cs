using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace MathPresentation
{
    using MathPresentation.Toolbox;
    using Methods;
    using UnityEngine.EventSystems;
    using UnityEngine.SceneManagement;

    public class ChartController : MonoBehaviour
    {
        [SerializeField]
        private MethodSwitcher methodSwitcher;

        [SerializeField]
        private ToolsController toolsController;

        [SerializeField]
        private ChartUI view;

        [SerializeField]
        private VectorOverlay overlay;

        [SerializeField]
        private GameObject mainChart,
            leftChart,
            rightChart;

        [SerializeField]
        private MyVector myVectorPrefab;

        [SerializeField]
        private Color[] vectorColors;

        private Method[] methods;

        private MyVector selectedVector;

        private List<MyVector> myVectors = new();

        public ChartUI View => view;

        public VectorOverlay Overlay => overlay;

        public MyVector GetFreeVector(Vector3 value, bool interactable = true, bool arrow = true, bool line = false)
        {
            var freeVector = myVectors.FirstOrDefault(_ => _.IsFree);

            if (freeVector == null)
            {
                freeVector = Instantiate(myVectorPrefab, mainChart.transform);
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

                freeVector.OnSelected += OnVectorSelected;
                freeVector.OnUnselected += OnVectorUnselected;
            }
            else
            {
                freeVector.transform.parent = mainChart.transform;
            }

            overlay.AddVector(freeVector);

            freeVector.Init(value, interactable, arrow, line);
            freeVector.Toggle(true);
            return freeVector;
        }

        public MyVector GetFreeVector(bool interactable = true, bool arrow = true, bool line = false) =>
            GetFreeVector(Vector3.zero, interactable, arrow, line);

        public void TransferVectorToChart(MyVector vector, int chartId)
        {
            var localPos = vector.transform.localPosition;
            _ = chartId switch
            {
                0 => vector.transform.parent = mainChart.transform,
                1 => vector.transform.parent = leftChart.transform,
                2 => vector.transform.parent = rightChart.transform,
                _ => vector.transform.parent = mainChart.transform
            };
            vector.transform.localPosition = localPos;
        }

        public void ToggleDoubleChart(bool toggle)
        {
            leftChart.SetActive(toggle);
            rightChart.SetActive(toggle);
            mainChart.SetActive(!toggle);
        }

        private void Awake()
        {
            methods = FindObjectsOfType<Method>(true);

            foreach (var method in methods)
            {
                method.Init(this);
                method.OnEnabled += view.SetMethodText;
                method.OnUpdated += view.SetMethodText;
                method.OnUpdated += (value) => overlay.UpdateVectors();
                method.OnDisabled += view.HideMethodText;
            }

            view.onNextClicked += methodSwitcher.ShowNextMethod;
            view.onPreviousClicked += methodSwitcher.ShowPreviousMethod;

            methodSwitcher.Init(view);

            methodSwitcher.OnSwitched += overlay.UpdateVectors;
        }

        private void Start()
        {
            methodSwitcher.ToggleMethod(0, true);
            view.TogglePreviousButton(true);
        }

        private void Update()
        {
            if(Input.GetMouseButtonUp(0))
            {
                EventSystem.current.SetSelectedGameObject(null);
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync("Menu");
            }
        }

        private void OnVectorSelected(MyVector vector)
        {
            selectedVector = vector;
            toolsController.ToggleCurrentTool(false);
        }

        private void OnVectorUnselected(MyVector vector)
        {
            selectedVector = null;
            toolsController.ToggleCurrentTool(true);
        }
    }
}