using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathPresentation.UI.Tabs
{ 
    public class TabGroup : MonoBehaviour
    {
        private TabWindow[] windows;

        private void Awake()
        {
            windows = GetComponentsInChildren<TabWindow>(true);
            foreach (var window in windows)
            {
                window.OnShow += OnTabWindowShown;
                window.OnHide += OnTabWindowHidden;
            }
        }

        private void OnTabWindowShown(TabWindow tabWindow)
        {
            foreach (var window in windows)
            {
                if (window == tabWindow) continue;
                window.HideCompletly();
            }
        }

        private void OnTabWindowHidden(TabWindow tabWindow)
        {
            foreach (var window in windows)
            {
                window.ShowButton();
            }
        }
    }
}