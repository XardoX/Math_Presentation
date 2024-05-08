using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using DG.Tweening;

namespace MathPresentation
{
    public class ChalkMask : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particles;

        [SerializeField]
        private SplineAnimate splineAnimate;

        Tween tween;
        public void Play()
        {
            particles.Stop();
            splineAnimate.NormalizedTime = 0f;
            particles.Play();
            tween = DOTween.To(() => splineAnimate.NormalizedTime, x => splineAnimate.NormalizedTime = x, 1f, splineAnimate.Duration)
                .SetEase(Ease.InOutSine);
        }

        public void PlayReverse()
        {
            particles.Stop();
            splineAnimate.NormalizedTime = 1f;
            particles.Play();
            tween = DOTween.To(() => splineAnimate.NormalizedTime, x => splineAnimate.NormalizedTime = x, 0f, splineAnimate.Duration)
                .SetEase(Ease.InOutSine);
        }

        public void ResetMask()
        {
            splineAnimate.Restart(false);
        }

        public void ClearMask()
        {
            particles.Clear();
        }
    }
}