using System.Collections.Generic;

namespace FourDoor.GameLogic
{
    public static class ScenesContainer
    {
        private static List<string> _loadScenes;
        private static List<string> _unloadScenes;

        public static List<string> LoadScenes => _loadScenes;
        public static List<string> UnloadScenes => _unloadScenes;
        
        public static void Apply(List<string> scenesToLoad, List<string> scenesToUnload)
        {
            _loadScenes = scenesToLoad;
            _unloadScenes = scenesToUnload;
        }
    }
}