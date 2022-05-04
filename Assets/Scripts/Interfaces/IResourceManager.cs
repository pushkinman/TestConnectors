namespace TestConnectors.Interfaces
{
    public interface IResourceManager
    {
        T1 LoadResource<T1, T2>(T2 resource) where T1 : UnityEngine.Object where T2 : System.Enum;
    }
}