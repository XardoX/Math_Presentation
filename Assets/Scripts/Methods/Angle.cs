using TMPro;
using UnityEngine;
namespace MathPresentation.Methods
{
    public class Angle : Method
    {
        [SerializeField]
        private SpriteRenderer angleCircle;

        [SerializeField]
        private TextMeshProUGUI angleText;

        [SerializeField]
        private float angleTextOffset = 0.5f;

        private MaterialPropertyBlock materialPropertyBlock;

        protected override void SetVectors() 
        { 
            vectors.Add(chart.GetFreeVector(Vector2.left, true, true, true));
            vectors.Add(chart.GetFreeVector(Vector2.right, true, true, true));
        }

        protected override void OnMethodEnable()
        {
            angleCircle.gameObject.SetActive(true);
            angleText.gameObject.SetActive(true);
        }

        protected override void OnMethodDisable()
        {
            angleCircle.gameObject.SetActive(false);
            angleText.gameObject.SetActive(false);
        }

        private void Start()
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            angleCircle.GetPropertyBlock(materialPropertyBlock);
        }

        private void Update()
        {
            var angle = Vector3.Angle(vectors[0].Value, vectors[1].Value);
            angle = Mathf.Round(angle);
            description = "Angle: "+angle.ToString();


            var dir = (vectors[1].Normalized + vectors[0].Normalized).normalized;

            if (angle == 180) dir = Vector2.Perpendicular(vectors[0].Value).normalized;

            float angleDir = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.AngleAxis(angleDir, Vector3.forward);

            angleCircle.transform.rotation = rot;

            materialPropertyBlock.SetFloat("_FillAmount", angle / 360);
            angleCircle.SetPropertyBlock(materialPropertyBlock);

            angleText.rectTransform.position = dir * angleTextOffset;
            angleText.text = angle.ToString() + "°";
        }
    }
}