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

    public Vector2Int Coordinate { get; set; }
    public List<GameObject> DoorList => doorList;

    #endregion

    #region Methods

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

    #endregion

}
