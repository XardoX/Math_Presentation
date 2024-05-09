using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MathPresentation.Toolbox
{
    public class EraserTool : Tool
    {
        [SerializeField]
        private float holdTimeToEraseAll = 1.5f;

        [SerializeField]
        private DrawTool drawTool;

        [SerializeField]
        private ChalkMask chalkMask;

        private float startHoldTime, endHoldtime;

        private bool erasing;

        public float HoldTimeToEraseAll => holdTimeToEraseAll;

        public override void OnToolClickedDown()
        {
            base.OnToolClickedDown();
            startHoldTime = Time.time;
            endHoldtime = Time.time + holdTimeToEraseAll;
            Invoke(nameof(CheckHoldTime), holdTimeToEraseAll);
        }

        public override void OnToolClickedUp()
        {
            base.OnToolClickedUp();
            endHoldtime = Time.time;
            CheckHoldTime();
        }

        public void StartEraseAll()
        {
            erasing = true;
            chalkMask.Play();
            Invoke(nameof(EraseAll), 1f);
        }

        public void EraseAll()
        {
            drawTool.ClearAllInCurrentParent();
            chalkMask.ClearMask();
            erasing = false;
        }

        private void CheckHoldTime()
        {
            var holdTime = endHoldtime - startHoldTime;

            if(holdTime >= holdTimeToEraseAll && erasing == false)
            {
                StartEraseAll();
            }

            startHoldTime = 0f;
            endHoldtime = 0f;
        }

    }
}