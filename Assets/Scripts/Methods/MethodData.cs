using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MathPresentation.Methods
{
    [CreateAssetMenu(menuName = "Data/Method", fileName = "Method", order = 0)]
    public class MethodData : ScriptableObject
    {
        [SerializeField]
        private MethodType type;

        [SerializeField]
        private string id;

        [SerializeField]
        private string title;

        [SerializeField]
        [TextArea(3, 10)]
        private string mathBehindIt;

        [SerializeField]
        [TextArea(3, 10)]
        private string whatIsUsedFor;

        [SerializeField]
        [TextArea(3,10)]
        private string code;

        [SerializeField]
        private MethodData[] similarMethods;

        public MethodType Type => type;
        public string Id => id; 
        public string Title => title;
        public string MathBehindIt => mathBehindIt;
        public string WhatIsUsedFor => whatIsUsedFor;
        public string Code => code;
        public MethodData[] SimilarMethods => similarMethods;

        private void OnEnable()
        {
            SetName();
        }

        private void SetName()
        {

            var tags = name.Split("_", System.StringSplitOptions.None);
            if(tags.Length > 1)
            {
                id = tags[0];
                type = Enum.Parse<MethodType>(tags[1]);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            SetName();
        }
#endif
    }

    public enum MethodType
    {
        Vector2,
        Vector3,
        Quternion,
        Mathf
    }
}
