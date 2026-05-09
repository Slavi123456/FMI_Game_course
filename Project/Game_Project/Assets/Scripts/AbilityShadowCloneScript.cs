using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class AbilityShadowCloneScript : MonoBehaviour
{
    private Queue<Vector2> mousePathPoints = new Queue<Vector2>();
    private Queue<GameObject> pointsVisual = new Queue<GameObject>();
    private HashSet<Enemy> recentAttackedEnemies = new HashSet<Enemy>();

    private CircleCollider2D circleCollider;
    private float pointTimer = 0f;
    private enum cloneState { 
        FollowingPath,
        Attacking
    };

    public float pointMaxTimer = 0.3f;
    public float maxDuration = 6f;
    public float speed;
    public float enemyRemoveTaimer = 0.5f;
    public GameObject pointPrefab;
    public UnityAction onFinished;

    int enemyLayerMask;
    private void Start()
    {
        enemyLayerMask = LayerMask.GetMask("Enemy");
        StartCoroutine(AbilityDuration());
        circleCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        handleTrajectoryTimer();
        //handleAbilityDuration();
        checkForPoints();
        checkForEnemies();
    }
    IEnumerator AbilityDuration()
    {
        yield return new WaitForSeconds(maxDuration);

        foreach (GameObject point in pointsVisual)
        {
            Destroy(point);
        }
        mousePathPoints.Clear();
        Debug.Log("Shadow Clone ability deactivated");
        onFinished?.Invoke();
        Destroy(gameObject);
    }
    private  void handleTrajectoryTimer()
    {
        if (pointMaxTimer <= pointTimer)
        {
            pointTimer = 0;
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
            worldPos.z = 0f;

            mousePathPoints.Enqueue(new Vector2(worldPos.x, worldPos.y));


            GameObject v = Instantiate(pointPrefab, worldPos, Quaternion.identity);
            pointsVisual.Enqueue(v);
            //Debug.Log("Saved point " + worldPos.x + " " + worldPos.y);
        }
        pointTimer += Time.deltaTime;
    }
    private  void checkForPoints()
    {
        if (mousePathPoints.Count > 0)
        {
            Vector2 target = mousePathPoints.Peek();

            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            if (Vector2.Distance(transform.position, target) < 0.1f)
            {
                mousePathPoints.Dequeue();

                GameObject point = pointsVisual.Dequeue();
                Destroy(point);
            }
        }
    }

    
    private void checkForEnemies() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, circleCollider.radius, enemyLayerMask);
        foreach (Collider2D hit in hits) {
            //Debug.Log("hit: " + hit);
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy == null) return;
            if (recentAttackedEnemies.Contains(enemy)) return;
            recentAttackedEnemies.Add(enemy);
            Debug.Log("Enemy: " + enemy);

            StartCoroutine(RemoveEnemy(enemy));
        }
    }

    private IEnumerator RemoveEnemy(Enemy enemy) { 
        yield return new WaitForSeconds(enemyRemoveTaimer);

        recentAttackedEnemies.Remove(enemy);
    }
}
