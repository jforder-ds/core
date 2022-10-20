using System.Threading.Tasks;
using UnityEngine;

namespace FourDoor.GameLogic
{
    public class InstantTransition : Transition
    {
        [SerializeField] private SceneLoadHandler sceneLoadHandler;
        private void Start() => Transition();

        private async Task Transition()
        {
            //await Begin();
            await sceneLoadHandler.LoadScenes();
            await sceneLoadHandler.UnloadScenes();
            //await End();
            
            await SceneLoader.Instance.UnLoadScene(gameObject.scene.name);
        }
        
        
        public override async Task Begin() => await Task.Yield();
        public override async Task End() => await Task.Yield();
    }
}