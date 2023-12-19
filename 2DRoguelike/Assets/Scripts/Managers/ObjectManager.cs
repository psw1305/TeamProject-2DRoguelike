using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectManager
{
    public Player Player { get; private set; }
    public List<Enemy> Enemies { get; set; } = new();
    public List<Projectile> Projectiles { get; set; } = new();
    public List<BaseItem> Pickups {  get; set; } = new();

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

        //Enemy 종류별로 소환되야할 듯
        if (type == typeof(Enemy))
        {
            GameObject obj = Main.Resource.InstantiatePrefab($"{key}", pooling: true);
            obj.transform.position = position;

            Enemy enemy = obj.GetOrAddComponent<Enemy>();
            Enemies.Add(enemy);

            return enemy as T;
        }

        if (type == typeof(Projectile))
        {
            GameObject obj = Main.Resource.InstantiatePrefab($"{key}", pooling: true);
            obj.transform.position = position;

            Projectile projectile = obj.GetOrAddComponent<Projectile>();
            Projectiles.Add(projectile);

            return projectile as T;
        }

        //아이템 어떻게 되있는지 모름
        if(type == typeof(PickupItem)) 
        {

        }

        if (type == typeof(Bomb))
        {

        }

        if (type == typeof(Chest))
        {

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

        else if (type == typeof(Enemy))
        {

        }

        else if (type == typeof(PickupItem))
        {

        }

        else if (type == typeof(Bomb))
        {

        }

        else if (type == typeof(Chest))
        {

        }
    }


    public void Clear()
    {
        Player = null;
        Enemies.Clear();
        Projectiles.Clear();
    }
}