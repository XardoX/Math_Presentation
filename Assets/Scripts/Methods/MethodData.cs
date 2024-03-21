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
        private string Id;

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

#if UNITY_EDITOR
        private void OnValidate()
        {
            var tags = name.Split("_", System.StringSplitOptions.None);
            if(tags.Length > 1)
            {
                Id = tags[0];
                type = Enum.Parse<MethodType>(tags[1]);
            }
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
