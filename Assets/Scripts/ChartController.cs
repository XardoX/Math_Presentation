using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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

    public MyVector GetFreeVector(Vector3 value, bool interactable = true, bool arrow = true, bool arrowType = false, bool line = false)
    {
        var freeVector = myVectors.FirstOrDefault(_ => _.IsFree);

        if(freeVector == null )
        {
            freeVector = Instantiate(myVectorPrefab, this.transform);
            freeVector.SetColor(vectorColors[myVectors.Count]);
            string id = Encoding.ASCII.GetString(new byte[] { (byte)(65 + myVectors.Count) });
            freeVector.SetId(id);
            freeVector.gameObject.name = "Vector " + id;
            myVectors.Add(freeVector);
            overlay.AddVector(freeVector);
        }

        freeVector.Init(value, interactable, arrow, arrowType, line);
        freeVector.Toggle(true);
        return freeVector;
    }

    public MyVector GetFreeVector(bool interactable = true, bool arrow = true, bool arrowType = false, bool line = false) =>
        GetFreeVector(Vector3.zero, interactable, arrow, arrowType, line);
    

    private void Awake()
    {
        methods = FindObjectsOfType<Method>(true);

        foreach (var method in methods)
        {
            method.Init(this);
        }
    }
}
