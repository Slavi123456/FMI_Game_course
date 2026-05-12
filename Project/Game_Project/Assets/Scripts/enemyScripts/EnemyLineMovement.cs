using System.Drawing;
using UnityEngine;

public class EnemyLineMovement: MonoBehaviour
{
    private Transform transform;
    private Transform player;
    public float speed = 1f;

    private void Start()
    {
        transform = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        LineMovement();
    }
    private void LineMovement()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(player.position.x, player.position.y),
            speed * Time.deltaTime
        );
    }
}
