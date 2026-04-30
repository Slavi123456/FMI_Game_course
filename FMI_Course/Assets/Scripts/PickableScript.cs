using System.Collections;
using UnityEngine;

public class PickableScript : MonoBehaviour
{
    public BoxCollider2D player;
    public PlayerScript playerScript;
    private PolygonCollider2D boxCollider;
    private bool hasTriggered = false;
    void Start()
    {
        boxCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        handlePickingUp();
    }

    private void handlePickingUp()
    {
        if (boxCollider == null || hasTriggered)
            return;

        if (boxCollider.bounds.Intersects(player.bounds))
        {
            hasTriggered = true;
            StartCoroutine(PickUpRoutine());
        }
    }

    IEnumerator PickUpRoutine () {
        yield return new WaitForSeconds(0.2f);

        playerScript.collectGem();

        Destroy(gameObject);
    }
}
