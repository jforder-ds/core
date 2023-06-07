using UnityEngine;

namespace FourDoor
{
    public class GameState : MonoBehaviour, IState
    {
        public virtual void Enter() {}

        public virtual void Run() {}

        public virtual void Exit() {}
    }
}