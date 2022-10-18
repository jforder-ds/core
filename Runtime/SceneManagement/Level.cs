using UnityEngine;

namespace FourDoor.Level
{
    public abstract class Level : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Exit();
    }
}