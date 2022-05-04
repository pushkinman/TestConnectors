using TestConnectors.Cameras;
using TestConnectors.Enums;
using TestConnectors.Extensions;
using TestConnectors.Input;
using TestConnectors.Interfaces;
using TestConnectors.Spheres;

namespace TestConnectors
{
    public class CompositionRoot
    {
        private static IInputManager _inputManager;
        private static ISpawner _spawner;
        private static IPlayerCamera _playerCamera;
        private static IResourceManager _resourceManager;

        public static IInputManager GetInputManager()
        {
            return _inputManager ??= new InputManager();
        }
        
        public static ISpawner GetSpawner()
        {
            return _spawner ??= GameObjectExtensions.CreateGameObjectWithComponent<SphereSpawner>();
        }

        public static IPlayerCamera GetPlayerCamera()
        {
            var resourceManager = GetResourceManager();
            return _playerCamera ??= resourceManager.LoadResource<PlayerCamera, ECamera>(ECamera.PlayerCamera);
        }

        public static IResourceManager GetResourceManager()
        {
            return _resourceManager ??= GameObjectExtensions.CreateGameObjectWithComponent<ResourceManager>();
        }
    }
}