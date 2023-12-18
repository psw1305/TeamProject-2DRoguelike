using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public Player Player { get; private set; }
    public List<Enemy> Enemies { get; set; } = new();
    private List<Projectile> Projectiles { get; set; } = new();

    public T Spawn<T>(string key, Vector2 position) where T : MonoBehaviour
    {
        System.Type type = typeof(T);

        if (type == typeof(Player))
        {
            GameObject obj = Main.Resource.InstantiatePrefab("Player");
            obj.transform.position = position;

            Player = obj.GetOrAddComponent<Player>();

            return Player as T;
        }

    

        if (type == typeof(Projectile))
        {
            GameObject obj = Main.Resource.InstantiatePrefab($"{key}", pooling: true);
            obj.transform.position = position;

            Projectile projectile = obj.GetOrAddComponent<Projectile>();
            Projectiles.Add(projectile);

            return projectile as T;
        }
        return null;
    }

    public void Despawn<T>(T obj) where T : MonoBehaviour
    {
        Type type = typeof(T);

        if (type == typeof(Player))
        {

        }
      
        else if (type == typeof(Projectile))
        {
            Projectiles.Remove(obj as Projectile);
            Main.Resource.Destroy(obj.gameObject);
        }
     
    }
}