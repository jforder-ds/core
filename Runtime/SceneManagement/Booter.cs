using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FourDoor.GameLogic
{
    public class Booter : MonoBehaviour
    {
        [SerializeField] private List<SceneAsset> startUpScenes = new List<SceneAsset>();
        
        private void Awake()
        {
            foreach (var scene in startUpScenes)
            {
                SceneLoader.Instance.LoadSceneAdditive(scene.name);
            }
        }
    }
}
