using UnityEngine;

public class BulletTest : MonoBehaviour
{

    public Vector3 _target;


    float _speed = 4f;

    void Update()
    {
        transform.Translate(_target * _speed * Time.deltaTime);

    }
}
