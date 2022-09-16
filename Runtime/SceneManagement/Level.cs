using UnityEngine;

namespace FourDoor.GameLogic
{
    public abstract class Level : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Exit();
    }
}