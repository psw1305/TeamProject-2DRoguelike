using UnityEngine;

public class SpawnTrap : Obstacle
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private int spawnCount;
    private Vector3 spawnPosition;

    private void Start()
    {
        spawnPosition = gameObject.transform.position;
    }

    public override void DestroySelf()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
        }

        base.DestroySelf();
    }
}
