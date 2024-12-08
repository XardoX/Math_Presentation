using MyBox;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MathPresentation.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        [ReadOnly]
        [Foldout("Debug", true)]
        private List<Window> windows = new();

        [SerializeField]
        [ReadOnly]
        private List<Window> openedWindows = new();

        public void GoBack()
        {
            if (openedWindows.Count <= 1) return;
            openedWindows.Last().Hide();
            openedWindows.Last().Show();
        }

        private void Awake()
        {
            windows = GetComponentsInChildren<Window>(true).ToList();
            windows.ForEach(_ => { 
                _.OnShow += OnWindowShown; 
                _.OnHide += OnWindowHidden;
                if(_.gameObject.activeInHierarchy)
                {
                    openedWindows.Add(_);
                }
            });
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GoBack();
            }
        }

        private void OnWindowShown(Window window)
        {
            HideAllOtherWindows(window);
            if(!openedWindows.Contains(window))
                openedWindows.Add(window);
        }

        private void OnWindowHidden(Window window)
        {
            if(openedWindows.Last() == window)
            {
                openedWindows.Remove(window);
            }
        }

        private void HideAllOtherWindows(Window window)
        {
            foreach (var item in windows)
            {
                if (item == window || !item.gameObject.activeInHierarchy) continue;
                item.Hide(true);
            }
        }
    }
}