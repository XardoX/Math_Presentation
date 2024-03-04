using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Method : MonoBehaviour
{
    [SerializeField]
    protected string title;

    [SerializeField]
    protected TextMeshProUGUI outputText;

    protected ChartController chart;

    public string Title => title;

    public void Init(ChartController chart)
    {
        this.chart = chart;
    }

    private void OnEnable()
    {
        
    }


}
