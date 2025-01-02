using Timeline.Samples;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace MathPresentation.SpaceShooter.Timeline
{
    public class SpaceShipClip : PlayableAsset
    {
        [NoFoldOut]
        [NotKeyable]
        public SpaceShipBehaviour template = new();

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            return ScriptPlayable<SpaceShipBehaviour>.Create(graph, template);
        }
    }
}