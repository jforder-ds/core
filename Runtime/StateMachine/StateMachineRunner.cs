using UnityEngine;

namespace FourDoor
{
    public class StateMachineRunner : MonoBehaviour
    {
        private StateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = new StateMachine();
            GameState welcomeState = new GameState();
            
            _stateMachine.ChangeState(welcomeState);
        }
        
        private void Update()
        {
            if (_stateMachine != null)
            {
                _stateMachine.Run();
            }
        }

        public void ChangeState(GameState state)
        {
            if (_stateMachine != null)
            {
                _stateMachine.ChangeState(state);
            }
        }
    }

}