namespace TestConnectors.Connectable.States
{
    public abstract class BaseSelectionState
    {
        public abstract void EnterState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager);
        public abstract void UpdateState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager);
    }
}