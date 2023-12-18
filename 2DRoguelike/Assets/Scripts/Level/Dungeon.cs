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
    private Room _currentRoom;

    #endregion

    #region Properties

    public Room[,] RoomArray => _roomArray;
    public Room CurrentRoom => _currentRoom;

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
        _currentRoom = startRoom;

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
            if (room != null && room.ActiveDoorCount == 1 && room != _currentRoom)
            {
                specialRoomList.Add(room);
            }
        }

        SetRoomsType(specialRoomList);
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

    /// <summary>
    /// 방 기준으로 상하좌우 방이 존재 시, 문 생성
    /// </summary>
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

    /// <summary>
    /// 방 타입 설정
    /// </summary>
    /// <param name="specialRoomList">특수방 리스트</param>
    private void SetRoomsType(List<Room> specialRoomList)
    {
        foreach (Room room in _roomArray)
        {
            if (room != null)
            {
                room.SetRoomType(RoomType.Normal);
            }
        }

        // 보스와 상점 1개씩을 제외한 나머지 특수방들은 보물방
        for (int i = 0; i < specialRoomList.Count - 2; i++)
        {
            specialRoomList[i].SetRoomType(RoomType.Treasure);
        }

        // 보스 방 생성
        specialRoomList[specialRoomList.Count - 1].SetRoomType(RoomType.Boss);
        // 상점 방 생성
        specialRoomList[specialRoomList.Count - 2].SetRoomType(RoomType.Shop);
        // 시작 방 설정
        _currentRoom.SetRoomType(RoomType.Start);

        foreach (Room room in _roomArray)
        {
            if (room != null)
            {
                room.Initialize();
            }
        }
    }

    #endregion

    #region Move To Room Method

    /// <summary>
    /// 맨 처음 시작 시, 방 설정
    /// </summary>
    private void MoveToStartRoom()
    {
        StartCoroutine(MoveToDesignativetRoom(Vector2Int.zero));
    }

    /// <summary>
    /// 들어간 방향에 따라, 다음 방 활성화
    /// </summary>
    /// <param name="MoveDirection">들어간 방향</param>
    public void MoveToNextRoom(Vector2Int MoveDirection)
    {
        StartCoroutine(MoveToDesignativetRoom(MoveDirection));
    }

    /// <summary>
    /// 방에 진입할 시, 좌표 설정 및 문 활성화 체크
    /// </summary>
    /// <param name="MoveDirection">들어간 방향</param>
    /// <returns>카메라 움직임 딜레이</returns>
    private IEnumerator MoveToDesignativetRoom(Vector2Int MoveDirection)
    {
        float delaySeconds = 0.3f;

        int x = _currentRoom.Coordinate.x + MoveDirection.y;
        int y = _currentRoom.Coordinate.y + MoveDirection.x;

        _currentRoom = _roomArray[x, y];
        _currentRoom.OpenActivatedDoor();

        Main.Game.Player.transform.position += new Vector3(MoveDirection.x * 5, MoveDirection.y * 5);

        Vector3 originPos = Camera.main.transform.position;
        Vector3 targetPos = _currentRoom.transform.position;
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
