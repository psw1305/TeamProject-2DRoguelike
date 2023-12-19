using UnityEngine;

public class SimplePair<T1, T2>
{
    public T1 value1;
    public T2 value2;

    public SimplePair(T1 value1, T2 value2)
    {
        this.value1 = value1;
        this.value2 = value2;
    }
}

/// <summary>
/// 게임 오브젝트, Vector2를 페어로 값을 가져옴
/// 해당 Vector2 포지션에 오브젝트를 배치하도록 함
/// </summary>
[System.Serializable]
public class PairWithObjectVector2 : SimplePair<GameObject, Vector2>
{
    public PairWithObjectVector2(GameObject value1, Vector2 value2) : base(value1, value2) { }
}
