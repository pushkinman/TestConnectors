using TestConnectors.Extensions;
using TestConnectors.Interfaces;

namespace TestConnectors.Input
{
    public class InputManager : IInputManager
    {
        public IInputProvider InputProvider { get; }

        public InputManager()
        {
            InputProvider = GameObjectExtensions.CreateGameObjectWithComponent<InputProvider>();
        }
    }
}