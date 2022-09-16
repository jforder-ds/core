using System;
using UnityEngine;

namespace FourDoor.Input
{
    public class Swipe : TouchHandler
    {
        [SerializeField] private bool detectSwipeAfterRelease;
        [SerializeField] private float swipeThreshold = 20f;
        
        private Vector2 _fingerDownPos;
        private Vector2 _fingerUpPos;

        public Action OnSwipeDown;
        public Action OnSwipeUp;
        public Action OnSwipeLeft;
        public Action OnSwipeRight;
        
        public Vector2 DragDelta { get; private set; }
        public Vector2 DragMoveAmount { get; private set; }
        
        public override void Begin(Vector2 position)
        {
            _fingerUpPos = position;
            _fingerDownPos = position;
        }

        public override void Touch(Vector2 position)
        {
            if (!detectSwipeAfterRelease) {
                _fingerDownPos = position;
                DetectSwipe ();
            }
            
            DragDelta = _fingerDownPos - position;
            DragMoveAmount = position - _fingerUpPos;
        }

        public override void End(Vector2 position)
        {
            _fingerDownPos = position;
            DetectSwipe ();
        }
        
        void DetectSwipe ()
        {
            if (VerticalMoveValue () > swipeThreshold && VerticalMoveValue () > HorizontalMoveValue ()) 
            {
                if (_fingerDownPos.y - _fingerUpPos.y > 0) 
                {
                    OnSwipeUp?.Invoke();
                }
                else if (_fingerDownPos.y - _fingerUpPos.y < 0) 
                {
                    OnSwipeDown?.Invoke();
                }
                
                _fingerUpPos = _fingerDownPos;
            } 
            else if (HorizontalMoveValue () > swipeThreshold && HorizontalMoveValue () > VerticalMoveValue ()) 
            {
                if (_fingerDownPos.x - _fingerUpPos.x > 0) 
                {
                    OnSwipeRight?.Invoke();
                } 
                else if (_fingerDownPos.x - _fingerUpPos.x < 0) 
                {
                    OnSwipeLeft?.Invoke();
                }
                
                _fingerUpPos = _fingerDownPos;
            }
        }
        
        float VerticalMoveValue () => Mathf.Abs (_fingerDownPos.y - _fingerUpPos.y);
        float HorizontalMoveValue () => Mathf.Abs (_fingerDownPos.x - _fingerUpPos.x);
    }
}