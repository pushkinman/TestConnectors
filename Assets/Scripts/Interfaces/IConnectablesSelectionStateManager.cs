using System.Collections.Generic;
using TestConnectors.Enums;

namespace TestConnectors.Interfaces
{
    public interface IConnectablesSelectionStateManager
    {
        IEnumerable<Connectables.Connectable> Connectables { get; set; }
    }
}