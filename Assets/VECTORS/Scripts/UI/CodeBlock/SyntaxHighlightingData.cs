using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation
{
    [CreateAssetMenu(menuName = "Data/SyntaxHighlightingData", fileName = "SyntaxHighlightingData", order = 1)]
    public class SyntaxHighlightingData : ScriptableObject
    {
        [SerializeField]
        private SyntaxHighlight[] highlights;

        public SyntaxHighlight[] Highlights => highlights;

    }

    [System.Serializable]
    public class SyntaxHighlight
    {
        [SerializeField]
        private Color color;

        [SerializeField]
        private char[] chars;

        [SerializeField]
        private string[] strings;

        public Color Color => color;

        public char[] Chars => chars;

        public string[] Strings => strings;

        public SyntaxHighlight(Color color, char[] chars, string[] strings)
        {
            this.color = color;
            this.chars = chars;
            this.strings = strings;
        }

        public SyntaxHighlight(Color color, char[] chars)
        {
            this.color = color;
            this.chars = chars;;
        }

        public SyntaxHighlight(Color color, string[] strings)
        {
            this.color = color;
            this.strings = strings;
        }
    }
}
