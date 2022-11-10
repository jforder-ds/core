using System;
using UnityEngine;

namespace FourDoor.Input
{
    [RequireComponent(typeof(Swipe))]
    public class SwipeLogger : MonoBehaviour
    {
        [SerializeField] private bool showLog;
        
        private Swipe _swipe;
        private readonly string _logPrefix = "Swipe_";
        
        private void Awake() => _swipe = GetComponent<Swipe>();
        private void OnEnable()
        {
            if(!showLog) return;
            
            _swipe.OnSwipeUp += () => Debug.Log($"{_logPrefix}UP");
            _swipe.OnSwipeDown += () => Debug.Log($"{_logPrefix}DOWN");
            _swipe.OnSwipeLeft += () => Debug.Log($"{_logPrefix}LEFT");
            _swipe.OnSwipeRight += () => Debug.Log($"{_logPrefix}RIGHT");
        }
    }
}