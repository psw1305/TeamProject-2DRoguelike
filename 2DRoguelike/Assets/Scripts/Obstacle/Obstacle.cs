using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Obstacle : MonoBehaviour, IGenerateReward
{
    [SerializeField][Range(0f, 100f)] 
    private int RewardDropRate;

    [SerializeField] private ParticleSystem destroyParticle;

    [SerializeField] private LayerMask canDestroyLayer;

    private GameObject _mainObj;

    protected virtual void DestroySelf()
    {
        Instantiate(destroyParticle, gameObject.transform.position, Quaternion.identity);
        destroyParticle.Play();

        _mainObj = transform.parent.gameObject;
        Destroy(_mainObj);
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
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDestroyLayer.value == (canDestroyLayer.value | (1 << collision.gameObject.layer)))
        {
            DestroySelf();
        }
    }
}
