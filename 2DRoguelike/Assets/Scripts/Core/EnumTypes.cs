/// <summary>
/// 열거형 모음 클래스
/// </summary>

// 레벨 생성 방향
public enum RoomDirection
{
    Up,
    Down,
    Left,
    Right
};

// 방 타입
public enum RoomType 
{ 
    Start, 
    Normal, 
    Boss, 
    Treasure, 
    Shop 
};

// 픽업 아이템 타입
public enum ItemType
{
    None,
    Coin,
    Heart,
    Bomb,
    Key,
    Chest,
    Special
}

public enum EnemyState
{
    Ready, // 이벤트씬 용
    live,
    Die
}
