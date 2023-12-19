using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Minimap : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform miniRoomNode;
    
    private float _cellWidth; 
    private float _height;

    private GameObject _miniRoomPrefab;
    private GameObject[,] _miniRoomArray;

    private GameObject _currentRoom;
    private Vector2Int _currentCoordinate;
    private List<Vector2> _hasBeenToList = new ();

    private Color white = new(1f, 1f, 1f, 1);
    private Color gray = new(0.6f, 0.6f, 0.6f, 1);
    private Color black = new(0.3f, 0.3f, 0.3f, 1);

    #endregion

    public void CreatMinimap()
    {
        _miniRoomPrefab = Main.Resource.GetObject("MinimapCell");
        _cellWidth = _miniRoomPrefab.GetComponent<RectTransform>().rect.size.x * transform.localScale.x;
        _height = _miniRoomPrefab.GetComponent<RectTransform>().rect.size.y * transform.localScale.y;

        var roomArray = Main.Game.Dungeon.RoomArray;
        _miniRoomArray = new GameObject[roomArray.GetLength(0), roomArray.GetLength(1)];
        Debug.Log(Main.Game.Dungeon.RoomArray.Length);

        foreach (Room room in roomArray)
        {
            if (room != null)
            {
                var cell = Instantiate(_miniRoomPrefab, miniRoomNode);
                _miniRoomArray[room.Coordinate.x, room.Coordinate.y] = cell;

                int x = room.Coordinate.y - roomArray.GetLength(0) / 2;
                int y = room.Coordinate.x - roomArray.GetLength(0) / 2;
                cell.GetComponent<RectTransform>().localPosition = new Vector2(x * _cellWidth, y * _height);

                var minimapIcon = cell.transform.GetChild(0).GetComponent<Image>();
                switch (room.RoomType)
                {
                    case RoomType.Boss:
                        minimapIcon.sprite = Main.Resource.GetSprite("minimap-boss");
                        break;
                    case RoomType.Treasure:
                        minimapIcon.sprite = Main.Resource.GetSprite("minimap-treasure");
                        break;
                    case RoomType.Shop:
                        minimapIcon.sprite = Main.Resource.GetSprite("minimap-shop");
                        break;
                    default:
                        minimapIcon.gameObject.SetActive(false);
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

        //_currentRoom.GetComponent<Image>().color = gray;
        _currentCoordinate.x += MoveDirection.y;
        _currentCoordinate.y += MoveDirection.x;
        //_currentRoom = _miniRoomArray[_currentCoordinate.x, _currentCoordinate.y];
        //_currentRoom.GetComponent<Image>().color = white;

        List<Vector2Int> neighborCoordinate = new()
        {
            _currentCoordinate + Vector2Int.right,_currentCoordinate + Vector2Int.left,
            _currentCoordinate + Vector2Int.down,_currentCoordinate + Vector2Int.up
        };

        //foreach (var coordinate in neighborCoordinate)
        //{
        //    GameObject cell = _miniRoomArray[coordinate.x, coordinate.y];
        //    if (cell != null && !_hasBeenToList.Contains(coordinate))
        //    {
        //        cell.GetComponent<Image>().color = black;
        //    }
        //}

        miniRoomNode.transform.localPosition -= new Vector3(MoveDirection.x * _cellWidth, MoveDirection.y * _height, 0);
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
