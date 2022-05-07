namespace TestConnectors.Connectable.States
{
    public abstract class BaseSelectionState
    {
        public abstract void EnterState(ConnectablesStateManager connectablesStateManager);
        public abstract void UpdateState(ConnectablesStateManager connectablesStateManager);
    }
}