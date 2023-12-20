using UnityEngine;

public class Obstacle : MonoBehaviour, IGenerateReward
{
    [SerializeField][Range(0f, 100f)] 
    private int RewardDropRate;

    [SerializeField] private ParticleSystem destroyParticle;

    [SerializeField] private LayerMask canDestroyLayer;

    public virtual void DestroySelf()
    {
        //1. 파티클 생성 및 재생
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        destroyParticle.Play();
        
        //2. 보상 생성 판정
        GenerateReward();

        //3. 객체 파괴
        Destroy(gameObject);
    }

    public void GenerateReward()
    {
        if (Random.Range(1, 101) <= RewardDropRate)
        {
            Main.Reward.PickupItemDrop(this.transform.position, Main.Game.Dungeon.CurrentRoom.ObjectContainer, 0);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDestroyLayer.value == (canDestroyLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroySelf();
        }
    }
}
