using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathPresentation.DialogSystem
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField]
        private DialogUI dialogUI;

        [SerializeField]
        private DialogData currentDialog;
        private int currentDialogLine;

        [ButtonMethod]
        public void StartDialog()
        {
            StartDialog(currentDialog);
        }

        public void StartDialog(DialogData dialog)
        {
            dialog.LoadLines();
            currentDialog = dialog;
            currentDialogLine = 0;
            dialogUI.StartDialog(dialog);
        }


        private void Update()
        {

            if (currentDialog == null) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }

        public void NextLine()
        {
            if (currentDialog == null)
            {
                dialogUI.EndDialog();
            }
            else if (currentDialogLine >= currentDialog.Dialog.Count - 1)
            {
                dialogUI.EndDialog();
            }
            else if (dialogUI.IsTyping)
            {
                dialogUI.DisplayInstantly(currentDialog.Dialog[currentDialogLine].Line);
            }
            else
            {
                currentDialogLine++;
                dialogUI.PlayNextLine(currentDialog.Dialog[currentDialogLine]);
            }

        }
    }
}