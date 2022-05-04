using TestConnectors.Interfaces;
using UnityEngine;

namespace TestConnectors
{
    public class ResourceManager : MonoBehaviour, IResourceManager
    {
        public T1 LoadResource<T1, T2>(T2 resource) where T1 : Object where T2 : System.Enum
        {
            var path = $"{typeof(T2).Name}/{resource.ToString()}";
            return Instantiate(Resources.Load<T1>(path));
        }
    }
}