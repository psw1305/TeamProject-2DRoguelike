using UnityEngine;

public class Obstacle : MonoBehaviour, IGenerateReward
{
    [SerializeField][Range(0f, 100f)] 
    private int RewardDropRate;
    
    [SerializeField] private GameObject rewardTable;
    private GameObject mainObj;

    protected virtual void DestroySelf()
    {
        mainObj = transform.parent.gameObject;
        Destroy(mainObj);
    }

    public void GenerateReward()
    {
        int ran = Random.Range(1, 101);
        if (ran <= RewardDropRate)
        {
            Debug.Log("보상 생성됨" + transform.position.x + transform.position.y);
        }
        Debug.Log(ran);
    }
}
