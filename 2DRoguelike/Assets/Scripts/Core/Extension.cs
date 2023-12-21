using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{

    // 풀 반환할때 확인용 메서드
    public static bool IsValid(this GameObject obj)
    {
        return obj != null && obj.activeSelf;
    }

    public static bool IsValid(this PlayerProjectile projectile)
    {
        return projectile != null && projectile.isActiveAndEnabled;
    }

    public static bool IsValid(this EnemyProjectile projectile)
    {
        return projectile != null && projectile.isActiveAndEnabled;
    }
}
