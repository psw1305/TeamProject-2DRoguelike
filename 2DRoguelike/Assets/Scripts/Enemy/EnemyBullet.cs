using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

   [HideInInspector] public Vector2 _target;

    float _speed = 3f;

    float rotZ;

    private void Start()
    {

        //  Vector3 ro = (_target - Vector2.zero);


        //     transform.rotation = Quaternion.AngleAxis(rotZ, Vector3.forward);

        //   float rotZ = Mathf.Atan2(_target.y, _target.x) * Mathf.Rad2Deg;
        //     transform.rotation = Quaternion.AngleAxis(rotZ - 90f, Vector3.forward);

        // Quaternion angleAxis = Quaternion.AngleAxis(rotZ - 90f, Vector3.forward);
        // transform.rotation = angleAxis;

        Destroy(gameObject, 4);

    }

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
