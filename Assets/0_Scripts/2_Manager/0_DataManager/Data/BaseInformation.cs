[System.Serializable]
public class Wrapper<T>
{
    // 초기 JsonUtility사용 => 유니티 제공 json은 루트가 배열인 형식을 받을 수 없는 한계를 가짐,
    // 해당 한계를 회피하기 위해 감쌀 클래스를 만듦, 후 NewtonsoftJson으로 변경 후 직접 사용하지는 않지만
    // 학습하고 툴 변경을 대비해 유지, 사용함. 
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
