[System.Serializable]
public class Wrapper<T>
{
    public T[] array;
}

public interface IDataKey
{
    public string Index { get; }
}

[System.Serializable]
public class BaseInformation : IDataKey
{
    public string index;
    public string Index { get => index; }
}
