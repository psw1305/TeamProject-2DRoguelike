using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceManager
{
    private Dictionary<string, GameObject> models = new();
    private Dictionary<string, Sprite> sprites = new();

    /// <summary>
    /// Resources 폴더 안 아이템 불러오기
    /// </summary>
    public void Initialize()
    {
        LoadPrefabs("Prefabs/Models", models);
        LoadSprites("Sprites", sprites);
    }

    #region Prefab

    /// <summary>
    /// 지정된 경로 안에 모든 프리팹들 로드
    /// </summary>
    /// <param name="path">폴더 경로</param>
    /// <param name="prefabs">로드할 프리팹 값</param>
    private void LoadPrefabs(string path, Dictionary<string, GameObject> prefabs)
    {
        GameObject[] objs = Resources.LoadAll<GameObject>(path);
        foreach (GameObject obj in objs)
        {
            prefabs[obj.name] = obj;
        }
    }

    /// <summary>
    /// string key를 기반으로 오브젝트 가져오기
    /// </summary>
    /// <param name="prefabName"></param>
    /// <returns></returns>
    public GameObject GetObject(string prefabName)
    {
        if (!models.TryGetValue(prefabName, out GameObject prefab)) return null;
        return prefab;
    }

    #endregion

    #region Sprite

    /// <summary>
    /// 지정된 경로 안에 모든 스프라이트 로드
    /// </summary>
    /// <param name="path">폴더 경로</param>
    /// <param name="sprites">로드할 스프라이트 값</param>
    private void LoadSprites(string path, Dictionary<string, Sprite> sprites)
    {
        Sprite[] objs = Resources.LoadAll<Sprite>(path);
        foreach (Sprite obj in objs)
        {
            sprites[obj.name] = obj;
        }
    }

    public Sprite GetSprite(string spriteName)
    {
        if (!sprites.TryGetValue(spriteName, out Sprite sprite)) return null;
        return sprite;
    }

    #endregion

    #region ScriptableObject

    public ItemBlueprint[] GetItemBlueprints(string path)
    {
        return Resources.LoadAll<ItemBlueprint>(path);
    }

    #endregion

    #region Pool

    public GameObject InstantiatePrefab(string key, Transform parent = null, bool pooling = false)
    {
        GameObject prefab = GetObject(key);
        if (prefab == null)
        {
            Debug.LogError($"[ResourceManager] Instantiate({key}): Failed to load prefab.");
            return null;
        }

        if (pooling) return Main.Pool.Pop(prefab);

        GameObject obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.name = prefab.name;
        return obj;
    }

    // 해당 오브젝트를 풀에 돌려놓거나 파괴한다.
    public void Destroy(GameObject obj)
    {
        if (obj == null) return;

        if (Main.Pool.Push(obj)) return;

        Object.Destroy(obj);
    }

    #endregion

    #region InitOnLoad

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitApplication()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("InitOnLoad");

        if (prefabs.Length > 0)
        {
            foreach (var prefab in prefabs)
            {
                GameObject go = Object.Instantiate(prefab);
                go.name = prefab.name;
                Object.DontDestroyOnLoad(go);
            }
        }
    }

    #endregion
}
