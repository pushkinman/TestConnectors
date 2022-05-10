namespace TestConnectors.Connectables.States
{
    public abstract class BaseSelectionState
    {
        public abstract void EnterState(ConnectablesSelectionStateManager connectablesSelectionStateManager);
        public abstract void UpdateState(ConnectablesSelectionStateManager connectablesSelectionStateManager);
    }
}