using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            UnityEngine.Profiling.Profiler.BeginSample("Singleton:Lock");

            //lock (_lock)
            {
                if (_instance == null)
                {
                    UnityEngine.Profiling.Profiler.BeginSample(string.Format("Singleton:FindObjectOfType<{0}>", typeof(T).Name));
                    _instance = (T)FindObjectOfType(typeof(T));
                    UnityEngine.Profiling.Profiler.EndSample();

                    // in case something in editor calls a singleton
                    // don't start adding things in not in play mode
                    if (Application.isPlaying)
                    {
                        UnityEngine.Profiling.Profiler.BeginSample("Singleton:FindObjectsOfType");
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more than 1 singleton!" +
                                " Reopening the scene might fix it.");
                            return _instance;
                        }
                        UnityEngine.Profiling.Profiler.EndSample();

                        UnityEngine.Profiling.Profiler.BeginSample("Singleton:Create");
                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T).ToString();

                            DontDestroyOnLoad(singleton);

                            Debug.Log("[Singleton] An instance of " + typeof(T) +
                                " is needed in the scene, so '" + singleton +
                                "' was created with DontDestroyOnLoad.");
                        }
                        else
                        {
                            //Debug.Log("[Singleton] Using instance already created: " +
                            //    _instance.gameObject.name);
                        }
                        UnityEngine.Profiling.Profiler.EndSample();

                    }
                }
            }

            UnityEngine.Profiling.Profiler.EndSample();

            return _instance;
        }
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}