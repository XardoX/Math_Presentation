using Extensions;
using MathPresentation.LocalizationWrapper;
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
            A.SetArrowType(true);

            vectors.Add(chart.GetFreeVector(Vector2.right * 2, true, true));
            B.SetArrowType(true);

            angleCircle.transform.position = A.Offset;
        }

        protected override void OnMethodEnable()
        {
            angleCircle.gameObject.SetActive(true);
            angleText.gameObject.SetActive(true);
            var angleKeyword = Localization.GetVectors("KEYWORD_ANGLE");
            description = Data.DescriptionString.GetLocalizedString(new 
            { 
                angle = angleKeyword.Color(angleColor), 
                A = A.Name, 
                B = B.Name
            });
        }

        protected override void OnMethodDisable()
        {
            angleCircle.gameObject.SetActive(false);
            angleText.gameObject.SetActive(false);
        }
        protected override void UpdateMethod()
        {
            var angle = Vector3.Angle(A.Value, B.Value);
            angle = Mathf.Round(angle);

            var dir = (B.Normalized + A.Normalized).normalized;

            if (angle == 180) dir = Vector2.Perpendicular(A.Value).normalized;

            float angleDir = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var rot = Quaternion.AngleAxis(angleDir, Vector3.forward);

            angleCircle.transform.rotation = rot;

            materialPropertyBlock.SetFloat("_FillAmount", angle / 360);
            angleCircle.SetPropertyBlock(materialPropertyBlock);


            angleText.rectTransform.position = dir * angleTextOffset + A.Offset;
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