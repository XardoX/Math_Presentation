using TMPro;
using UnityEngine;

public class Angle : Method
{
    [SerializeField]
    private SpriteRenderer angleCircle;

    [SerializeField]
    private TextMeshProUGUI angleText;

    [SerializeField]
    private float angleTextOffset = 0.5f;

    private MyVector vectorA, vectorB;

    private MaterialPropertyBlock materialPropertyBlock;

    private void OnEnable()
    {
        vectorA = chart.GetFreeVector(Vector2.left, true, true, false, true);
        vectorB = chart.GetFreeVector(Vector2.right, true, true, false, true);

        angleCircle.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        vectorA.Toggle(false);
        vectorB.Toggle(false);
        angleCircle.gameObject.SetActive(false);
    }

    private void Start()
    {
        materialPropertyBlock = new MaterialPropertyBlock();
        angleCircle.GetPropertyBlock(materialPropertyBlock);
    }

    private void Update()
    {
        var angle = Vector3.Angle(vectorA.Value, vectorB.Value);
        angle = Mathf.Round(angle);
        outputText.text = angle.ToString();


        var dir = (vectorB.Normalized + vectorA.Normalized).normalized;

        if (angle == 180) dir = Vector2.Perpendicular(vectorA.Value).normalized;

        float angleDir = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var rot = Quaternion.AngleAxis(angleDir, Vector3.forward);

        angleCircle.transform.rotation = rot;

        materialPropertyBlock.SetFloat("_FillAmount", angle / 360);
        angleCircle.SetPropertyBlock(materialPropertyBlock);

        angleText.rectTransform.position = dir * angleTextOffset;
        angleText.text = angle.ToString() + "°";
    }
}
