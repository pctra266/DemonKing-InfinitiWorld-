using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float moveDistance = 3f;
    private Vector3 startPos;
    private int direction = 1;
    void OnEnable()
    {
        startPos = transform.position;
        direction = 1; 
    }


    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (Vector3.Distance(startPos, transform.position) >= moveDistance)
            direction *= -1;
    }
}
