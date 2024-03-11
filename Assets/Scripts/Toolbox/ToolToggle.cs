using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MathPresentation.Toolbox
{
    public class ToolToggle : MonoBehaviour
    {
        [SerializeField]
        private Toggle toggle;

        public bool IsOn => toggle.isOn;

        public UnityEvent<bool> OnValueChanged => toggle.onValueChanged;
    }
}