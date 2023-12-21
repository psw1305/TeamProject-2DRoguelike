using UnityEngine;

public class DieEffect : MonoBehaviour
{
    void Start()
    {
        Invoke("Die", 3);
    }

    void Die()
    {
        Destroy(gameObject);

    }
}
