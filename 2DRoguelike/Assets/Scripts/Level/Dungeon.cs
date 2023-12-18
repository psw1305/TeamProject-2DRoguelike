using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour
{
    #region Field

    [SerializeField] private int roomAmount;

    private Room _roomPrefab;
    private Room[,] _roomArray = new Room[20, 20];
    private Room currentRoom;

    #endregion

    #region Init

    public void Initialize()
    {
        // 던전 생성
        _roomPrefab = Main.Resource.GetObject("Room").GetComponent<Room>();
        CreateDungeon();

        // 시작 방 세팅
        MoveToStartRoom();
    }

    #endregion

    #region Room Create Method

    /// <summary>
    /// 던전 생성
    /// </summary>
    private void CreateDungeon()
    {
        // 방 생성을 위한 좌표 리스트
        List<Vector2Int> alternativeRoomList = new();
        List<Vector2Int> hasBeenRemoveRoomList = new();
        // 특수 방 리스트
        List<Room> specialRoomList = new();

        // 시작 방 생성
        int outsetX = _roomArray.GetLength(0) / 2;
        int outsetY = _roomArray.GetLength(1) / 2;
        Room startRoom = _roomArray[outsetX, outsetY] = CreateRoom(new Vector2Int(outsetX, outsetY));
        currentRoom = startRoom;

        // 방 생성 델리게이트
        Action<int, int> action = (newX, newY) =>
        {
            Vector2Int coordinate = new(newX, newY);

            if (_roomArray[newX, newY] == null)
            {
                if (alternativeRoomList.Contains(coordinate))
                {
                    alternativeRoomList.Remove(coordinate);
                    hasBeenRemoveRoomList.Add(coordinate);
                }
                else if (!hasBeenRemoveRoomList.Contains(coordinate))
                {
                    alternativeRoomList.Add(coordinate);
                }
            }
        };

        // 지정된 수 만큼 방 생성
        for (int i = 1; i < roomAmount; i++)
        {
            int x = startRoom.Coordinate.x;
            int y = startRoom.Coordinate.y;

            action(x + 1, y);
            action(x - 1, y);
            action(x, y + 1);
            action(x, y - 1);

            Vector2Int newRoomCoordinate = alternativeRoomList[UnityEngine.Random.Range(0, alternativeRoomList.Count)];
            startRoom = _roomArray[newRoomCoordinate.x, newRoomCoordinate.y] = CreateRoom(newRoomCoordinate);
            alternativeRoomList.Remove(newRoomCoordinate);
        }

        LinkDoors();

        // 방 문이 1개인 경우 => 특수 방으로 추가
        foreach (Room room in _roomArray)
        {
            if (room != null && room.ActiveDoorCount == 1 && room != currentRoom)
            {
                specialRoomList.Add(room);
            }
        }
    }

    /// <summary>
    /// 해당 좌표에 방 생성
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    private Room CreateRoom(Vector2Int coordinate)
    {
        Room newRoom = Instantiate(_roomPrefab, this.transform);
        newRoom.SetCoordinate(coordinate);

        int x = coordinate.x - _roomArray.GetLength(0) / 2;
        int y = coordinate.y - _roomArray.GetLength(1) / 2;
        newRoom.transform.position = new Vector2(y * Globals.RoomWidth, x * Globals.RoomHeight);

        return newRoom;
    }

    private void LinkDoors()
    {
        foreach (Room room in _roomArray) 
        {
            if (room != null)
            {
                int x = room.Coordinate.x;
                int y = room.Coordinate.y;

                if (_roomArray[x + 1, y] != null)
                {
                    room.ActivationDoor(RoomDirection.Up, _roomArray[x + 1, y], _roomArray[x + 1, y].DoorList[1]);
                }

                if (_roomArray[x - 1, y] != null)
                {
                    room.ActivationDoor(RoomDirection.Down, _roomArray[x - 1, y], _roomArray[x - 1, y].DoorList[0]);
                }

                if (_roomArray[x, y - 1] != null)
                {
                    room.ActivationDoor(RoomDirection.Left, _roomArray[x, y - 1], _roomArray[x, y - 1].DoorList[3]);
                }

                if (_roomArray[x, y + 1] != null)
                {
                    room.ActivationDoor(RoomDirection.Right, _roomArray[x, y + 1], _roomArray[x, y + 1].DoorList[2]);
                }
            }
        }
    }

    #endregion

    #region Move To Room Method

    private void MoveToStartRoom()
    {
        StartCoroutine(MoveToDesignativetRoom(Vector2Int.zero));
    }

    public void MoveToNextRoom(Vector2Int MoveDirection)
    {
        StartCoroutine(MoveToDesignativetRoom(MoveDirection));
    }

    private IEnumerator MoveToDesignativetRoom(Vector2Int MoveDirection)
    {
        float delaySeconds = 0.3f;

        int x = currentRoom.Coordinate.x + MoveDirection.y;
        int y = currentRoom.Coordinate.y + MoveDirection.x;

        currentRoom = _roomArray[x, y];
        Debug.Log($"{x} : {y}");
        currentRoom.OpenActivatedDoor();

        Main.Game.Player.transform.position += new Vector3(MoveDirection.x * 5, MoveDirection.y * 5);

        Vector3 originPos = Camera.main.transform.position;
        Vector3 targetPos = currentRoom.transform.position;
        targetPos.z += Camera.main.transform.position.z;

        float time = 0;
        while (time <= delaySeconds)
        {
            Camera.main.transform.position = Vector3.Lerp(originPos, targetPos, (1 / delaySeconds) * (time += Time.deltaTime));
            yield return null;
        }
    }

    #endregion
}
