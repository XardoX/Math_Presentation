using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

namespace MathPresentation
{
    public class MaskController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particles;

        [SerializeField]
        private SplineAnimate splineAnimate;

        public void Play()
        {
            particles.Stop();
            particles.Play();
            splineAnimate.Restart(true);
        }

        public void ResetMask()
        {
            splineAnimate.Restart(false);
        }
    }
}