namespace TestConnectors.Connectable.States
{
    public abstract class BaseSelectionState
    {
        public abstract void EnterState(ConnectablesManager connectablesManager);
        public abstract void UpdateState(ConnectablesManager connectablesManager);
    }
}