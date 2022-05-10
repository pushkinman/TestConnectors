using System.Collections.Generic;

namespace TestConnectors.Interfaces
{
    public interface IConnectablesSpawner
    {
        IEnumerable<Connectables.Connectable> SpawnObjects(int count, float spawnRadius);
    }
}