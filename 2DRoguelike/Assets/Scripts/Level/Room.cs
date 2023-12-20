using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Fields

    [SerializeField] private RoomType roomType;
    [SerializeField] private Vector2Int coordinate;
    [SerializeField] private Transform floor;
    [SerializeField] private List<GameObject> doorList;
    [SerializeField] private Transform objectContainer;
    [SerializeField] private Transform enemyContainer;

    private List<GameObject> _activeDoorList = new();
    private List<GameObject> _neighborDoorList = new();
    private List<Room> _neighborRoomList = new();

    private RoomBlueprint _roomBlueprint;
    private bool isArrived;
    private bool isCleared;

    #endregion

    #region Properties

    public RoomType RoomType => roomType;
    public Vector2Int Coordinate => coordinate;
    public List<GameObject> DoorList => doorList;
    public Transform ObjectContainer => objectContainer;
    public Transform EnemyContainer => enemyContainer;
    public int ActiveDoorCount => _activeDoorList.Count;
    public bool IsArrived => isArrived;
    public bool IsCleared => isCleared;

    public void SetRoomType(RoomType roomType)
    {
        this.roomType = roomType;
    }

    public void SetCoordinate(Vector2Int coordinate)
    {
        this.coordinate = coordinate;
    }

    #endregion

    #region Init

    public void Initialize()
    {
        isArrived = false;
        isCleared = false;
        _roomBlueprint = Main.Game.Dungeon.GetRoomBlueprint(roomType);

        for (int i = 0; i < floor.childCount; i++)
        {
            floor.GetChild(i).GetComponent<SpriteRenderer>().sprite = _roomBlueprint.Floor;
        }

        ChangeDoorOutWard();
    }

    #endregion

    #region Door Methods

    /// <summary>
    /// 타입에 따른 문 디자인 변경
    /// </summary>
    private void ChangeDoorOutWard()
    {
        if (roomType == RoomType.Normal || roomType == RoomType.Start) return;

        List<GameObject> doors = new();
        doors.AddRange(_activeDoorList);
        doors.AddRange(_neighborDoorList);

        foreach (var door in doors)
        {
            var doorFrame = door.transform.GetChild(0).GetComponent<SpriteRenderer>();

            switch (roomType)
            {
                case RoomType.Boss:
                    doorFrame.sprite = Main.Resource.GetSprite("door-boss");
                    break;
                case RoomType.Treasure:
                    doorFrame.sprite = Main.Resource.GetSprite("door-treasure");
                    break;
                case RoomType.Shop:
                    //doorFrame.sprite = Main.Resource.GetSprite("door-shop");
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 해당 방향에 위치한 문 활성화
    /// </summary>
    /// <param name="direction"></param>
    /// <param name="neighborRoom"></param>
    /// <param name="neighborDoor"></param>
    public void ActivationDoor(RoomDirection direction, Room neighborRoom, GameObject neighborDoor)
    {
        var door = doorList[(int)direction];

        for (int i = 0; i < door.transform.childCount; i++)
        {
            door.transform.GetChild(i).gameObject.SetActive(true);
        }

        _activeDoorList.Add(door);
        _neighborDoorList.Add(neighborDoor);
        _neighborRoomList.Add(neighborRoom);    
    }

    /// <summary>
    /// 방 클리어 시 => 등록된 문 수 만큼 활성화
    /// </summary>
    public void OpenActivatedDoor()
    {
        for (int i = 0; i < _activeDoorList.Count; i++)
        {
            var door = _activeDoorList[i];
            door.GetComponent<BoxCollider2D>().enabled = false;
            door.GetComponent<Animator>().Play("Open");
        }
    }

    #endregion

    #region Container Methods

    /// <summary>
    /// 방 내부의 콘텐츠 생성 => 장애물, 아이템, 적
    /// </summary>
    public void GenerateRoomContents()
    {
        isArrived = true;

        for (int i = 0; i < _roomBlueprint.ObstacleList.Count; i++)
        {
            GenerateObject(_roomBlueprint.ObstacleList[i].value1, _roomBlueprint.ObstacleList[i].value2, objectContainer);
        }

        for (int i = 0; i < _roomBlueprint.ItemList.Count; i++)
        {
            GenerateObject(_roomBlueprint.ItemList[i].value1, _roomBlueprint.ItemList[i].value2, objectContainer);
        }

        for (int i = 0; i < _roomBlueprint.EnemyList.Count; i++)
        {
            GenerateObject(_roomBlueprint.EnemyList[i].value1, _roomBlueprint.EnemyList[i].value2, enemyContainer);
        }

        // 보물방인 경우 => 아이템 전시
        if (roomType == RoomType.Treasure)
        {
            Main.Reward.DisplayTreasures(this.transform.position);
        }

        // 상점방인 경우 => 상품 전시
        if (roomType == RoomType.Shop)
        {
            Main.Reward.DisplayProducts(this.transform.position);
        }

        CheckRoomClear();
    }

    /// <summary>
    /// 방 클리어 조건 체크
    /// </summary>
    public void CheckRoomClear()
    {
        StartCoroutine(DelayCheck());
    }

    private IEnumerator DelayCheck()
    {
        yield return null;

        // 적이 없고, 방이 클리어가 아닌 상태일 경우
        // 잠겨있던 문 활성화, 체크시 보상까지 추가
        if (enemyContainer.childCount == 0 && !isCleared)
        {
            OpenActivatedDoor();
            
            if (_roomBlueprint.IsReward) 
            {
                RoomClearReward(); 
            }
            
            isCleared = true;
        }
    }
    
    /// <summary>
    /// 방 클리어시, 보상 획득
    /// </summary>
    private void RoomClearReward()
    {
        Main.Reward.GenerateReward(_roomBlueprint.RewardPosition, ObjectContainer);
    }

    public GameObject GenerateObject(GameObject prefab, Vector2 position, Transform container)
    {
        if (prefab == null) { return null; }

        GameObject go = Instantiate(prefab, container);
        go.transform.localPosition = position;
        return go;
    }

    #endregion
}
