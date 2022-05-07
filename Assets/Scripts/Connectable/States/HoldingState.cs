using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class HoldingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesManager connectablesManager)
        {
            connectablesManager.UpdateSpheres(true);
        }

        public override void UpdateState(ConnectablesManager connectablesManager)
        {
            if (connectablesManager._inputProvider.GetMouseButtonUp(0) == true)
            {
                var ray = connectablesManager._camera.ScreenPointToRay(connectablesManager._inputProvider
                    .MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null) return;
                    
                    if (hitSphere.GetInstanceID() == connectablesManager._selectedSphere.GetInstanceID())
                    {
                        connectablesManager.ChangeSelectionState(connectablesManager.ClickingState);
                    }
                    else
                    {
                        connectablesManager.CreateConnection(connectablesManager._selectedSphere.transform,
                            hitSphere.transform);
                        connectablesManager.DeselectSphere();
                        connectablesManager.ChangeSelectionState(connectablesManager.UnselectedState);
                    }
                }
            }
        }
    }
}