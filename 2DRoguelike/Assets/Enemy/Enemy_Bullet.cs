using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    protected JEH_Player _player;

   public float speed = 2f;
   public int atk = 1;
    public Vector2 dir;

    void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<JEH_Player>();
    }

    void OnEnable()
    {
        Destroy(gameObject, 5f);
    }


    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(_player._tag))
        {
            _player.Damaged(atk);
            Debug.Log("공기팡!");
            Destroy(gameObject);
        }

    }
}
