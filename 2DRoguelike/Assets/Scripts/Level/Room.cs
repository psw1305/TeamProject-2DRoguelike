using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Fields

    [SerializeField] private RoomType roomType;
    [SerializeField] private Vector2Int coordinate;
    [SerializeField] private List<GameObject> doorList;

    private List<GameObject> activeDoorList = new();
    private List<GameObject> neighborDoorList = new();
    private List<Room> neighborRoomList = new();

    #endregion

    #region Properties

    public RoomType RoomType => roomType;
    public Vector2Int Coordinate => coordinate;
    public List<GameObject> DoorList => doorList;
    public int ActiveDoorCount => activeDoorList.Count;

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
        ChangeDoorOutWard();
    }

    #endregion

    #region Methods

    /// <summary>
    /// 타입에 따른 문 디자인 변경
    /// </summary>
    private void ChangeDoorOutWard()
    {
        if (roomType == RoomType.Normal || roomType == RoomType.Start) { return; }

        List<GameObject> doors = new();
        doors.AddRange(activeDoorList);
        doors.AddRange(neighborDoorList);

        foreach (var door in doors)
        {
            SpriteRenderer doorFrame = door.transform.GetChild(0).GetComponent<SpriteRenderer>();

            switch (roomType)
            {
                case RoomType.Boss:
                    doorFrame.sprite = Main.Resource.GetSprite("door-boss");
                    break;
                case RoomType.Treasure:
                    doorFrame.sprite = Main.Resource.GetSprite("door-treasure");
                    break;
                case RoomType.Shop:
                    doorFrame.sprite = Main.Resource.GetSprite("door-shop");
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

        activeDoorList.Add(door);
        neighborDoorList.Add(neighborDoor);
        neighborRoomList.Add(neighborRoom);    
    }

    /// <summary>
    /// 방 클리어 시 => 등록된 문 수 만큼 활성화
    /// </summary>
    public void OpenActivatedDoor()
    {
        for (int i = 0; i < activeDoorList.Count; i++)
        {
            var door = activeDoorList[i];
            door.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    #endregion

}
