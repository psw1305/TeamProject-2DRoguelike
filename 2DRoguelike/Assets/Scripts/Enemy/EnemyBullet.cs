using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    float _speed = 3f;


    private void Start()
    {

        Destroy(gameObject, 3);

    }

    void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

}
