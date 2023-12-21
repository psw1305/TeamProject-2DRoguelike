using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public Player Player { get; private set; }
    public List<PlayerProjectile> PlayerProjectiles { get; set; } = new();
    public List<EnemyProjectile> EnemyProjectiles { get; set; } = new();

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


        if (type == typeof(PlayerProjectile))
        {
            GameObject obj = Main.Resource.InstantiatePrefab($"{key}", pooling: true);
            obj.transform.position = position;

            PlayerProjectile projectile = obj.GetOrAddComponent<PlayerProjectile>();
            PlayerProjectiles.Add(projectile);

            return projectile as T;
        }

        if (type == typeof(EnemyProjectile))
        {
            GameObject obj = Main.Resource.InstantiatePrefab($"{key}", pooling: true);
            obj.transform.position = position;

            EnemyProjectile projectile = obj.GetOrAddComponent<EnemyProjectile>();
            EnemyProjectiles.Add(projectile);

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
      
        else if (type == typeof(PlayerProjectile))
        {
            PlayerProjectiles.Remove(obj as PlayerProjectile);
            Main.Resource.Destroy(obj.gameObject);
        }

        else if (type == typeof(EnemyProjectile))
        {
            EnemyProjectiles.Remove(obj as EnemyProjectile);
            Main.Resource.Destroy(obj.gameObject);
        }

    }


    public void Clear()
    {
        Player = null;
        PlayerProjectiles.Clear();
        EnemyProjectiles.Clear();
    }
}