using UnityEngine;


public class RangeTrap : MonoBehaviour
{
    [SerializeField] private LayerMask activeTargetLayer;

    [SerializeField] private float trapShootDelay;

    [SerializeField] private GameObject bullet;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activeTargetLayer.value == (activeTargetLayer | (1 << collision.gameObject.layer)))
        {
            Debug.Log("활성화");
            InvokeRepeating("ActiveRangeTrap", 0, trapShootDelay);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (activeTargetLayer.value == (activeTargetLayer | (1 << collision.gameObject.layer)))
        {
            CancelInvoke();
        }
    }

    private void ActiveRangeTrap()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }
}
