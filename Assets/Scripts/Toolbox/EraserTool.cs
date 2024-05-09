using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
namespace MathPresentation.Toolbox
{
    public class EraserTool : Tool
    {
        [SerializeField]
        private float holdTimeToEraseAll = 1.5f;

        [SerializeField]
        private float radius = 1f;

        [SerializeField]
        private ContactFilter2D contactFilter;

        [SerializeField]
        private DrawTool drawTool;

        [SerializeField]
        private ChalkMask chalkMask;

        [SerializeField]
        private SpriteRenderer eraseCircle;

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
            Invoke(nameof(EraseAll), 1f); //todo courutine?
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

        private void Erase(Vector2 point)
        {
            List<Collider2D> results = new();
            if (Physics2D.OverlapCircle(point, radius, contactFilter, results) > 0)
            {
                results.ForEach(_ => Destroy(_.gameObject));
                drawTool.ClearMissing();
            }
        }

        private void Update()
        {
            if (isEnabled == false) return;
            var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            point.z = 0f;
            eraseCircle.transform.position = point;
            if (Input.GetMouseButton(0))
            {
                Erase(point);
            }

            if(Input.GetMouseButtonDown(0))
            {
                eraseCircle.DOFade(0.2f, 0.25f);
            } 
            else if (Input.GetMouseButtonUp(0))
            {
                eraseCircle.DOFade(0.08f, 0.25f);
            }
        }

        protected override void OnEnabled()
        {
            base.OnEnabled();
            eraseCircle.gameObject.SetActive(true);
        }

        protected override void OnDisabled()
        {
            base.OnDisabled();
            eraseCircle.gameObject.SetActive(false);
        }
    }
}