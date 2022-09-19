using System.Threading.Tasks;
using UnityEngine;

namespace FourDoor.GameLogic
{
    public class SceneLoadHandler : MonoBehaviour
    {
        public async Task LoadScenes()
        {
            if (ScenesContainer.LoadScenes.Count > 0)
            {
                foreach (var scene in ScenesContainer.LoadScenes)
                {
                    await SceneLoader.Instance.LoadSceneAdditive(scene);
                }
            }
        }

        public async Task UnloadScenes()
        {
            if (ScenesContainer.UnloadScenes.Count > 0)
            {
                foreach (var scene in ScenesContainer.UnloadScenes)
                {
                    await SceneLoader.Instance.UnLoadScene(scene);
                }
            }
        }
    }
}