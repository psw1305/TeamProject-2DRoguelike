using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Fields

    [SerializeField] private Vector2Int coordinate;
    [SerializeField] private List<GameObject> doorList;

    private List<GameObject> activeDoorList = new();
    private List<GameObject> neighborDoorList = new();
    private List<Room> neighborRoomList = new();

    #endregion

    #region Properties

    public Vector2Int Coordinate => coordinate;
    public List<GameObject> DoorList => doorList;
    public int ActiveDoorCount => activeDoorList.Count;

    public void SetCoordinate(Vector2Int coordinate)
    {
        this.coordinate = coordinate;
    }

    #endregion

    #region Methods

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
