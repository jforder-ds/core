using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace FourDoor.GameLogic
{
    public class SceneLoader : Singleton<SceneLoader>
    {
        [SerializeField] private bool useAddressableSystem;
        
        private readonly List<Scene> _loadedScenes = new();
        private readonly List<AsyncOperationHandle<SceneInstance>> _addressableLoadedScenes = new();

        private float _loadProgress;
        private const float ProgressComplete = 0.9f;
        private Scene _lastSceneLoaded;
        private AsyncOperationHandle<SceneInstance> _lastSceneLoadedAddressables;
        
        public float LoadingProgress => _loadProgress;
        public Scene LastSceneLoaded
        {
            get
            {
                if (useAddressableSystem)
                {
                    return _lastSceneLoadedAddressables.Result.Scene;
                }
                
                return _lastSceneLoaded;
            }
        }

        private static string _booterSceneName = "_Booter";

        private AssetReference _scene;
        
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
            if (!useAddressableSystem)
            {
                var scene =  SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                
                scene.allowSceneActivation = false;

                await UpdateLoadingProgress(scene);
            
                scene.allowSceneActivation = true;
            }
            else
            {
                var scene = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Additive, false);

                scene.Completed += OnSceneLoadedAddressables;
                
                while (scene.PercentComplete < ProgressComplete)
                {
                    _loadProgress = scene.PercentComplete;
                }
            }
        }
        
        public async Task UnLoadScene(string sceneName)
        {
            bool checkSceneIsLoaded = _loadedScenes.Any(scene => scene.name.Equals(sceneName));
            
            if(!checkSceneIsLoaded)
                return;

            if (!useAddressableSystem)
            {
                SceneManager.UnloadSceneAsync(sceneName, UnloadSceneOptions.None);
            }
            else
            {
                var loadedScene = _addressableLoadedScenes.Find(handle => handle.Result.Scene.name.Equals(sceneName));
                Addressables.UnloadSceneAsync(loadedScene);
            }
        }
        
        private void OnSceneLoadedAddressables(AsyncOperationHandle<SceneInstance> sceneHandle)
        {
            _addressableLoadedScenes.Add(sceneHandle);
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