using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace FourDoor.GameLogic
{
    public class Booter : MonoBehaviour
    {
        [SerializeField] private List<SceneReference> startUpScenes = new List<SceneReference>();
        
        private List<string> _startUpScenes = new List<string>();

        private void Awake()
        {
            _startUpScenes = startUpScenes.Select(asset => asset.ScenePath).ToList();
            
            foreach (var scene in _startUpScenes)
            {
                SceneLoader.Instance.LoadSceneAdditive(scene);
            }
        }
    }
}
