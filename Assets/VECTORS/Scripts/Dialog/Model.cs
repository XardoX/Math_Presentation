using UnityEngine;
using MyBox;

namespace MathPresentation.DialogSystem
{
    public abstract class Model : ScriptableObject
    {
        [SerializeField]
        [ReadOnly]
        
        protected string id;

        public string ID => id;

        protected virtual void OnValidate()
        {
            id = name;
        }
    }
}