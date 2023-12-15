using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string, GameObject> models = new();

    /// <summary>
    /// Resources 폴더 안 아이템 불러오기
    /// </summary>
    public void Initialize()
    {
        LoadPrefabs("Prefabs/Models", models);
    }

    /// <summary>
    /// 지정된 경로 안에 모든 프리팹들 로드
    /// </summary>
    /// <param name="path">폴더 경로</param>
    /// <param name="prefabs">로드할 프리팹 필드값</param>
    public void LoadPrefabs(string path, Dictionary<string, GameObject> prefabs)
    {
        GameObject[] objs = Resources.LoadAll<GameObject>(path);
        foreach (GameObject obj in objs)
        {
            prefabs[obj.name] = obj;
        }
    }

    public GameObject Instantiate(string prefabName)
    {
        if (!models.TryGetValue(prefabName, out GameObject prefab)) return null;
        return GameObject.Instantiate(prefab);
    }
}
