using Extensions;
using TMPro;
using UnityEngine;
namespace MathPresentation.Methods
{
    public class Angle : Method
    {
        [SerializeField]
        private Color angleColor = Color.yellow;

        [SerializeField]
        private SpriteRenderer angleCircle;

        [SerializeField]
        private TextMeshProUGUI angleText;

        [SerializeField]
        private float angleTextOffset = 0.5f;

        private MaterialPropertyBlock materialPropertyBlock;

        protected override void SetVectors() 
        { 
            vectors.Add(chart.GetFreeVector(Vector2.up * 2, true, true));
            vectors[0].SetArrowType(true);

            vectors.Add(chart.GetFreeVector(Vector2.right * 2, true, true));
            vectors[1].SetArrowType(true);
        }

        protected override void OnMethodEnable()
        {
            angleCircle.gameObject.SetActive(true);
            angleText.gameObject.SetActive(true);

            description = $"Calculates the {"angle".Color(angleColor)} between vectors {vectors[0].Id.Color(vectors[0].Color)} and {vectors[1].Id.Color(vectors[1].Color)}";
        }

        protected override void OnMethodDisable()
        {
            angleCircle.gameObject.SetActive(false);
            angleText.gameObject.SetActive(false);
        }
        protected override void UpdateMethod()
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

        private void Awake()
        {
            materialPropertyBlock = new MaterialPropertyBlock();
            angleCircle.GetPropertyBlock(materialPropertyBlock);

            angleText.color = angleColor;
            angleCircle.color = angleColor;
        }

    }
}