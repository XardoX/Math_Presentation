using MathPresentation.DialogSystem;
using UnityEngine;
using UnityEngine.Playables;

namespace MathPresentation.DialogSystem.Timeline
{
    public class DialogPlayableBehaviour : PlayableBehaviour
    {
        public DialogData dialogData;
        public bool pauseTimeline;

        private DialogController dialogController;
        private PlayableDirector director;

        private bool dialogPlayed;

        public override void OnGraphStart(Playable playable)
        {
            dialogController = Object.FindFirstObjectByType<DialogController>();
            director = Object.FindFirstObjectByType<PlayableDirector>();
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if(Application.isPlaying == false) return;
            if (dialogPlayed) return;
            if (dialogController == null || dialogData == null) return;

            if (!dialogController.DialogUI.IsTyping)
            {
                dialogController.StartDialog(dialogData);
                dialogPlayed = true;
            }

            if (pauseTimeline && director != null)
            {
                director.Pause();
                dialogController.DialogUI.onDialogEnded += Resume;
            }
        }

        private void Resume()
        {
            Debug.Log("Resumed");
            director.Resume();
            dialogController.DialogUI.onDialogEnded -= Resume;
        }
    }
}