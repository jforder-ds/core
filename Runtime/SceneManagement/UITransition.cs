using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FourDoor.GameLogic
{
    public class UITransition : Transition
    {
        [SerializeField] private Image image;
        [SerializeField] private float  duration = 1;
        [SerializeField] private SceneLoadHandler sceneLoadHandler;
        
        private float _alpha;

        private void Awake()
        {
            _alpha = 0;
            UpdateAlpha();
        }

        private void Start() => Transition();

        private async Task Transition()
        {
            await Begin();
            await sceneLoadHandler.LoadScenes();
            await sceneLoadHandler.UnloadScenes();
            await End();
            
            await SceneLoader.Instance.UnLoadScene(gameObject.scene.name);
        }

        public override async Task Begin()
        {
            DOTween.To(()=> _alpha, x=> _alpha = x, 1, duration).OnUpdate(UpdateAlpha);
            await Task.Delay((int)(duration * 1000));
        }

        public override async Task End()
        {
            DOTween.To(()=> _alpha, x=> _alpha = x, 0, duration).OnUpdate(UpdateAlpha);
            await Task.Delay((int)(duration * 1000));
        }

        private void UpdateAlpha()
        {
            var color = image.color;
            color.a = _alpha;
            image.color = color;
        }
    }
}