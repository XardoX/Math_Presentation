using MathPresentation.DialogSystem.Timeline;
using UnityEngine;
using UnityEngine.Playables;
namespace MathPresentation.DialogSystem.Timeline
{
    [System.Serializable]
    public class DialogPlayableAsset : PlayableAsset
    {
        public DialogData dialogData;
        public bool pauseTimeline;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialogPlayableBehaviour>.Create(graph);

            DialogPlayableBehaviour dialogBehaviour = playable.GetBehaviour();
            dialogBehaviour.dialogData = dialogData;
            dialogBehaviour.pauseTimeline = pauseTimeline;

            return playable;
        }
    }
}