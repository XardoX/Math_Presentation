using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.DialogSystem
{
    public class BackgroundDialog : MonoBehaviour
    {
        [SerializeField]
        private DialogData dialog;
        [SerializeField]
        private float dialogDuration;
        [SerializeField]
        private DialogCloud[] characterDialogClouds;

        private bool inProgress = false;
        public void ShowDialog()
        {
            if (inProgress == false)
                StartCoroutine(PlayDialog());
        }

        private IEnumerator PlayDialog()
        {
            dialog.LoadLines();
            inProgress = true;
            foreach (var line in dialog.Dialog)
            {
                characterDialogClouds[line.SpeakingCharacterId].ShowDialogText(line.Line);
                yield return new WaitForSeconds(dialogDuration);
                characterDialogClouds[line.SpeakingCharacterId].Hide();
            }
            inProgress = false;
        }
    }
}