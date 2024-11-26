using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Ami.BroAudio;
namespace MathPresentation.UI.Tabs
{
    public class TabButton : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private SoundID onClickSFX;

        private RectTransform rectTransform;

        public UnityEvent OnClick => button.onClick;

        public RectTransform Rect => rectTransform;

        private void Awake()
        {
            if(button == null)
            {
                button = GetComponent<Button>();
            }

            rectTransform = GetComponent<RectTransform>();
            button.onClick.AddListener(OnClicked);
        }

        private void OnClicked()
        {
            BroAudio.Play(onClickSFX);
        }
    }
}