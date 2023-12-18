using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Minimap : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform miniRoomNode;
    [SerializeField] private GameObject miniRoomPrefab;
    [SerializeField] private GameObject miniIconBoss;
    [SerializeField] private GameObject miniIconTreasure;
    [SerializeField] private GameObject miniIconShop;
    
    private float _width; 
    private float _height;
    private GameObject[,] _miniRoomArray;
    private GameObject _currentRoom;
    private Vector2Int _currentCoordinate;
    private List<Vector2> _hasBeenToList = new ();

    private Color white = new Color(1f, 1f, 1f, 1);
    private Color gray = new Color(0.6f, 0.6f, 0.6f, 1);
    private Color black = new Color(0.3f, 0.3f, 0.3f, 1);

    #endregion

    private void Start()
    {
        _width = miniRoomPrefab.GetComponent<RectTransform>().rect.size.x * transform.localScale.x;
        _height = miniRoomPrefab.GetComponent<RectTransform>().rect.size.y * transform.localScale.y;
    }

    public void CreatMinimap()
    {
        Room[,] roomArray = Main.Game.Dungeon.RoomArray;
        _miniRoomArray = new GameObject[roomArray.GetLength(0), roomArray.GetLength(1)];

        foreach (Room room in roomArray)
        {
            if (room != null)
            {
                var cell = Instantiate(miniRoomPrefab, miniRoomNode);
                _miniRoomArray[room.Coordinate.x, room.Coordinate.y] = cell;

                int x = room.Coordinate.y - roomArray.GetLength(0) / 2;
                int y = room.Coordinate.x - roomArray.GetLength(0) / 2;
                cell.GetComponent<RectTransform>().localPosition = new Vector2(x * _width, y * _height);

                switch (room.RoomType)
                {
                    case RoomType.Boss:
                        Instantiate(miniIconBoss, cell.transform);
                        break;
                    case RoomType.Treasure:
                        Instantiate(miniIconTreasure, cell.transform);
                        break;
                    case RoomType.Shop:
                        Instantiate(miniIconShop, cell.transform);
                        break;
                    default:
                        break;
                }
            }
        }

        _currentCoordinate = new Vector2Int(Main.Game.Dungeon.CurrentRoom.Coordinate.x, Main.Game.Dungeon.CurrentRoom.Coordinate.y);
        _currentRoom = _miniRoomArray[_currentCoordinate.x, _currentCoordinate.y];
    }

    public void UpdateMinimap(Vector2Int MoveDirection)
    {
        _hasBeenToList.Add(_currentCoordinate);

        _currentRoom.GetComponent<Image>().color = gray;
        _currentCoordinate.x += MoveDirection.y;
        _currentCoordinate.y += MoveDirection.x;
        _currentRoom = _miniRoomArray[_currentCoordinate.x, _currentCoordinate.y];
        _currentRoom.GetComponent<Image>().color = white;

        List<Vector2> neighborCoordinate = new List<Vector2>()
        {
            _currentCoordinate + Vector2Int.right,_currentCoordinate + Vector2Int.left,
            _currentCoordinate + Vector2Int.down,_currentCoordinate + Vector2Int.up
        };
        foreach (var coordinate in neighborCoordinate)
        {
            GameObject cell = _miniRoomArray[(int)coordinate.x, (int)coordinate.y];
            if (cell != null && !_hasBeenToList.Contains(coordinate))
            {
                cell.GetComponent<Image>().color = black;
            }
        }

        miniRoomNode.transform.localPosition -= new Vector3(MoveDirection.x * _width, MoveDirection.y * _height, 0);
    }

    public void ShowAllMinimap()
    {
        for (int i = 0; i < _miniRoomArray.GetLength(0); i++)
        {
            for (int j = 0; j < _miniRoomArray.GetLength(1); j++)
            {
                if (_miniRoomArray[i, j] != null && !_hasBeenToList.Contains(new Vector2(i, j)))
                {
                    _miniRoomArray[i, j].GetComponent<Image>().color = black;
                }
            }
        }

        UpdateMinimap(Vector2Int.zero);
    }
}
