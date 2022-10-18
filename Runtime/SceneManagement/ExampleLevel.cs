using FourDoor.GameLogic;
using UnityEngine;
using UnityEngine.UI;

namespace FourDoor.Level
{
    public class ExampleLevel : Level
    {
        [Header("Exit Level")]
        [SerializeField] private Button getStartedButton;
        [SerializeField] private TravelToScene travelToScene;

        private void OnEnable()
        {
            getStartedButton.onClick.AddListener(OnClickGetStartedButton);
        }

        private void OnDisable()
        {
            getStartedButton.onClick.RemoveListener(OnClickGetStartedButton);
        }

        private void OnClickGetStartedButton()
        {
            travelToScene.Travel();
        }

        public override async void Init() {}

        public override async void Exit()
        {
           
        }
    }
}