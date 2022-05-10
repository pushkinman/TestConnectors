using TestConnectors.Cameras;
using TestConnectors.Connectable;
using TestConnectors.Enums;
using TestConnectors.Extensions;
using TestConnectors.Input;
using TestConnectors.Interfaces;
using TestConnectors.Resources;

namespace TestConnectors
{
    public static class CompositionRoot
    {
        private static IInputManager _inputManager;
        private static IConnectablesSelectionStateManager _connectablesSelectionStateManager;
        private static IConnectablesMover _connectablesMover;
        private static IPlayerCamera _playerCamera;
        private static IResourceManager _resourceManager;

        public static IInputManager GetInputManager()
        {
            return _inputManager ??= new InputManager();
        }
        
        public static IConnectablesSelectionStateManager GetConnectablesStateManager()
        {
            return _connectablesSelectionStateManager ??= GameObjectExtensions.CreateGameObjectWithComponent<ConnectablesSelectionSelectionStateManager>();
        }
        
        public static IConnectablesMover GetConnectablesMover()
        {
            return _connectablesMover ??= GameObjectExtensions.CreateGameObjectWithComponent<ConnectablesMover>();
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