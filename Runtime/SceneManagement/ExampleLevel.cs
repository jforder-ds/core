using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace FourDoor.GameLogic
{
    public class ExampleLevel : Level
    {
        [Header("Scene Handling")]
        [SerializeField] private List<SceneAsset> loadScenes = new List<SceneAsset>();
        [SerializeField] private List<SceneAsset> unloadScenes = new List<SceneAsset>();

        public override async void Init() {}

        public override async void Exit()
        {
            await LoadExitScenes();
            await UnloadExitScenes();
        }
        
        private async Task LoadExitScenes()
        {
            if (loadScenes.Count > 0)
            {
                foreach (var scene in loadScenes)
                {
                    await SceneLoader.Instance.LoadSceneAdditive(scene.name);
                }
            }
        }

        private async Task UnloadExitScenes()
        {
            if (unloadScenes.Count > 0)
            {
                foreach (var scene in unloadScenes)
                {
                    await SceneLoader.Instance.UnLoadScene(scene.name);
                }
            }
        }
    }
}