using TestConnectors.Enums;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class UnselectedState : BaseSelectionState
    {
        public override void EnterState(ConnectablesStateManager connectablesStateManager)
        {
            connectablesStateManager.DeselectAllSpheres();
            connectablesStateManager.UpdateSpheres();
        }

        public override void UpdateState(ConnectablesStateManager connectablesStateManager)
        {
            if (connectablesStateManager.InputProvider.GetMouseButtonDown(0) == true)
            {
                var ray = connectablesStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesStateManager
                    .InputProvider.MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null) return;

                    hitSphere.GetParentConnectable().IsSphereSelected = true;
                    connectablesStateManager.FirstSelectedSphere = hitSphere;

                    connectablesStateManager.ChangeSelectionState(connectablesStateManager.HoldingState);
                }
            }
        }
    }
}