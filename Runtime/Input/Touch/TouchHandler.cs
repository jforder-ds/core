using UnityEngine;

namespace FourDoor.Input
{
    public abstract class TouchHandler : MonoBehaviour, ITouchState
    {
        private void Update()
        {
            #if UNITY_EDITOR
            EditorInput();
            #else
            DeviceInput();
            #endif
        }

        private void EditorInput()
        {
            var mousePosition = UnityEngine.Input.mousePosition;
        
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                Begin(mousePosition);
            }

            if (UnityEngine.Input.GetMouseButton(0))
            {
                Touch(mousePosition);
            }
        
            if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                End(mousePosition);
            }
        }
        
        private void DeviceInput()
        {
            foreach (Touch touch in UnityEngine.Input.touches) {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        Begin(touch.position);
                        break;
                    case TouchPhase.Moved:
                    {
                        Touch(touch.position);
                        break;
                    }
                    case TouchPhase.Ended:
                        Touch(touch.position);
                        break;
                }
            }
        }
        
        public abstract void Begin(Vector2 position);
        public abstract void Touch(Vector2 position);
        public abstract void End(Vector2 position);
    }
}