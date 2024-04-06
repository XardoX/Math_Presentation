using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Timeline.Samples
{
    // Runtime representation of a Tween clip.
    public class TweenBehaviour : PlayableBehaviour
    {
        public Vector3 startPosition;
        public Vector3 endPosition;
        public Vector3 startRotation;
        public Vector3 endRotation;
        public Vector3 startScale;
        public Vector3 endScale;


        public Transform startLocation;
        public Transform endLocation;
        public bool shouldTweenPosition;
        public bool shouldTweenRotation;

        public AnimationCurve curve;
    }
}
