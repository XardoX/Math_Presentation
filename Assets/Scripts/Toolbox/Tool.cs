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
        }

        public virtual void OnToolClickedDown()
        {

        }

        public virtual void OnToolClickedUp()
        {

        }
    }
}