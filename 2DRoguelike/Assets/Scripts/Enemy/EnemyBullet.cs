using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

   [HideInInspector] public Vector2 _target;
    float _speed = 3f;


    void Update()
    {
        transform.Translate(_target * _speed * Time.deltaTime);
    }
    /*
     
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("트리거엔터");
    }
     
     
     */
}
