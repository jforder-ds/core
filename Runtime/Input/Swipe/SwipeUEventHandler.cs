using UnityEngine;
using UnityEngine.Events;

namespace FourDoor.Input
{
    public class SwipeUEventHandler : MonoBehaviour
    {
        [SerializeField] private Swipe swipe;

        [SerializeField] private UnityEvent OnSwipeUp;
        [SerializeField] private UnityEvent OnSwipeDown;
        [SerializeField] private UnityEvent OnSwipeLeft;
        [SerializeField] private UnityEvent OnSwipeRight;
        
        private void OnEnable()
        {
            swipe.OnSwipeUp += SwipeUp;
            swipe.OnSwipeDown += SwipeDown;
            swipe.OnSwipeLeft += SwipeLeft;
            swipe.OnSwipeRight += SwipeRight;
        }

        private void OnDisable()
        {
            swipe.OnSwipeUp -= SwipeUp;
            swipe.OnSwipeDown -= SwipeDown;
            swipe.OnSwipeLeft -= SwipeLeft;
            swipe.OnSwipeRight -= SwipeRight;
        }

        private void SwipeUp() => OnSwipeUp?.Invoke();
        private void SwipeDown() => OnSwipeDown?.Invoke();
        private void SwipeLeft() => OnSwipeLeft?.Invoke();
        private void SwipeRight() => OnSwipeRight?.Invoke();
    }
}