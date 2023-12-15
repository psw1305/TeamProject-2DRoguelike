/// <summary>
/// 열거형 모음 클래스
/// </summary>

// 레벨 생성 방향
public enum Didirection
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
    Special // TODO : 후순위
}

// 아이템 상호작용 타입
public enum HowItemGet
{
    None,
    Automatic,
    Interact // TODO : 후순위
}
