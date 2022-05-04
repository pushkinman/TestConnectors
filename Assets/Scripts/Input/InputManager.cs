using TestConnectors.Extensions;
using TestConnectors.Interfaces;

namespace TestConnectors.Input
{
    public class InputManager : IInputManager
    {
        public IInputProvider InputProviderInGame { get; }

        public InputManager()
        {
            InputProviderInGame = GameObjectExtensions.CreateGameObjectWithComponent<InputProvider>();
        }
    }
}