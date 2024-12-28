using UnityEngine;
using System.Collections.Generic;

namespace MathPresentation.Extenstions
{
    ///<summary>Collection of useful methods</summary>
    public static class Helper
    {
        public static float RemapValue(this float value, float inMin, float inMax, float outMin, float outMax, bool clamp)
        {
            if (clamp)
            {
                if (value > inMax) value = inMax;
                if (value < inMin) value = inMin;
            }
            value = outMin + (value - inMin) * (outMax - outMin) / (inMax - inMin);
            return value;
        }
        public static float RemapValue(this float value, float inMin, float inMax, float outMin, float outMax)
        {
            return RemapValue(value, inMin, inMax, outMin, outMax, false);
        }

        public static float RemapValue(this float value, Vector2 InMinMax, Vector2 OutMinMax)
        {
            return RemapValue(value, InMinMax.x, InMinMax.y, InMinMax.x, InMinMax.y, false); ;
        }

        public static float RemapValue(this float value, Vector2 InMinMax, Vector2 OutMinMax, bool clamp)
        {
            return RemapValue(value, InMinMax.x, InMinMax.y, InMinMax.x, InMinMax.y, clamp); ;
        }
    
        public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
        {
            if (parent == null) { throw new System.ArgumentNullException(); }
            if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
            List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
            if (list.Count == 0) { return null; }

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].CompareTag(tag) == false)
                {
                    list.RemoveAt(i);
                }
            }
            return list.ToArray();
        }

        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
        {
            if (parent == null) { throw new System.ArgumentNullException(); }
            if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

            T[] list = parent.GetComponentsInChildren<T>(forceActive);
            for (int i = list.Length - 1; i >= 0; i--)
            {
                if (list[i].CompareTag(tag) == true)
                {
                    return list[i];
                }
            }
            return null;
        }
    }
}