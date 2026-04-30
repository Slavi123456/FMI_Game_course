using System.Collections;
using UnityEngine;

public class FallingPlatformScript : MonoBehaviour
{
    public BoxCollider2D player;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rigidbody2;
    private bool hasTriggered = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        handleFalling();
    }

    void handleFalling()
    {
        if (boxCollider == null || rigidbody2 == null || hasTriggered)
            return;

        if (boxCollider.bounds.Intersects(player.bounds))
        {
            hasTriggered = true;
            StartCoroutine(FallRoutine());
        }
    }

    IEnumerator FallRoutine()
    {
        yield return new WaitForSeconds(1f);

        rigidbody2.bodyType = RigidbodyType2D.Dynamic;

        yield return new WaitForSeconds(3f);

        Destroy(gameObject);
    }
}
