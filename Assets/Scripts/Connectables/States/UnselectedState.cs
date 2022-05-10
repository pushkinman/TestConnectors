using TestConnectors.Enums;
using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors.Connectable.States
{
    public class UnselectedState : BaseSelectionState
    {
        public override void EnterState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            connectablesSelectionSelectionStateManager.DeselectAllSpheres();
            connectablesSelectionSelectionStateManager.UpdateSpheres();
        }

        public override void UpdateState(ConnectablesSelectionSelectionStateManager connectablesSelectionSelectionStateManager)
        {
            if (connectablesSelectionSelectionStateManager.InputProvider.GetMouseButtonDown(0) == true)
            {
                var ray = connectablesSelectionSelectionStateManager.PlayerCamera.Camera.ScreenPointToRay(connectablesSelectionSelectionStateManager
                    .InputProvider.MousePosition);

                if (Physics.Raycast(ray, out var hit))
                {
                    var objectHit = hit.transform;
                    var hitSphere = objectHit.GetComponent<SelectableSphere>();

                    if (hitSphere == null) return;

                    hitSphere.GetParentConnectable().IsSphereSelected = true;
                    connectablesSelectionSelectionStateManager.FirstSelectedSphere = hitSphere;

                    connectablesSelectionSelectionStateManager.ChangeSelectionState(connectablesSelectionSelectionStateManager.HoldingState);
                }
            }
        }
    }
}