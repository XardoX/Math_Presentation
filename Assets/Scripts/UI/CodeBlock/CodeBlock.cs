using Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

namespace MathPresentation
{
    public class CodeBlock : MonoBehaviour
    {
        [SerializeField]
        private SyntaxHighlightingData data;

        [SerializeField]
        private TextMeshProUGUI codeText;

        private string rawCode;

        private List<SyntaxHighlight> dynamicHighLights = new();

        private List<SyntaxHighlight> highlights = new();

        public void SetCodeText(string text)
        {
            rawCode = text;
            var charArray = text.ToCharArray();
            var coloredText = string.Empty;

            string nextWord = string.Empty;
            string coloredChar = string.Empty;

            highlights.Clear();
            highlights.AddRange(dynamicHighLights);
            highlights.AddRange(data.Highlights);

            foreach (var c in charArray)
            {
                bool found = false;

                if (char.IsWhiteSpace(c) && !char.IsLetter(c))
                {
                    coloredText += nextWord + c;
                    nextWord = string.Empty;
                    continue;
                }

                foreach(var highlight in highlights)
                {
                    coloredChar = string.Empty;
                    if (highlight.Chars == null) continue;

                    if (highlight.Chars.Any(_ => _ == c))
                    {
                        coloredChar = c.ToString().Color(highlight.Color);
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    coloredText += HighlightWord(nextWord) + coloredChar;
                    Debug.Log(nextWord);
                    nextWord = string.Empty;
                }else
                {
                    nextWord += c;
                }
            }

            codeText.text = coloredText;
        }

        public void AddDynamicHighLights(string character, Color color)
        {
            var stringArray = new string[] { character };
            dynamicHighLights.Add(new SyntaxHighlight(color, stringArray));
        }

        public void ClearDynamicHighlights()
        {
            dynamicHighLights.Clear();
        }

        private string HighlightWord(string word)
        {
            highlights.ForEach(_ =>
            {
                if(_.Strings.Any(s => s == word))
                {
                    word = word.Color(_.Color);
                }
            });
            return word;
        }
    }
}