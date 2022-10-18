using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FourDoor.GameLogic
{
    public class TravelToScene : MonoBehaviour
    {
        [Header("---Scene References---")]

        [SerializeField] private List<SceneReference> scenesToLoad = new List<SceneReference>();
        [SerializeField] private List<SceneReference> scenesToUnload = new List<SceneReference>();

        [SerializeField] private bool unloadSelf;
        
        [Header("---Transition---")] 
        [SerializeField] private SceneReference transitionScene;
        
        private List<string> _scenesToLoad = new List<string>();
        private List<string> _scenesToUnload = new List<string>();

        private bool HasLoadedScenes => scenesToLoad.Count > 0;
        private bool HasUnloadedScenes => scenesToUnload.Count > 0 || unloadSelf;

        public void Travel()
        {
            if(!HasLoadedScenes && !HasUnloadedScenes)
                Debug.Log("Nothing to Process!");

            StoreScenesInContainer();
            SceneLoader.Instance.LoadSceneAdditive(transitionScene.ScenePath);
        }

        private void StoreScenesInContainer()
        {
            _scenesToLoad = scenesToLoad.Select(asset => asset.ScenePath).ToList();
            _scenesToUnload = scenesToUnload.Select(asset => asset.ScenePath).ToList();

            if(unloadSelf)
                _scenesToUnload.Add(gameObject.scene.name);
            
            ScenesContainer.Apply(_scenesToLoad, _scenesToUnload);
        }
    }
}