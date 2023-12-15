using System;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    #region Field

    [SerializeField] private Room roomPrefab;
    [SerializeField] private int roomAmount;
    private Room[,] _roomArray = new Room[100, 100];
    //private Room _currentRoom;

    #endregion

    #region Init

    private void Start()
    {
        CreateRooms();
    }

    #endregion

    #region Room Method

    private void CreateRooms()
    {
        // 방 생성을 위한 좌표 리스트
        List<Vector2> alternativeRoomList = new();
        List<Vector2> hasBeenRemoveRoomList = new();

        alternativeRoomList.Clear();
        hasBeenRemoveRoomList.Clear();

        // 시작 방 생성
        int outsetX = _roomArray.GetLength(0) / 2;
        int outsetY = _roomArray.GetLength(1) / 2;
        Room lastRoom = _roomArray[outsetX, outsetY] = CreateRoom(new Vector2(outsetX, outsetY));
        //_currentRoom = lastRoom;

        // 다른 방 생성 메서드
        Action<int, int> action = (newX, newY) =>
        {
            Vector2 coordinate = new(newX, newY);

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

        // 방 생성
        for (int i = 1; i < roomAmount; i++)
        {
            int x = (int)lastRoom.coordinate.x;
            int y = (int)lastRoom.coordinate.y;

            action(x + 1, y);
            action(x - 1, y);
            action(x, y + 1);
            action(x, y - 1);

            Vector2 newRoomCoordinate = alternativeRoomList[UnityEngine.Random.Range(0, alternativeRoomList.Count)];
            lastRoom = _roomArray[(int)newRoomCoordinate.x, (int)newRoomCoordinate.y] = CreateRoom(newRoomCoordinate);
            alternativeRoomList.Remove(newRoomCoordinate);
        }
    }

    /// <summary>
    /// 해당 좌표에 방 생성
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    private Room CreateRoom(Vector2 coordinate)
    {
        Room newRoom = Instantiate(roomPrefab, transform);
        newRoom.coordinate = coordinate;

        int x = (int)coordinate.x - _roomArray.GetLength(0) / 2;
        int y = (int)coordinate.y - _roomArray.GetLength(1) / 2;
        newRoom.transform.position = new Vector2(y * Room.RoomWidth, x * Room.RoomHeight);

        return newRoom;
    }

    #endregion
}
