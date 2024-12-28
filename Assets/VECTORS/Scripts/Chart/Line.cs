using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation
{
    public class Line : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer lineSprite;

        public void SetLength(float length)
        {
            lineSprite.size = new Vector2(length, lineSprite.size.y);
        }

        public void SetColor(Color color)
        {
            lineSprite.color = color;
        }
    }
}