using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.Toolbox
{
    public class Tool : MonoBehaviour
    {
        protected bool isEnabled;

        public void Toggle(bool toggle)
        {
            isEnabled = toggle;
            if(toggle)
                OnEnabled();
            else 
                OnDisabled();
        }

        public virtual void OnToolClickedDown()
        {

        }

        public virtual void OnToolClickedUp()
        {

        }

        protected virtual void OnEnabled()
        {

        }

        protected virtual void OnDisabled()
        {

        }
    }
}