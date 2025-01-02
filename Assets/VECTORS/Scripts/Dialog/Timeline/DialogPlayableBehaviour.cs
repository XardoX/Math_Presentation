using MathPresentation.DialogSystem;
using UnityEngine;
using UnityEngine.Playables;

namespace MathPresentation.DialogSystem.Timeline
{
    public class DialogPlayableBehaviour : PlayableBehaviour
    {
        public DialogData dialogData;

        private DialogUI dialogUI;

        public override void OnGraphStart(Playable playable)
        {
            dialogUI = Object.FindFirstObjectByType<DialogUI>();
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (dialogUI == null || dialogData == null) return;

            if (!dialogUI.IsTyping)
            {
                dialogUI.StartDialog(dialogData);
            }
        }

        public override void OnGraphStop(Playable playable)
        {
            if (dialogUI != null)
            {
                dialogUI.EndDialog();
            }
        }
    }
}