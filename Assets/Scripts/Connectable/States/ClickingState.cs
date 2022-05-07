using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class ClickingState : BaseSelectionState
    {
        public override void EnterState(ConnectablesStateManager connectablesStateManager)
        {
            connectablesStateManager.UpdateSpheres(true);
        }

        public override void UpdateState(ConnectablesStateManager connectablesStateManager)
        {
            if (connectablesStateManager.InputProvider.GetMouseButtonDown(0) == true)
            {
                var ray = connectablesStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesStateManager.InputProvider
                    .MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere != null &&
                        hitSphere.GetInstanceID() != connectablesStateManager.SelectedSphere.GetInstanceID())
                    {
                        connectablesStateManager.CreateConnection(connectablesStateManager.SelectedSphere.transform,
                            hitSphere.transform);
                    }

                    connectablesStateManager.DeselectSphere();
                    connectablesStateManager.ChangeSelectionState(connectablesStateManager.UnselectedState);
                }
            }
        }
    }
}