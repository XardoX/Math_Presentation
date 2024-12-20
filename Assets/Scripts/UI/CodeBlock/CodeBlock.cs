using Extensions;
using MathPresentation.UI.Tabs;
using MathPresentation.UI.TooltipSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MathPresentation
{
    public class CodeBlock : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private SyntaxHighlightingData data;

        [SerializeField]
        private TextMeshProUGUI codeText;

        [SerializeField]
        private TabWindow tabWindow;

        [SerializeField]
        private TooltipTrigger tooltipTrigger;

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
                    coloredText += HighlightWord(nextWord) + c;
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
                    nextWord = string.Empty;
                }else
                {
                    nextWord += c;
                }
            }

            codeText.text = coloredText;
            Invoke(nameof(UpdateTabWindow), 0.1f);
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
            var coloredWord = word;
            highlights.Any(_ =>
            {
                if(_.Strings.Any(s => s == word))
                {
                    coloredWord = word.Color(_.Color);
                    return true;
                }
                return false;
            });
            return coloredWord;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            WebGLClipboard.CopyToWebGLClipboard(rawCode);
#else
            GUIUtility.systemCopyBuffer = rawCode;
#endif
            tooltipTrigger.OverrideText("Copied!");
            Debug.Log("Copied!");
        }

        private void UpdateTabWindow()
        {
            if(tabWindow.IsShown)
                tabWindow.Show();
        }
    }
}