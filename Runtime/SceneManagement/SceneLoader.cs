using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FourDoor.GameLogic
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        private readonly List<Scene> _loadedScenes = new List<Scene>();

        private float _loadProgress;
        private const float ProgressComplete = 0.9f;
        private Scene _lastSceneLoaded;
        
        public float LoadingProgress => _loadProgress;
        public Scene LastSceneLoaded => _lastSceneLoaded;
        
        private static string _booterSceneName = "_Booter";
        
        /// <summary>
        /// Creates a Temp Base scene to make sure there is always a scene loaded
        /// Unless loaded from _BooterScene
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateBaseScene()
        {
            if (!SceneManager.GetActiveScene().name.Equals(_booterSceneName))
            {
                var baseTempScene = SceneManager.CreateScene("BaseTemp");
                SceneManager.SetActiveScene(baseTempScene);
            }
        }
        
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        public async Task LoadSceneAdditive(string sceneName)
        {
            var scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            scene.allowSceneActivation = false;

            await UpdateLoadingProgress(scene);
            
            scene.allowSceneActivation = true;
        }

        public async Task UnLoadScene(string sceneName)
        {
            bool checkSceneIsLoaded = _loadedScenes.Any(scene => scene.name.Equals(sceneName));
            
            if(!checkSceneIsLoaded)
                return;
            
            var scene = SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.None);
            scene.allowSceneActivation = false;

            await UpdateLoadingProgress(scene);

            scene.allowSceneActivation = true;
        }
        
        private void OnSceneLoaded(Scene loadedScene, LoadSceneMode loadedMode)
        {
            _loadedScenes.Add(loadedScene);
            _lastSceneLoaded = loadedScene;
        }
        
        private void OnSceneUnloaded(Scene unloadedScene)
        {
            _loadedScenes.Remove(unloadedScene);
        }

        private async Task UpdateLoadingProgress(AsyncOperation scene)
        {
            do
            {
                await Task.Delay(100);
                _loadProgress = scene.progress;
            } while (_loadProgress < ProgressComplete);
        }
    }
}