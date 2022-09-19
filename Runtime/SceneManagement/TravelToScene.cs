using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace FourDoor.GameLogic
{
    public class TravelToScene : MonoBehaviour
    {
        [Header("---Scene References---")] 
        
        [SerializeField] private List<SceneAsset> scenesToLoad = new List<SceneAsset>();
        [SerializeField] private List<SceneAsset> scenesToUnload = new List<SceneAsset>();
        [SerializeField] private bool unloadSelf;
        
        [Header("---Transition---")] 
        [SerializeField] private SceneAsset transitionScene;

        private bool HasLoadedScenes => scenesToLoad.Count > 0;
        private bool HasUnloadedScenes => scenesToUnload.Count > 0 || unloadSelf;

        public void Travel()
        {
            if(!HasLoadedScenes && !HasUnloadedScenes)
                Debug.Log("Nothing to Process!");

            StoreScenesInContainer();
            SceneLoader.Instance.LoadSceneAdditive(transitionScene.name);
        }

        private void StoreScenesInContainer()
        {
            List<string> loadScenes = scenesToLoad.Select(t => t.name).ToList();
            List<string> unloadScenes = scenesToUnload.Select(t => t.name).ToList();
            
            if(unloadSelf)
                unloadScenes.Add(gameObject.scene.name);
            
            ScenesContainer.Apply(loadScenes, unloadScenes);
        }
    }
}